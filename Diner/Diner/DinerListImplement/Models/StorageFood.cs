using System;
using System.Collections.Generic;
using System.Text;

namespace DinerListImplement.Models
{
    public class StorageFood
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public int FoodId { get; set; }
        public int Count { get; set; }
    }
}
