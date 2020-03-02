using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.Interfaces;
using DinerBusinessLogic.ViewModels;
using DinerListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DinerListImplement.Implements
{
    public class SnackLogic : ISnackLogic
    {
        private readonly DataListSingleton source;
        public SnackLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(SnackBindingModel model)
        {
            Snack tempSnack = model.Id.HasValue ? null : new Snack { Id = 1 };
            foreach (var snack in source.Snacks)
            {
                if (snack.SnackName == model.SnackName && snack.Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
                if (!model.Id.HasValue && snack.Id >= tempSnack.Id)
                {
                    tempSnack.Id = snack.Id + 1;
                }
                else if (model.Id.HasValue && snack.Id == model.Id)
                {
                    tempSnack = snack;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempSnack == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempSnack);
            }
            else
            {
                source.Snacks.Add(CreateModel(model, tempSnack));
            }
        }
        public void Delete(SnackBindingModel model)
        {
            for (int i = 0; i < source.SnackFoods.Count; ++i)
            {
                if (source.SnackFoods[i].SnackId == model.Id)
                {
                    source.SnackFoods.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Snacks.Count; ++i)
            {
                if (source.Snacks[i].Id == model.Id)
                {
                    source.Snacks.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Snack CreateModel(SnackBindingModel model, Snack snack)
        {
            snack.SnackName = model.SnackName;
            snack.Price = model.Price;
            int maxSFId = 0;
            for (int i = 0; i < source.SnackFoods.Count; ++i)
            {
                if (source.SnackFoods[i].Id > maxSFId)
                {
                    maxSFId = source.SnackFoods[i].Id;
                }
                if (source.SnackFoods[i].SnackId == snack.Id)
                {
                    if
                    (model.SnackFoods.ContainsKey(source.SnackFoods[i].FoodId))
                    {
                        source.SnackFoods[i].Count =
                        model.SnackFoods[source.SnackFoods[i].FoodId].Item2;
                        model.SnackFoods.Remove(source.SnackFoods[i].FoodId);
                    }
                    else
                    {
                        source.SnackFoods.RemoveAt(i--);
                    }
                }
            }
            foreach (var sf in model.SnackFoods)
            {
                source.SnackFoods.Add(new SnackFood
                {
                    Id = ++maxSFId,
                    SnackId = snack.Id,
                    FoodId = sf.Key,
                    Count = sf.Value.Item2
                });
            }
            return snack;
        }
        public List<SnackViewModel> Read(SnackBindingModel model)
        {
            List<SnackViewModel> result = new List<SnackViewModel>();
            foreach (var food in source.Snacks)
            {
                if (model != null)
                {
                    if (food.Id == model.Id)
                    {
                        result.Add(CreateViewModel(food));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(food));
            }
            return result;
        }
        private SnackViewModel CreateViewModel(Snack Snack)
        {
            Dictionary<int, (string, int)> SnackFoods = new Dictionary<int, (string, int)>();
            foreach (var sf in source.SnackFoods)
            {
                if (sf.SnackId == Snack.Id)
                {
                    string FoodName = string.Empty;
                    foreach (var Food in source.Foods)
                    {
                        if (sf.FoodId == Food.Id)
                        {
                            FoodName = Food.FoodName;
                            break;
                        }
                    }
                    SnackFoods.Add(sf.FoodId, (FoodName, sf.Count));
                }
            }
            return new SnackViewModel
            {
                Id = Snack.Id,
                SnackName = Snack.SnackName,
                Price = Snack.Price,
                SnackFoods = SnackFoods
            };
        }
    }
}