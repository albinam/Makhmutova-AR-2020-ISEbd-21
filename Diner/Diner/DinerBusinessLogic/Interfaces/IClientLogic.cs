using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DinerBusinessLogic.Interfaces
{
    public interface IClientLogic
    {
        List<ClientViewModel> Read(ClientBindingModel model);
        void CreateOrUpdate(ClientBindingModel model);
        void Delete(ClientBindingModel model);
    }
}
