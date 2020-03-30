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
        public List<StorageViewModel> GetList()
        {
            using (var context = new DinerDatabase())
            {
                return context.Storages
                .ToList()
               .Select(rec => new StorageViewModel
               {
                   Id = rec.Id,
                   StorageName = rec.StorageName,
                   StorageFoods = context.StorageFoods
                .Include(recSF => recSF.Food)
               .Where(recSF => recSF.StorageId == rec.Id).
               Select(x => new StorageFoodViewModel
               {
                   Id = x.Id,
                   StorageId = x.StorageId,
                   FoodId = x.FoodId,
                   FoodName = context.Foods.FirstOrDefault(y => y.Id == x.FoodId).FoodName,
                   Count = x.Count
               })
               .ToList()
               })
            .ToList();
            }
        }
        public StorageViewModel GetElement(int id)
        {
            using (var context = new DinerDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.Id == id);
                if (elem == null)
                {
                    throw new Exception("Элемент не найден");
                }
                else
                {
                    return new StorageViewModel
                    {
                        Id = id,
                        StorageName = elem.StorageName,
                        StorageFoods = context.StorageFoods
                .Include(recSF => recSF.Food)
               .Where(recSF => recSF.StorageId == elem.Id)
                        .Select(x => new StorageFoodViewModel
                        {
                            Id = x.Id,
                            StorageId = x.StorageId,
                            FoodId = x.FoodId,
                            FoodName = context.Foods.FirstOrDefault(y => y.Id == x.FoodId).FoodName,
                            Count = x.Count
                        }).ToList()
                    };
                }
            }
        }
        public void AddElement(StorageBindingModel model)
        {
            using (var context = new DinerDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.StorageName == model.StorageName);
                if (elem != null)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
                var storage = new Storage();
                context.Storages.Add(storage);
                storage.StorageName = model.StorageName;
                context.SaveChanges();
            }
        }
        public void UpdElement(StorageBindingModel model)
        {
            using (var context = new DinerDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.StorageName == model.StorageName && x.Id != model.Id);
                if (elem != null)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
                var elemToUpdate = context.Storages.FirstOrDefault(x => x.Id == model.Id);
                if (elemToUpdate != null)
                {
                    elemToUpdate.StorageName = model.StorageName;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public void DelElement(int id)
        {
            using (var context = new DinerDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.Id == id);
                if (elem != null)
                {
                    context.Storages.Remove(elem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
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
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}
