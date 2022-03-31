using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.ViewModels;
using System.Collections.Generic;

namespace AbstractForgeContracts.BusinessLogicsContracts
{
    public interface IComponentLogic
    {
        List<ComponentViewModel> Read(ComponentBindingModel model);
        void CreateOrUpdate(ComponentBindingModel model);
        void Delete(ComponentBindingModel model);
    }
}
