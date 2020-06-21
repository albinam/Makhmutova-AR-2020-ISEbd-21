using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.Enums;
using DinerBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DinerBusinessLogic
{
    public class MainLogic
    {
        private readonly IOrderLogic orderLogic;
        private readonly IStorageLogic storageLogic;
        private readonly object locker = new object();
        public MainLogic(IOrderLogic orderLogic, IStorageLogic storageLogic)
        {
            this.orderLogic = orderLogic;
            this.storageLogic = storageLogic;
        }
        public void CreateOrder(CreateOrderBindingModel model)
        {
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                SnackId = model.SnackId,
                ClientId = model.ClientId,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Принят
            });
        }
        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            lock (locker)
            {
                var order = orderLogic.Read(new OrderBindingModel
                {
                    Id = model.OrderId
                })?[0];
                if (order == null)
                {
                    throw new Exception("Не найден заказ");
                }
                if (order.Status != OrderStatus.Принят && order.Status != OrderStatus.Требуются_продукты)
                {
                    throw new Exception("Заказ не в статусе \"Принят\"или \"Требуются продукты\"");
                }
                if (order.ImplementerId.HasValue)
                {
                    throw new Exception("У заказа уже есть исполнитель");
                }
                var orderModel = new OrderBindingModel
                {
                    Id = order.Id,
                    SnackId = order.SnackId,
                    Count = order.Count,
                    Sum = order.Sum,
                    ClientId = order.ClientId,
                    DateCreate = order.DateCreate
                };
                try
                {
                    storageLogic.RemoveFromStorage(order.SnackId,order.Count);
                    orderModel.DateImplement = DateTime.Now;
                    orderModel.Status = OrderStatus.Выполняется;
                    orderModel.ImplementerId = model.ImplementerId;
                }
                catch
                {
                    orderModel.Status = OrderStatus.Требуются_продукты;
                }
                orderLogic.CreateOrUpdate(orderModel);             
            }
        }
        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                SnackId = order.SnackId,
                Count = order.Count,
                Sum = order.Sum,
                ImplementerId = order.ImplementerId,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Готов
            });
        }
        public void PayOrder(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                SnackId = order.SnackId,
                Count = order.Count,
                Sum = order.Sum,
                ImplementerId = order.ImplementerId,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Оплачен
            });
        }
        public void FillStorage(StorageFoodBindingModel model)
        {
            storageLogic.FillStorage(model);
        }
    }
}