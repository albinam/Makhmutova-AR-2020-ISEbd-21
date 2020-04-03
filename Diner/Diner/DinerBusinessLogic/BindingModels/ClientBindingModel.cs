using System;
using System.Collections.Generic;
using System.Text;

namespace DinerBusinessLogic.BindingModels
{
    public class ClientBindingModel
    {
        public int? Id { get; set; }
        public string ClientFIO { get; set; }
        public string Email{ get; set; }
        public string Password { get; set; }
    }
}
