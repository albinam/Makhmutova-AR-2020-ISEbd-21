using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DinerDatabaseImplement.Models
{
    public class Snack
    {
        public int Id { get; set; }
        [Required]
        public string SnackName { get; set; }
        [Required]
        public decimal Price { get; set; }
        public virtual List<SnackFood> SnackFoods { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
