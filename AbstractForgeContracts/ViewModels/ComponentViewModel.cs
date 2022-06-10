using System.ComponentModel;
using AbstractForgeContracts.Attributes;

namespace AbstractForgeContracts.ViewModels
{
    /// <summary>
    /// Компонент, требуемый для изготовления изделия
    /// </summary>
    public class ComponentViewModel
    {
        [Column(title: "Номер", width: 80)]
        public int Id { get; set; }

        [Column(title: "Название компонента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ComponentName { get; set; }
    }

}
