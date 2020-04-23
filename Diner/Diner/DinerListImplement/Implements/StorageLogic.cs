using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.Interfaces;
using DinerBusinessLogic.ViewModels;
using DinerListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerListImplement.Implements
{
    public class StorageLogic : IStorageLogic
    {
        private readonly DataListSingleton source;
        public StorageLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(StorageBindingModel model)
        {
            Storage tempStorage = model.Id.HasValue ? null : new Storage { Id = 1, StorageName = model.StorageName };

            foreach (var Storage in source.Storages)
            {
                if (Storage.StorageName == model.StorageName && Storage.Id != model.Id)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
                if (!model.Id.HasValue && Storage.Id >= tempStorage.Id)
                {
                    tempStorage.Id = Storage.Id + 1;
                }
                else if (model.Id.HasValue && Storage.Id == model.Id)
                {
                    tempStorage = Storage;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempStorage == null)
                {
                    throw new Exception("Элемент не найден");
                }
                tempStorage.StorageName = model.StorageName;
            }
            else
            {
                source.Storages.Add(tempStorage);
            }
        }
        public void Delete(StorageBindingModel model)
        {
            for (int i = 0; i < source.StorageFoods.Count; ++i)
            {
                if (source.StorageFoods[i].StorageId == model.Id)
                {
                    source.StorageFoods.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == model.Id)
                {
                    source.Storages.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void FillStorage(StorageFoodBindingModel model)
        {
            int foundItemIndex = -1;
            for (int i = 0; i < source.StorageFoods.Count; ++i)
            {
                if (source.StorageFoods[i].FoodId == model.FoodId
                    && source.StorageFoods[i].StorageId == model.StorageId)
                {
                    foundItemIndex = i;
                    break;
                }
            }
            if (foundItemIndex != -1)
            {
                source.StorageFoods[foundItemIndex].Count =
                    source.StorageFoods[foundItemIndex].Count + model.Count;
            }
            else
            {
                int maxId = 0;
                for (int i = 0; i < source.StorageFoods.Count; ++i)
                {
                    if (source.StorageFoods[i].Id > maxId)
                    {
                        maxId = source.StorageFoods[i].Id;
                    }
                }
                source.StorageFoods.Add(new StorageFood
                {
                    Id = maxId + 1,
                    StorageId = model.StorageId,
                    FoodId = model.FoodId,
                    Count = model.Count
                });
            }
        }
        public List<StorageViewModel> Read(StorageBindingModel model)
        {
            List<StorageViewModel> result = new List<StorageViewModel>();

            foreach (var Storage in source.Storages)
            {
                if (model != null)
                {
                    if (Storage.Id == model.Id)
                    {
                        result.Add(CreateViewModel(Storage));
                        break;
                    }

                    continue;
                }
                result.Add(CreateViewModel(Storage));
            }
            return result;
        }
        public void RemoveFromStorage(int SnackId, int SnacksCount)
        {
            var SnackFoods = source.SnackFoods.Where(x => x.SnackId == SnackId);
            if (SnackFoods.Count() == 0) return;
            foreach (var elem in SnackFoods)
            {
                int left = elem.Count * SnacksCount;
                var storageFoods = source.StorageFoods.FindAll(x => x.FoodId == elem.FoodId);
                foreach (var rec in storageFoods)
                {
                    int toRemove = left > rec.Count ? rec.Count : left;
                    rec.Count -= toRemove;
                    left -= toRemove;
                    if (left == 0) break;
                }
            }
            return;
        }
        private StorageViewModel CreateViewModel(Storage Storage)
        {
            Dictionary<int, (string, int)> StorageFoods = new Dictionary<int, (string, int)>();
            foreach (var sf in source.StorageFoods)
            {
                if (sf.StorageId == Storage.Id)
                {
                    string FoodName = string.Empty;
                    foreach (var Food in source.Foods)
                    {
                        if (sf.FoodId == Food.Id)
                        {
                            FoodName = Food.FoodName;
                            break;
                        }
                    }
                    StorageFoods.Add(sf.FoodId, (FoodName, sf.Count));
                }
            }
            return new StorageViewModel
            {
                Id = Storage.Id,
                StorageName = Storage.StorageName,
                StorageFoods = StorageFoods
            };
        }
    }
}