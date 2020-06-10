using DinerBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinerRestApi.Models
{
    public class StorageModel
    {
        public int Id { get; set; }
        public string StorageName { get; set; }
        public List<StorageFoodViewModel> StorageFoods { get; set; }
    }
}
