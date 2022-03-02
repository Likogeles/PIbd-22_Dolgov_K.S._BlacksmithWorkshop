using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractForgeListImplement.Models
{
    /// <summary>
    /// Изделие, изготавливаемое в кузнечной мастерской
    /// </summary>
    public class Manufacture
    {
        public int Id { get; set; }
        public string ManufactureName { get; set; }
        public decimal Price { get; set; }

        public Dictionary<int, int> ManufactureComponents { get; set; }
    }
}
