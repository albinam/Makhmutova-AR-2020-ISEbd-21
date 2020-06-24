using DinerBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace DinerBusinessLogic.ViewModels
{
    [DataContract]
    public class SnackViewModel : BaseViewModel
    {
        [Column(title: "Название закуски", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string SnackName { get; set; }
        [Column(title: "Цена", width: 50)]
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> SnackFoods { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "SnackName",
            "Price"
        };
    }
}