using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.Interfaces;
using DinerBusinessLogic.ViewModels;
using DinerFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerFileImplement.Implements
{
    public class StorageLogic : IStorageLogic
    {
        private readonly FileDataListSingleton source;
        public StorageLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<StorageViewModel> GetList()
        {
            return source.Storages.Select(rec => new StorageViewModel
            {
                Id = rec.Id,
                StorageName = rec.StorageName,
                StorageFoods = source.StorageFoods.Where(z => z.StorageId == rec.Id).Select(x => new StorageFoodViewModel
                {
                    Id = x.Id,
                    StorageId = x.StorageId,
                    FoodId = x.FoodId,
                    FoodName = source.Foods.FirstOrDefault(y => y.Id == x.FoodId)?.FoodName,
                    Count = x.Count
                }).ToList()
            })
                .ToList();
        }
        public StorageViewModel GetElement(int id)
        {
            var elem = source.Storages.FirstOrDefault(x => x.Id == id);
            if (elem == null)
            {
                throw new Exception("Элемент не найден");
            }
            else
            {
                return new StorageViewModel
                {
                    Id = id,
                    StorageName = elem.StorageName,
                    StorageFoods = source.StorageFoods.Where(z => z.StorageId == elem.Id).Select(x => new StorageFoodViewModel
                    {
                        Id = x.Id,
                        StorageId = x.StorageId,
                        FoodId = x.FoodId,
                        FoodName = source.Foods.FirstOrDefault(y => y.Id == x.FoodId)?.FoodName,
                        Count = x.Count
                    }).ToList()
                };
            }
        }

        public void AddElement(StorageBindingModel model)
        {

            var elem = source.Storages.FirstOrDefault(x => x.StorageName == model.StorageName);
            if (elem != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.Storages.Count > 0 ? source.Storages.Max(rec => rec.Id) : 0;
            source.Storages.Add(new Storage
            {
                Id = maxId + 1,
                StorageName = model.StorageName
            });
        }
        public void UpdElement(StorageBindingModel model)
        {
            var elem = source.Storages.FirstOrDefault(x => x.StorageName == model.StorageName && x.Id != model.Id);
            if (elem != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            var elemToUpdate = source.Storages.FirstOrDefault(x => x.Id == model.Id);
            if (elemToUpdate != null)
            {
                elemToUpdate.StorageName = model.StorageName;
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public void DelElement(int id)
        {
            var elem = source.Storages.FirstOrDefault(x => x.Id == id);
            if (elem != null)
            {
                source.Storages.Remove(elem);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public void FillStorage(StorageFoodBindingModel model)
        {
            var item = source.StorageFoods.FirstOrDefault(x => x.FoodId == model.FoodId
                    && x.StorageId == model.StorageId);

            if (item != null)
            {
                item.Count += model.Count;
            }
            else
            {
                int maxId = source.StorageFoods.Count > 0 ? source.StorageFoods.Max(rec => rec.Id) : 0;
                source.StorageFoods.Add(new StorageFood
                {
                    Id = maxId + 1,
                    StorageId = model.StorageId,
                    FoodId = model.FoodId,
                    Count = model.Count
                });
            }
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
    }
}