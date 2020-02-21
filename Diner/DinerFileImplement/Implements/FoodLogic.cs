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
    public class FoodLogic : IFoodLogic
    {
        private readonly FileDataListSingleton source;
        public FoodLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(FoodBindingModel model)
        {
            Food element = source.Foods.FirstOrDefault(rec => rec.FoodName
           == model.FoodName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть продукт с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Foods.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Foods.Count > 0 ? source.Foods.Max(rec =>
               rec.Id) : 0;
                element = new Food { Id = maxId + 1 };
                source.Foods.Add(element);
            }
            element.FoodName = model.FoodName;
        }
        public void Delete(FoodBindingModel model)
        {
            Food element = source.Foods.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                source.Foods.Remove(element);
            }
            else
            { }
        }
        public List<FoodViewModel> Read(FoodBindingModel model)
        {
            return source.Foods
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new FoodViewModel
            {
                Id = rec.Id,
                FoodName = rec.FoodName
            })
            .ToList();
        }
    }
}