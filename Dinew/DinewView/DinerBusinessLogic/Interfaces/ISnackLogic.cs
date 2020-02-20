using DinerBusinessLogic.ViewModels;
using DinerBusinessLogic.BindingModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DinerBusinessLogic.Interfaces
{
    public interface ISnackLogic
    {
        List<SnackViewModel> Read(SnackBindingModel model);
        void CreateOrUpdate(SnackBindingModel model);
        void Delete(SnackBindingModel model);
    }
}