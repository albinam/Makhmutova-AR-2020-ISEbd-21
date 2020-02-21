using System;
using System.Collections.Generic;
using System.Text;

namespace DinerBusinessLogic.BindingModels
{
    public class SnackFoodBindingModel
    {
        public int Id { get; set; }
        public int SnackId { get; set; }
        public int FoodId { get; set; }
        public int Count { get; set; }
    }
}
