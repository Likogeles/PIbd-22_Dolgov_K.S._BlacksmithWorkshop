using System.ComponentModel;
using AbstractForgeContracts.Attributes;

namespace AbstractForgeContracts.ViewModels
{
    public class ImplementerViewModel
    {
        [Column(title: "Номер", width: 70)]
        public int Id { get; set; }

        [Column(title: "ФИО", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ImplementerFIO { get; set; }

        [Column(title: "Время на заказ", width: 100)]
        public int WorkingTime { get; set; }

        [Column(title: "Время на перерыв", width: 100)]
        public int PauseTime { get; set; }
    }
}
