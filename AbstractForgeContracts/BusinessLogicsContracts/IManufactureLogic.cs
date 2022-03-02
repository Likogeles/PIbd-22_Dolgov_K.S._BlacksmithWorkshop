using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.ViewModels;
using System.Collections.Generic;

namespace AbstractForgeContracts.BusinessLogicsContracts
{
    public interface IManufactureLogic
    {
        List<ManufactureViewModel> Read(ManufactureBindingModel model);
        void CreateOrUpdate(ManufactureBindingModel model);
        void Delete(ManufactureBindingModel model);
    }
}
