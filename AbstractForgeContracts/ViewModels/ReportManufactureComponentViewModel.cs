using System;
using System.Collections.Generic;

namespace AbstractForgeContracts.ViewModels
{
    public class ReportManufactureComponentViewModel
    {
        public string ComponentName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Manufactures { get; set; }
    }
}
