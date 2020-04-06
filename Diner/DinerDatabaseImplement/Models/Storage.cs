using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DinerDatabaseImplement.Models
{
    public class Storage
    {
        public int Id { get; set; }
        [Required]
        public string StorageName { get; set; }
        public virtual List<StorageFood> StorageFoods { get; set; }
    }
}
