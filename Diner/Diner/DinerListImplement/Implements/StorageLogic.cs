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
        public List<StorageViewModel> GetList()
        {
            List<StorageViewModel> result = new List<StorageViewModel>();
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                List<StorageFoodViewModel> StorageFoods = new
    List<StorageFoodViewModel>();
                for (int j = 0; j < source.StorageFoods.Count; ++j)
                {
                    if (source.StorageFoods[j].StorageId == source.Storages[i].Id)
                    {
                        string FoodName = string.Empty;
                        for (int k = 0; k < source.Foods.Count; ++k)
                        {
                            if (source.StorageFoods[j].FoodId ==
                           source.Foods[k].Id)
                            {
                                FoodName = source.Foods[k].FoodName;
                                break;
                            }
                        }
                        StorageFoods.Add(new StorageFoodViewModel
                        {
                            Id = source.StorageFoods[j].Id,
                            StorageId = source.StorageFoods[j].StorageId,
                            FoodId = source.StorageFoods[j].FoodId,
                            FoodName = FoodName,
                            Count = source.StorageFoods[j].Count
                        });
                    }
                }
                result.Add(new StorageViewModel
                {
                    Id = source.Storages[i].Id,
                    StorageName = source.Storages[i].StorageName,
                    StorageFoods = StorageFoods
                });
            }
            return result;
        }
        public StorageViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                List<StorageFoodViewModel> StorageFoods = new
    List<StorageFoodViewModel>();
                for (int j = 0; j < source.StorageFoods.Count; ++j)
                {
                    if (source.StorageFoods[j].StorageId == source.Storages[i].Id)
                    {
                        string FoodName = string.Empty;
                        for (int k = 0; k < source.Foods.Count; ++k)
                        {
                            if (source.StorageFoods[j].FoodId ==
                           source.Foods[k].Id)
                            {
                                FoodName = source.Foods[k].FoodName;
                                break;
                            }
                        }
                        StorageFoods.Add(new StorageFoodViewModel
                        {
                            Id = source.StorageFoods[j].Id,
                            StorageId = source.StorageFoods[j].StorageId,
                            FoodId = source.StorageFoods[j].FoodId,
                            FoodName = FoodName,
                            Count = source.StorageFoods[j].Count
                        });
                    }
                }
                if (source.Storages[i].Id == id)
                {
                    return new StorageViewModel
                    {
                        Id = source.Storages[i].Id,
                        StorageName = source.Storages[i].StorageName,
                        StorageFoods = StorageFoods
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(StorageBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id > maxId)
                {
                    maxId = source.Storages[i].Id;
                }
                if (source.Storages[i].StorageName == model.StorageName)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            source.Storages.Add(new Storage
            {
                Id = maxId + 1,
                StorageName = model.StorageName
            });
        }
        public void UpdElement(StorageBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Storages[i].StorageName == model.StorageName &&
                source.Storages[i].Id != model.Id)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Storages[index].StorageName = model.StorageName;
        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.StorageFoods.Count; ++i)
            {
                if (source.StorageFoods[i].StorageId == id)
                {
                    source.StorageFoods.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == id)
                {
                    source.Storages.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        public bool CheckFoodsAvailability(int SnackId, int SnacksCount)
        {
            bool result = true;
            var SnackFoods = source.SnackFoods.Where(x => x.SnackId == SnackId);
            if (SnackFoods.Count() == 0) return false;
            foreach (var elem in SnackFoods)
            {
                int count = 0;
                var storageFoods = source.StorageFoods.FindAll(x => x.FoodId == elem.FoodId);
                foreach (var rec in storageFoods)
                {
                    count += rec.Count;
                }
                if (count < elem.Count * SnacksCount) result = false;
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
    }
}
