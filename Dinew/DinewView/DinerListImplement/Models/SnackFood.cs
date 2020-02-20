using System;
using System.Collections.Generic;
using System.Text;

namespace DinerListImplement.Models
{
    public class SnackFood
    {
        public int Id { get; set; }
        public int SnackId { get; set; }
        public int FoodId { get; set; }
        public int Count { get; set; }
    }
}
