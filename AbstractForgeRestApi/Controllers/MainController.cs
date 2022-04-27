using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.BusinessLogicsContracts;
using AbstractForgeContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BlacksmithWorkshopRestApi.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IManufactureLogic _manufacture;
        public MainController(IOrderLogic order, IManufactureLogic manufacture)
        {
            _order = order;
            _manufacture = manufacture;
        }
        [HttpGet]
        public List<ManufactureViewModel> GetManufactureList() => _manufacture.Read(null)?.ToList();
        [HttpGet]
        public ManufactureViewModel GetManufacture(int manufactureId) => _manufacture.Read(new ManufactureBindingModel
        { Id = manufactureId })?[0];
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel
        { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) =>
       _order.CreateOrder(model);
    }
}
