﻿using DinerBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DinerBusinessLogic.HelperModels
{
    class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportSnackFoodViewModel> SnackFoods { get; set; }
        public List<ReportStorageFoodViewModel> StorageFoods { get; set; }
    }
}
