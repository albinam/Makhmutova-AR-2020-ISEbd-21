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
            Order tempOrder = model.Id.HasValue ? null : new Order { Id = 1 };
            if (!model.Id.HasValue)
            {
                tempOrder.Id = source.Orders.FirstOrDefault(rec => rec.Id >= tempOrder.Id).Id + 1;
            }
            else
            {
                tempOrder = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            }
            if (model.Id.HasValue)
            {
                if (tempOrder == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempOrder);
            }
            else
            {
                source.Orders.Add(CreateModel(model, tempOrder));
            }
        }
        public void Delete(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id.Value);
            if (element != null)
            {
                source.Orders.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        } 
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            if (model != null)
            {
                result.Add(CreateViewModel(source.Orders.FirstOrDefault(rec => rec.Id == model.Id)));
            }
            else
            {
                result.AddRange(source.Orders.Select(rec => CreateViewModel(rec)));
            }
            return result;
        }
        private Order CreateModel(OrderBindingModel model, Order order)
        {
            Snack Snack = source.Snacks.Where(rec => rec.Id == model.SnackId).FirstOrDefault();
            if (Snack == null)
            {
                throw new Exception("Элемент не найден");
            }
            order.SnackId = model.SnackId;
            order.Count = model.Count;
            order.Sum = model.Count * Snack.Price;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }
        private OrderViewModel CreateViewModel(Order order)
        {
            Snack Snack = source.Snacks.Where(rec => rec.Id == order.SnackId).FirstOrDefault();
            if (Snack == null)
            {
                throw new Exception("Элемент не найден");
            }
            return new OrderViewModel
            {
                Id = order.Id,
                SnackId = order.SnackId,
                SnackName = Snack.SnackName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}