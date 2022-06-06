using System.Collections.Generic;
using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.ViewModels;

namespace AbstractForgeContracts.BusinessLogicsContracts
{
    public interface IImplementerLogic
    {
        List<ImplementerViewModel> Read(ImplementerBindingModel model);
        void CreateOrUpdate(ImplementerBindingModel model);
        void Delete(ImplementerBindingModel model);
    }
}
