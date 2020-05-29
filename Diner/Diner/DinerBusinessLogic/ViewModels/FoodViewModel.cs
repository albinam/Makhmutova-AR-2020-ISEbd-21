using DinerBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DinerBusinessLogic.ViewModels
{
    public class FoodViewModel : BaseViewModel
    {
        [Column(title: "Продукт", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string FoodName { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "FoodName"
        };
    }
}