using System.ComponentModel;

namespace AbstractForgeContracts.ViewModels
{
    public class ImplementerViewModel
    {
        public int Id { get; set; }

        [DisplayName("ФИО")]
        public string ImplementerFIO { get; set; }

        [DisplayName("Время на заказ")]
        public int WorkingTime { get; set; }

        [DisplayName("Время на перерыв")]
        public int PauseTime { get; set; }
    }
}
