using System;
using System.ComponentModel;

namespace AbstractForgeContracts.ViewModels
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class OrderViewModel
    {

        public int Id { get; set; }
        public int ManufactureId { get; set; }
        public int ClientId { get; set; }
        public int? ImplementerId { get; set; }

        [DisplayName("Клиент")]
        public string ClientFIO { get; set; }

        [DisplayName("Изделие")]
        public string ManufactureName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }
        [DisplayName("Статус")]
        public string Status { get; set; }
        [DisplayName("Исполнитель")]
        public string ImplementerFIO { get; set; }
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
