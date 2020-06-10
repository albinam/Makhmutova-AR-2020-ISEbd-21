using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.Enums;
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
    public class OrderLogic : IOrderLogic
    {
        public void CreateOrUpdate(OrderBindingModel model)
        {
            using (var context = new DinerDatabase())
            {
                Order element;
                if (model.Id.HasValue)
                {
                    element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Order();
                    context.Orders.Add(element);
                }
                element.SnackId = model.SnackId == 0 ? element.SnackId : model.SnackId;
                element.Count = model.Count;
                element.DateCreate = model.DateCreate;
                element.DateImplement = model.DateImplement;
                element.ClientId = model.ClientId.Value;
                element.ImplementerId = model.ImplementerId;
                element.Status = model.Status;
                element.Sum = model.Sum;
                context.SaveChanges();
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using (var context = new DinerDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            using (var context = new DinerDatabase())
            {
                return context.Orders.Where(rec => model == null
                    || rec.Id == model.Id && model.Id.HasValue
                    || model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo
                    || model.ClientId.HasValue && rec.ClientId == model.ClientId
                    || model.FreeOrders.HasValue && model.FreeOrders.Value && !rec.ImplementerId.HasValue
                    || model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId && rec.Status == OrderStatus.Выполняется
                    || model.NotEnoughFoodsOrders.HasValue && model.NotEnoughFoodsOrders.Value && rec.Status == OrderStatus.Требуются_продукты)               
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    ImplementerId = rec.ImplementerId,
                    SnackId = rec.SnackId,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                    Status = rec.Status,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    ClientFIO = rec.Client.ClientFIO,
                    ImplementerFIO = rec.ImplementerId.HasValue ?
                rec.Implementer.ImplementerFIO : string.Empty,
                    SnackName = rec.Snack.SnackName
                })
                .ToList();
            }
        }
    }
}