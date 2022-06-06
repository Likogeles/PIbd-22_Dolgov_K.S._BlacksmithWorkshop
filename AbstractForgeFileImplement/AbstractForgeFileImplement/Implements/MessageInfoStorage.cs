using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.StoragesContracts;
using AbstractForgeContracts.ViewModels;
using AbstractForgeFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractForgeFileImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        private readonly FileDataListSingleton source;
        public MessageInfoStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<MessageInfoViewModel> GetFullList()
        {
            return source.MessagesInfo.Select(CreateModel).ToList();
        }
        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.MessagesInfo.Where(rec => (model.ClientId.HasValue && rec.ClientId == model.ClientId) ||
            (!model.ClientId.HasValue && rec.DateDelivery.Date == model.DateDelivery.Date))
                .Select(CreateModel)
                .ToList();
        }
        public void Insert(MessageInfoBindingModel model)
        {
            MessageInfo element = source.MessagesInfo.FirstOrDefault(rec => rec.MessageId == model.MessageId);
            if (element != null)
            {
                throw new Exception("Уже есть письмо с таким идентификатором");
            }
            source.MessagesInfo.Add(CreateModel(model, element));
        }
        private static MessageInfo CreateModel(MessageInfoBindingModel model, MessageInfo messageInfo)
        {
            messageInfo.MessageId = model.MessageId;
            messageInfo.ClientId = model.ClientId;
            messageInfo.SenderName = model.FromMailAddress;
            messageInfo.DateDelivery = model.DateDelivery;
            messageInfo.Subject = model.Subject;
            messageInfo.Body = model.Body;
            return messageInfo;
        }
        private MessageInfoViewModel CreateModel(MessageInfo messageInfo)
        {
            return new MessageInfoViewModel
            {
                MessageId = messageInfo.MessageId,
                SenderName = messageInfo.SenderName,
                DateDelivery = messageInfo.DateDelivery,
                Subject = messageInfo.Subject,
                Body = messageInfo.Body
            };
        }
    }
}
