using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.Enums;
using DinerBusinessLogic.Interfaces;
using DinerBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DinerBusinessLogic.BusinessLogics
{
    public class WorkModeling
    {
        private readonly IImplementerLogic implementerLogic;
        private readonly IOrderLogic orderLogic;
        private readonly MainLogic mainLogic;
        private readonly Random rnd;
        public WorkModeling(IImplementerLogic implementerLogic, IOrderLogic orderLogic, MainLogic mainLogic)
        {
            this.implementerLogic = implementerLogic;
            this.orderLogic = orderLogic;
            this.mainLogic = mainLogic;
            rnd = new Random(1000);
        }
        public void DoWork()
        {
            var implementers = implementerLogic.Read(null);
            var orders = orderLogic.Read(new OrderBindingModel { FreeOrders = true });
            foreach (var implementer in implementers)
            {
                WorkerWorkAsync(implementer, orders);
            }
        }
        /// <summary>
        /// Иммитация работы исполнителя
        /// </summary>
        /// <param name="implementer"></param>
        /// <param name="orders"></param>
        private async void WorkerWorkAsync(ImplementerViewModel implementer, List<OrderViewModel> orders)
        {
            // ищем заказы, которые уже в работе (вдруг исполнителя прервали)
            var runOrders = await Task.Run(() => orderLogic.Read(new OrderBindingModel
            {
                ImplementerId = implementer.Id
            }));
            foreach (var order in runOrders)
            {
                // делаем работу заново
                Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count);
                mainLogic.FinishOrder(new ChangeStatusBindingModel
                {
                    OrderId = order.Id
                });
                // отдыхаем
                Thread.Sleep(implementer.PauseTime);
            }
            var notEnoughMaterialsOrders = orders
                .Where(x => x.Status == OrderStatus.Требуются_продукты)
                .Select(x => x)
                .ToList();
            orders.RemoveAll(x => notEnoughMaterialsOrders.Contains(x));
            this.DoWork(implementer, notEnoughMaterialsOrders);

            await Task.Run(() =>
            {
                this.DoWork(implementer, orders);
            });
        }
        private void DoWork(ImplementerViewModel implementer, List<OrderViewModel> orders)
        {
            foreach (var order in orders)
            {
                // пытаемся назначить заказ на исполнителя
                try
                {
                    mainLogic.TakeOrderInWork(new ChangeStatusBindingModel
                    {
                        OrderId = order.Id,
                        ImplementerId = implementer.Id
                    });
                    Boolean isNotEnoughMaterials = orderLogic.Read(new OrderBindingModel
                    {
                        Id = order.Id
                    }).FirstOrDefault().Status == OrderStatus.Требуются_продукты;
                    if (isNotEnoughMaterials)
                    {
                        continue;
                    }
                    // делаем работу
                    Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count);
                    mainLogic.FinishOrder(new ChangeStatusBindingModel
                    {
                        OrderId = order.Id,
                        ImplementerId = implementer.Id
                    });
                    // отдыхаем
                    Thread.Sleep(implementer.PauseTime);
                }
                catch (Exception) { }
            }
        }
    }
}