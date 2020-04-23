using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DinerBusinessLogic.ViewModels
{
    public class StorageViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название склада")]
        public string StorageName { get; set; }
        public Dictionary<int, (string, int)> StorageFoods { get; set; }
    }
}
