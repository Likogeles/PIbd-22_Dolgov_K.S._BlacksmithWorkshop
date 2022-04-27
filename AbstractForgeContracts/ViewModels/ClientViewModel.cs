using System.ComponentModel;

namespace AbstractForgeContracts.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        [DisplayName("ФИО")]
        public string ClientFIO { get; set; }

        [DisplayName("Логин")]
        public string Email { get; set; }

        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
