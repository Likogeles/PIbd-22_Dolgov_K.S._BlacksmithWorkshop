using System;
using System.Collections.Generic;

namespace AbstractForgeContracts.ViewModels
{
    public class ReportManufactureComponentViewModel
    {
        public string ManufactureName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Components { get; set; }
    }
}
