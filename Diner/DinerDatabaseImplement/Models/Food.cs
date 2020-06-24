using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DinerDatabaseImplement.Models
{
    public class Food
    {
        public int Id { get; set; }
        [Required]
        public string FoodName { get; set; }
        [ForeignKey("FoodId")]
        public virtual List<SnackFood> SnackFoods { get; set; }
    }
}
