using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DinerBusinessLogic.ViewModels
{
    public class StorageFoodViewModel
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public int FoodId { get; set; } 
        [DisplayName("Название склада")]
        public string StorageName { get; set; }
        [DisplayName("Название продукта")]
        public string FoodName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
