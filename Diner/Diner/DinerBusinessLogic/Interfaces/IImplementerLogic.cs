using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DinerBusinessLogic.Interfaces
{
    public interface IImplementerLogic
    {
        List<ImplementerViewModel> Read(ImplementerBindingModel model);
        void CreateOrUpdate(ImplementerBindingModel model);
        void Delete(ImplementerBindingModel model);
    }
}
