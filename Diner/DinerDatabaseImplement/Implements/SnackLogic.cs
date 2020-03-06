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
    public class SnackLogic : ISnackLogic
    {
        public void CreateOrUpdate(SnackBindingModel model)
        {
            using (var context = new DinerDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Snack element = context.Snacks.FirstOrDefault(rec =>
                       rec.SnackName == model.SnackName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть изделие с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Snacks.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Snack();
                            context.Snacks.Add(element);
                        }
                        element.SnackName = model.SnackName;
                        element.Price = model.Price;
                        context.SaveChanges();
                        if (model.Id.HasValue)
                        {
                            var SnackFoods = context.SnackFoods.Where(rec
                           => rec.SnackId == model.Id.Value).ToList();
                            // удалили те, которых нет в модели
                            context.SnackFoods.RemoveRange(SnackFoods.Where(rec =>
                            !model.SnackFoods.ContainsKey(rec.FoodId)).ToList());
                            context.SaveChanges();
                            // обновили количество у существующих записей
                            foreach (var updateFood in SnackFoods)
                            {
                                updateFood.Count =
                               model.SnackFoods[updateFood.FoodId].Item2;

                                model.SnackFoods.Remove(updateFood.FoodId);
                            }
                            context.SaveChanges();
                        }
                        // добавили новые
                        foreach (var pc in model.SnackFoods)
                        {
                            context.SnackFoods.Add(new SnackFood
                            {
                                SnackId = element.Id,
                                FoodId = pc.Key,
                                Count = pc.Value.Item2
                            });
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(SnackBindingModel model)
        {
            using (var context = new DinerDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // удаяем записи по продуктам при удалении закуски
                        context.SnackFoods.RemoveRange(context.SnackFoods.Where(rec =>
                        rec.SnackId == model.Id));
                        Snack element = context.Snacks.FirstOrDefault(rec => rec.Id
                        == model.Id);
                        if (element != null)
                        {
                            context.Snacks.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public List<SnackViewModel> Read(SnackBindingModel model)
        {
            using (var context = new DinerDatabase())
            {
                return context.Snacks
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
               .Select(rec => new SnackViewModel
               {
                   Id = rec.Id,
                   SnackName = rec.SnackName,
                   Price = rec.Price,
                   SnackFoods = context.SnackFoods
                .Include(recPC => recPC.Food)
               .Where(recPC => recPC.SnackId == rec.Id)
               .ToDictionary(recPC => recPC.FoodId, recPC =>
                (recPC.Food?.FoodName, recPC.Count))
               })
               .ToList();
            }
        }
    }
}