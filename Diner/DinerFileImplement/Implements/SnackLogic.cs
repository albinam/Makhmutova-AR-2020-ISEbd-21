using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.Interfaces;
using DinerBusinessLogic.ViewModels;
using DinerFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerFileImplement.Implements
{
    public class SnackLogic : ISnackLogic
    {
        private readonly FileDataListSingleton source;
        public SnackLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(SnackBindingModel model)
        {
            Snack element = source.Snacks.FirstOrDefault(rec => rec.SnackName ==
           model.SnackName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть закуска с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Snacks.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Snacks.Count > 0 ? source.Foods.Max(rec =>
               rec.Id) : 0;
                element = new Snack { Id = maxId + 1 };
                source.Snacks.Add(element);
            }
            element.SnackName = model.SnackName;
            element.Price = model.Price;
            // удалили те, которых нет в модели
            source.SnackFoods.RemoveAll(rec => rec.SnackId == model.Id &&
           !model.SnackFoods.ContainsKey(rec.FoodId));
            // обновили количество у существующих записей
            var updateFoods = source.SnackFoods.Where(rec => rec.SnackId ==
           model.Id && model.SnackFoods.ContainsKey(rec.FoodId));
            foreach (var updateFood in updateFoods)
            {
                updateFood.Count =
               model.SnackFoods[updateFood.FoodId].Item2;
                model.SnackFoods.Remove(updateFood.FoodId);
            }
            // добавили новые
            int maxFSId = source.SnackFoods.Count > 0 ?
           source.SnackFoods.Max(rec => rec.Id) : 0;
            foreach (var pc in model.SnackFoods)
            {
                source.SnackFoods.Add(new SnackFood
                {
                    Id = ++maxFSId,
                    SnackId = element.Id,
                    FoodId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
        }
        public void Delete(SnackBindingModel model)
        {
            source.SnackFoods.RemoveAll(rec => rec.SnackId == model.Id);
            Snack element = source.Snacks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Snacks.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<SnackViewModel> Read(SnackBindingModel model)
        {
            return source.Snacks
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new SnackViewModel
            {
                Id = rec.Id,
                SnackName = rec.SnackName,
                Price = rec.Price,
                SnackFoods = source.SnackFoods
            .Where(recFS => recFS.SnackId == rec.Id)
           .ToDictionary(recFS => recFS.FoodId, recFS =>
            (source.Foods.FirstOrDefault(recC => recC.Id ==
           recFS.FoodId)?.FoodName, recFS.Count))
            })
            .ToList();
        }
    }
}