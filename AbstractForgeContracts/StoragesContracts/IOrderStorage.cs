using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.ViewModels;
using System.Collections.Generic;

namespace AbstractForgeContracts.StoragesContracts
{
    public interface IOrderStorage
    {
        List<OrderViewModel> GetFullList();
        List<OrderViewModel> GetFilteredList(OrderBindingModel model);
        OrderViewModel GetElement(OrderBindingModel model);
        void Insert(OrderBindingModel model);
        void Update(OrderBindingModel model);
        void Delete(OrderBindingModel model);

    }
}
