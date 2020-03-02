using System;
using System.Collections.Generic;
using System.Text;

namespace DinerBusinessLogic.BindingModels
{
    public class StorageFoodBindingModel
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public int FoodId { get; set; }
        public int Count { get; set; }
    }
}
