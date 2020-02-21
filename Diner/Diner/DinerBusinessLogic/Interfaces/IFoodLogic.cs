using DinerBusinessLogic.ViewModels;
using DinerBusinessLogic.BindingModels;
using System;
using System.Collections.Generic;
using System.Text;


namespace DinerBusinessLogic.Interfaces
{
    public interface IFoodLogic
    {
        List<FoodViewModel> Read(FoodBindingModel model);
        void CreateOrUpdate(FoodBindingModel model);
        void Delete(FoodBindingModel model);
    }
}
