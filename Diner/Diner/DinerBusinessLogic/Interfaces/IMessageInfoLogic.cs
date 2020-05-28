using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DinerBusinessLogic.Interfaces
{
    public interface IMessageInfoLogic
    {
        List<MessageInfoViewModel> Read(MessageInfoBindingModel model);
        void Create(MessageInfoBindingModel model);
    }
}
