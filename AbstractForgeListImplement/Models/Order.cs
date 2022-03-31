using AbstractForgeContracts.Enums;
using System;

namespace AbstractForgeListImplement.Models
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class Order
    {
        public int Id { get; set; }
        public int ManufactureId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
    }
}
