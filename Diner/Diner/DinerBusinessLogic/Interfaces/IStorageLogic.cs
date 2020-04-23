using System;
using System.Collections.Generic;
using System.Text;
using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.ViewModels;

namespace DinerBusinessLogic.Interfaces
{
    public interface IStorageLogic
    {
        List<StorageViewModel> Read(StorageBindingModel model);
        void CreateOrUpdate(StorageBindingModel model);
        void Delete(StorageBindingModel model);
        void FillStorage(StorageFoodBindingModel model);
        void RemoveFromStorage(int snackId, int snacksCount);
    }
}
