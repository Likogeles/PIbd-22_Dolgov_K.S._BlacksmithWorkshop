using System.Collections.Generic;
using System.ComponentModel;

namespace AbstractForgeContracts.ViewModels
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    public class ManufactureViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название изделия")]
        public string ManufactureName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> ManufactureComponents { get; set; }
    }
}
