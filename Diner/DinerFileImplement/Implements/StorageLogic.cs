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
        public void CreateOrUpdate(StorageBindingModel model)
        {
            Storage element = source.Storages.FirstOrDefault(rec => rec.StorageName == model.StorageName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Storages.FirstOrDefault(rec => rec.Id == model.Id);

                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Storages.Count > 0 ? source.Storages.Max(rec => rec.Id) : 0;
                element = new Storage { Id = maxId + 1 };
                source.Storages.Add(element);
            }
            element.StorageName = model.StorageName;
        }
        public void Delete(StorageBindingModel model)
        {
            source.StorageFoods.RemoveAll(rec => rec.StorageId == model.Id);
            Storage element = source.Storages.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Storages.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public void AddFood(StorageFoodBindingModel model)
        {
            Storage Storage = source.Storages.FirstOrDefault(rec => rec.Id == model.StorageId);
            if (Storage == null)
            {
                throw new Exception("Склад не найден");
            }
            Food Food = source.Foods.FirstOrDefault(rec => rec.Id == model.FoodId);

            if (Food == null)
            {
                throw new Exception("Компонент не найден");
            }
            StorageFood element = source.StorageFoods
                        .FirstOrDefault(rec => rec.StorageId == model.StorageId && rec.FoodId == model.FoodId);
            if (element != null)
            {
                element.Count += model.Count;
                return;
            }
            source.StorageFoods.Add(new StorageFood
            {
                Id = source.StorageFoods.Count > 0 ? source.StorageFoods.Max(rec => rec.Id) + 1 : 0,
                StorageId = model.StorageId,
                FoodId = model.FoodId,
                Count = model.Count
            });
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
        public List<StorageViewModel> Read(StorageBindingModel model)
        {
            return source.Storages
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new StorageViewModel
            {
                Id = rec.Id,
                StorageName = rec.StorageName,
                StorageFoods = source.StorageFoods
                                    .Where(recSF => recSF.StorageId == rec.Id)
                                    .ToDictionary(
                                        recSF => recSF.FoodId,
                                        recSF => (
                                            source.Foods.FirstOrDefault(recF => recF.Id == recSF.FoodId)?.FoodName, recSF.Count
                                            )
                                        )
            })
            .ToList();
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