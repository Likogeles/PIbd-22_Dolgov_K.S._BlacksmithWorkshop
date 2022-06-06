using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.BusinessLogicsContracts;
using AbstractForgeContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AbstractForgeRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMessageInfoLogic _messageLogic;
        private readonly IClientLogic _clientLogic;
        public ClientController(IClientLogic clientLogic, IMessageInfoLogic messageLogic)
        {
            _clientLogic = clientLogic;
            _messageLogic = messageLogic;
        }
        [HttpGet]
        public ClientViewModel Login(string login, string password)
        {
            var list = _clientLogic.Read(new ClientBindingModel
            {
                Email = login,
                Password = password
            });
            return (list != null && list.Count > 0) ? list[0] : null;
        }
        [HttpPost]
        public void Register(ClientBindingModel model) => _clientLogic.CreateOrUpdate(model);

        [HttpPost]
        public void UpdateData(ClientBindingModel model) => _clientLogic.CreateOrUpdate(model);

        [HttpGet]
        public List<MessageInfoViewModel> GetClientsMessagesInfo(int clientId) => _messageLogic.Read(new MessageInfoBindingModel { ClientId = clientId });
    }

}
