using System;
using System.Collections.Generic;
using System.Text;

namespace DinerBusinessLogic.ViewModels
{
    public class ReportSnackFoodViewModel
    {
        public string FoodName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Snacks { get; set; }
    }
}
