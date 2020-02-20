using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DinerBusinessLogic.ViewModels
{
    public class SnackFoodViewModel
    {
        public int Id { get; set; }
        public int SnackId { get; set; }
        public int FoodId { get; set; }
        [DisplayName("Продукт")]
        public string FoodName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
