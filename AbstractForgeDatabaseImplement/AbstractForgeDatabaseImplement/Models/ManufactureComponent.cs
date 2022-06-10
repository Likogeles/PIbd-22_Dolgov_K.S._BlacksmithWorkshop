using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractForgeDatabaseImplement.Models
{
    /// <summary>
    /// Сколько компонентов, требуется при изготовлении изделия
    /// </summary>
    public class ManufactureComponent
    {
        public int Id { get; set; }
        [Required]
        public int ManufactureId { get; set; }
        [Required]
        public int ComponentId { get; set; }
        public int Count { get; set; }
        public virtual Component Component { get; set; }
        public virtual Manufacture Manufacture { get; set; }
    }
}
