using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DinerBusinessLogic.BindingModels
{
    [DataContract]
    public class CreateOrderBindingModel
    {
        [DataMember]
        public int SnackId { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
    }
}
