using System.Collections.Generic;

namespace AbstractForgeContracts.BindingModels
{
    /// <summary>
    /// Изделие, изготавливаемое в кузнечной мастерской
    /// </summary>
    public class ManufactureBindingModel
    {
        public int? Id { get; set; }
        public string ManufactureName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> ManufactureComponents { get; set; }
    }
}
