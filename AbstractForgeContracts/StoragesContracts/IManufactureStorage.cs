using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.ViewModels;
using System.Collections.Generic;

namespace AbstractForgeContracts.StoragesContracts
{
    public interface IManufactureStorage
    {
        List<ManufactureViewModel> GetFullList();
        List<ManufactureViewModel> GetFilteredList(ManufactureBindingModel model);
        ManufactureViewModel GetElement(ManufactureBindingModel model);
        void Insert(ManufactureBindingModel model);
        void Update(ManufactureBindingModel model);
        void Delete(ManufactureBindingModel model);

    }
}
