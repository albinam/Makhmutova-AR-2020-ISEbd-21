using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinerBusinessLogic;
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
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly ISnackLogic _snack;
        private readonly MainLogic _main;
        public MainController(IOrderLogic order, ISnackLogic snack, MainLogic main)
        {
            _order = order;
            _snack = snack;
            _main = main;
        }
        [HttpGet]
        public List<SnackModel>  GetSnackList() => _snack.Read(null)?.Select(rec =>
       Convert(rec)).ToList();
        [HttpGet]
        public SnackModel GetSnack(int SnackId) => Convert(_snack.Read(new
       SnackBindingModel
        { Id = SnackId })?[0]);
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new
       OrderBindingModel
        { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) =>
       _main.CreateOrder(model);
        private SnackModel Convert(SnackViewModel model)
        {
            if (model == null) return null;
            return new SnackModel
            {
                Id = model.Id,
                SnackName = model.SnackName,
                Price = model.Price
            };
        }
    }
}