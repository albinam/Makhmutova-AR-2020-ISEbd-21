using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.Interfaces;
using DinerBusinessLogic.ViewModels;
using DinerRestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DinerRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorageLogic _storage;
        private readonly IFoodLogic _food;
        public StorageController(IStorageLogic storage, IFoodLogic food)
        {
            _storage = storage;
            _food = food;
        }
        [HttpGet]
        public List<StorageModel> GetStoragesList() => _storage.GetList()?.Select(rec => Convert(rec)).ToList();
        [HttpGet]
        public List<FoodViewModel> GetFoodsList() => _food.Read(null)?.ToList();
        [HttpGet]
        public StorageModel GetStorage(int StorageId) => Convert(_storage.GetElement(StorageId));
        [HttpPost]
        public void CreateOrUpdateStorage(StorageBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _storage.UpdElement(model);
            }
            else
            {
                _storage.AddElement(model);
            }
        }
        [HttpPost]
        public void DeleteStorage(StorageBindingModel model) => _storage.DelElement(model);
        [HttpPost]
        public void FillStorage(StorageFoodBindingModel model) => _storage.FillStorage(model);
        private StorageModel Convert(StorageViewModel model)
        {
            if (model == null) return null;

            return new StorageModel
            {
                Id = model.Id,
                StorageName = model.StorageName
            };
        }
    }
}
