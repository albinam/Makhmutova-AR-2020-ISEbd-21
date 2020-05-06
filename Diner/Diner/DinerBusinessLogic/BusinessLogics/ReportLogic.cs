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
        private readonly IStorageLogic storageLogic;
        public ReportLogic(ISnackLogic SnackLogic, IFoodLogic FoodLogic,
       IOrderLogic orderLogic, IStorageLogic storageLogic)
        {
            this.SnackLogic = SnackLogic;
            this.FoodLogic = FoodLogic;
            this.orderLogic = orderLogic;
            this.storageLogic = storageLogic;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportSnackFoodViewModel> GetSnackFood()
        {
            var Snacks = SnackLogic.Read(null);
            var list = new List<ReportSnackFoodViewModel>();
            foreach (var snack in Snacks)
            {
                foreach (var sf in snack.SnackFoods)
                {
                    var record = new ReportSnackFoodViewModel
                    {
                        SnackName = snack.SnackName,
                        FoodName = sf.Value.Item1,
                        Count = sf.Value.Item2
                    };
                    list.Add(record);
                }
            }
            return list;
        }
        public List<ReportStorageFoodViewModel> GetStorageFoods()
        {
            var list = new List<ReportStorageFoodViewModel>();
            var storages = storageLogic.GetList();
            foreach (var storage in storages)
            {
                foreach (var sf in storage.StorageFoods)
                {
                    var record = new ReportStorageFoodViewModel
                    {
                        StorageName = storage.StorageName,
                        FoodName = sf.FoodName,
                        Count = sf.Count
                    };

                    list.Add(record);
                }
            }
            return list;
        }
        public List<IGrouping<DateTime, OrderViewModel>> GetOrders(ReportBindingModel model)
        {
            var list = orderLogic
            .Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .GroupBy(rec => rec.DateCreate.Date)
            .OrderBy(recG => recG.Key)
            .ToList();

            return list;
        }
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveSnacksToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список закусок",
                Snacks = SnackLogic.Read(null)
            });
        }
        /// <summary>
        /// Сохранение закусок с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrders(model)
            });
        }
        /// <summary>
        /// Сохранение закусок с продуктами в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveSnacksToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список закусок по продуктам",
                SnackFoods = GetSnackFood(),
            });
        }
        public void SaveStoragesToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список складов",
                Snacks = null,
                Storages = storageLogic.GetList()
            });
        }
        public void SaveStorageFoodsToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список продуктов в складах",
                Orders = null,
                Storages = storageLogic.GetList()
            }) ;
        }
        public void SaveFoodsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список продуктов",
                Foods = GetStorageFoods()
            });
        }
    }
}
