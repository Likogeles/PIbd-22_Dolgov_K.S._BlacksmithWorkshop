using System.Collections.Generic;
using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.ViewModels;

namespace AbstractForgeContracts.StoragesContracts
{
    public interface IClientStorage
    {
        List<ClientViewModel> GetFullList();

        List<ClientViewModel> GetFilteredList(ClientBindingModel model);

        ClientViewModel GetElement(ClientBindingModel model);

        void Insert(ClientBindingModel model);

        void Update(ClientBindingModel model);

        void Delete(ClientBindingModel model);
    }
}
