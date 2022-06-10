using System.ComponentModel;
using AbstractForgeContracts.Attributes;

namespace AbstractForgeContracts.ViewModels
{
    public class ClientViewModel
    {
        [Column(title: "Номер", width: 80)]
        public int Id { get; set; }

        [Column(title: "ФИО", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ClientFIO { get; set; }

        [Column(title: "Логин", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Email { get; set; }

        [Column(title: "Пароль", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Password { get; set; }
    }
}
