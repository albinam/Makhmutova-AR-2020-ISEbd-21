using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.HelperModels;
using DinerBusinessLogic.Interfaces;
using DinerBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IFoodLogic FoodLogic;
        private readonly ISnackLogic SnackLogic;
        private readonly IOrderLogic orderLogic;
        public ReportLogic(ISnackLogic SnackLogic, IFoodLogic FoodLogic,
       IOrderLogic orderLogic)
        {
            this.SnackLogic = SnackLogic;
            this.FoodLogic = FoodLogic;
            this.orderLogic = orderLogic;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportSnackFoodViewModel> GetSnackFood()
        {
            var Foods = FoodLogic.Read(null);
            var Snacks = SnackLogic.Read(null);
            var list = new List<ReportSnackFoodViewModel>();
            foreach (var Food in Foods)
            {
                var record = new ReportSnackFoodViewModel
                {
                    FoodName = Food.FoodName,
                    Snacks = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var Snack in Snacks)
                {
                    if (Snack.SnackFoods.ContainsKey(Food.Id))
                    {
                        record.Snacks.Add(new Tuple<string, int>(Snack.SnackName,
                       Snack.SnackFoods[Food.Id].Item2));
                        record.TotalCount +=
                       Snack.SnackFoods[Food.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return orderLogic.Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                SnackName = x.SnackName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
            .ToList();
        }
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveFoodsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список продуктов",
                Foods = FoodLogic.Read(null)
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveSnackFoodToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список продуктов",
                SnackFoods = GetSnackFood()
            });
        }
        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
    }
}