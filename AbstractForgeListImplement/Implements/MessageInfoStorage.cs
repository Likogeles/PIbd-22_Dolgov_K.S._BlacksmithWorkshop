using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.StoragesContracts;
using AbstractForgeContracts.ViewModels;
using AbstractForgeListImplement.Models;
using System.Collections.Generic;

namespace AbstractForgeListImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        private readonly DataListSingleton source;
        public MessageInfoStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<MessageInfoViewModel>();
            foreach (var messageInfo in source.MessagesInfo)
            {
                if ((model.ClientId.HasValue && messageInfo.ClientId == model.ClientId) ||
                    (!model.ClientId.HasValue && messageInfo.DateDelivery.Date == model.DateDelivery.Date))
                {
                    result.Add(CreateModel(messageInfo));
                }
            }
            if (result.Count > 0)
            {
                return result;
            }
            return null;
        }

        public List<MessageInfoViewModel> GetFullList()
        {
            var result = new List<MessageInfoViewModel>();
            foreach (var messageInfo in source.MessagesInfo)
            {
                result.Add(CreateModel(messageInfo));
            }
            return result;
        }

        public void Insert(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            source.MessagesInfo.Add(CreateModel(model, new MessageInfo()));
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
        private static MessageInfoViewModel CreateModel(MessageInfo messageInfo)
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
