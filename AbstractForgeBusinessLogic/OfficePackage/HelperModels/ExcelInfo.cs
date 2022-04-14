using AbstractForgeContracts.ViewModels;
using System.Collections.Generic;

namespace AbstractForgeBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportManufactureComponentViewModel> ManufactureComponents { get; set; }
    }
}
