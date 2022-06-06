using System.Collections.Generic;
using System.ComponentModel;
using AbstractForgeContracts.Attributes;

namespace AbstractForgeContracts.ViewModels
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    public class ManufactureViewModel
    {
        [Column(title: "Номер", width: 80)]
        public int Id { get; set; }
        [Column(title: "Название изделия", width: 180)]
        public string ManufactureName { get; set; }
        [Column(title: "Цена", width: 60)]
        public decimal Price { get; set; }
        [Column(title: "Компоненты", gridViewAutoSize: GridViewAutoSize.Fill)]
        public Dictionary<int, (string, int)> ManufactureComponents { get; set; }
        public string GetComponents()
        {
            string stringComponents = string.Empty;
            if (ManufactureComponents != null)
            {
                foreach (var component in ManufactureComponents)
                {
                    stringComponents += component.Value.Item1 + " = " + component.Value.Item2 + " шт.; ";
                }
            }
            return stringComponents;
        }
    }
}
