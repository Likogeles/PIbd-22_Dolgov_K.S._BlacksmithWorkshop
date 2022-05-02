using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractForgeDatabaseImplement.Models
{
    public class Manufacture
    {
        public int Id { get; set; }
        [Required]
        public string ManufactureName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("DBManufactureId")]
        public virtual List<Order> Orders { get; set; }
        [ForeignKey("DBManufactureId")]
        public virtual List<ManufactureComponent> ManufactureComponents { get; set; }
    }
}
