using System;
using System.Collections.Generic;
using System.Text;
using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.ViewModels;

namespace DinerBusinessLogic.Interfaces
{
    public interface IStorageLogic
    {
        List<StorageViewModel> GetList();
        StorageViewModel GetElement(int id);
        void AddElement(StorageBindingModel model);
        void UpdElement(StorageBindingModel model);
        void DelElement(StorageBindingModel model);
        void FillStorage(StorageFoodBindingModel model);
        void RemoveFromStorage(int snackId, int snacksCount);
    }
}
