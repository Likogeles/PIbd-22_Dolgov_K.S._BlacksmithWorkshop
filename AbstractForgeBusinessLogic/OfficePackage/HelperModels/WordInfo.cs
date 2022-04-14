using AbstractForgeContracts.ViewModels;
using System.Collections.Generic;


namespace AbstractForgeBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ManufactureViewModel> Manufactures { get; set; }
    }
}
