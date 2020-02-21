using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.Interfaces;
using DinerBusinessLogic.ViewModels;
using DinerListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerFileImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        private readonly FileDataListSingleton source;
        public OrderLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть продукт с таким названием");
            }
            if (model.Id.HasValue)
                element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Foods.Count > 0 ? source.Foods.Max(rec =>
               rec.Id) : 0;
                element = new Order { Id = maxId + 1 };
                source.Orders.Add(element);
            }
        }
        public void Delete(OrderBindingModel model)
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
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            return source.
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
