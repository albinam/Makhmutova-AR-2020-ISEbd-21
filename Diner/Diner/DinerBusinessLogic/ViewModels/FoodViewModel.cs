using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DinerBusinessLogic.ViewModels
{
    public class FoodViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название продукта")]
        public string FoodName { get; set; }
    }
}
