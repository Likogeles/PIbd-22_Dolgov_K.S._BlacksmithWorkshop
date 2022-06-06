using System;
using System.ComponentModel;
using AbstractForgeContracts.Attributes;

namespace AbstractForgeContracts.ViewModels
{
    /// <summary>
    /// Сообщения, приходящие на почту
    /// </summary>
    public class MessageInfoViewModel
    {
        public string MessageId { get; set; }
        [Column(title: "Отправитель", width: 195)]
        public string SenderName { get; set; }

        [Column(title: "Дата письма", width: 120)]
        public DateTime DateDelivery { get; set; }

        [Column(title: "Заголовок", width: 210)]
        public string Subject { get; set; }

        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Body { get; set; }
    }
}
