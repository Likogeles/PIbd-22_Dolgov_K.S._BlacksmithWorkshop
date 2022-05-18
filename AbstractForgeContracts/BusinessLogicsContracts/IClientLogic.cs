using System.Collections.Generic;
using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.ViewModels;

namespace AbstractForgeContracts.BusinessLogicsContracts
{
    public interface IClientLogic
    {
        List<ClientViewModel> Read(ClientBindingModel model);
        void CreateOrUpdate(ClientBindingModel model);
        void Delete(ClientBindingModel model);
    }
}
