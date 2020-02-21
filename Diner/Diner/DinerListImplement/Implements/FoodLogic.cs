using System;
using System.Collections.Generic;
using System.Text;
using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.Interfaces;
using DinerBusinessLogic.ViewModels;
using DinerListImplement.Models;

namespace DinerListImplement.Implements
{
    public class FoodLogic : IFoodLogic
    {
        private readonly DataListSingleton source;
        public FoodLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(FoodBindingModel model)
        {
            Food tempFood = model.Id.HasValue ? null : new Food
            {
                Id = 1
            };
            foreach (var food in source.Foods)
            {
                if (food.FoodName == model.FoodName && food.Id !=
               model.Id)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
                if (!model.Id.HasValue && food.Id >= tempFood.Id)
                {
                    tempFood.Id = food.Id + 1;
                }
                else if (model.Id.HasValue && food.Id == model.Id)
                {
                    tempFood = food;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempFood == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempFood);
            }
            else
            {
                source.Foods.Add(CreateModel(model, tempFood));
            }
        }
        public void Delete(FoodBindingModel model)
        {
            for (int i = 0; i < source.Foods.Count; ++i)
            {
                if (source.Foods[i].Id == model.Id.Value)
                {
                    source.Foods.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        public List<FoodViewModel> Read(FoodBindingModel model)
        {
            List<FoodViewModel> result = new List<FoodViewModel>();
            foreach (var Food in source.Foods)
            {
                if (model != null)
                {
                    if (Food.Id == model.Id)
                    {
                        result.Add(CreateViewModel(Food));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(Food));
            }
            return result;
        }
        private Food CreateModel(FoodBindingModel model, Food food)
        {
            food.FoodName = model.FoodName;
            return food;
        }
        private FoodViewModel CreateViewModel(Food food)
        {
            return new FoodViewModel
            {
                Id = food.Id,
                FoodName = food.FoodName
            };
        }
    }
}