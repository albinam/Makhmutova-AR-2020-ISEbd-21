using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.Interfaces;
using DinerBusinessLogic.ViewModels;
using DinerDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerDatabaseImplement.Implements
{
    public class StorageLogic : IStorageLogic
    {
        public void CreateOrUpdate(StorageBindingModel model)
        {
            using (var context = new DinerDatabase())
            {
                Storage element = context.Storages.FirstOrDefault(rec => rec.StorageName == model.StorageName && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
                if (model.Id.HasValue)
                {
                    element = context.Storages.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Storage();
                    context.Storages.Add(element);
                }
                element.StorageName = model.StorageName;
                context.SaveChanges();
            }
        }
        public void Delete(StorageBindingModel model)
        {
            using (var context = new DinerDatabase())
            {
                context.StorageFoods.RemoveRange(context.StorageFoods.Where(rec => rec.StorageId == model.Id));
                Storage element = context.Storages.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Storages.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<StorageViewModel> Read(StorageBindingModel model)
        {
            using (var context = new DinerDatabase())
            {
                return context.Storages.Where(rec => model == null || rec.Id == model.Id)
                .ToList()
                .Select(rec => new StorageViewModel
                {
                    Id = rec.Id,
                    StorageName = rec.StorageName,
                    StorageFoods = context.StorageFoods
                                                .Include(recSF => recSF.Food)
                                                .Where(recSF => recSF.StorageId == rec.Id)
                                                .ToDictionary(recWC => recWC.StorageId, recWC => (
                                                    recWC.Food?.FoodName, recWC.Count
                                                ))
                }).ToList();
            }
        }
        public void FillStorage(StorageFoodBindingModel model)
        {
            using (var context = new DinerDatabase())
            {
                var item = context.StorageFoods.FirstOrDefault(x => x.FoodId == model.FoodId
    && x.StorageId == model.StorageId);

                if (item != null)
                {
                    item.Count += model.Count;
                }
                else
                {
                    var elem = new StorageFood();
                    context.StorageFoods.Add(elem);
                    elem.StorageId = model.StorageId;
                    elem.FoodId = model.FoodId;
                    elem.Count = model.Count;
                }
                context.SaveChanges();
            }
        }
        public void RemoveFromStorage(int snackId, int snacksCount)
        {
            using (var context = new DinerDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var snackFoods = context.SnackFoods.Where(x => x.SnackId == snackId);
                        if (snackFoods.Count() == 0) return;
                        foreach (var elem in snackFoods)
                        {
                            int left = elem.Count * snacksCount;
                            var StorageFoods = context.StorageFoods.Where(x => x.FoodId == elem.FoodId);
                            int available = StorageFoods.Sum(x => x.Count);
                            if (available < left) throw new Exception("Недостаточно продуктов на складе");
                            foreach (var rec in StorageFoods)
                            {
                                int toRemove = left > rec.Count ? rec.Count : left;
                                rec.Count -= toRemove;
                                left -= toRemove;
                                if (left == 0) break;
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        return;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}