﻿using AbstractSweetShopBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractSweetShopBusinessLogic.ViewModels
{
    /// <summary>
    /// Сообщения, приходящие на почту
    /// </summary>
    [DataContract]
    public class MessageInfoViewModel : BaseViewModel
    {
        [DataMember]
        public string MessageId { get; set; }

        [Column(title: "Отправитель", gridViewAutoSize: GridViewAutoSize.AllCells)]
        [DisplayName("Отправитель")]
        [DataMember]
        public string SenderName { get; set; }

        [Column(title: "Дата письма", gridViewAutoSize: GridViewAutoSize.AllCells)]
        [DisplayName("Дата письма")]
        [DataMember]
        public DateTime DateDelivery { get; set; }

        [Column(title: "Заголовок", gridViewAutoSize: GridViewAutoSize.AllCells)]
        [DisplayName("Заголовок")]
        [DataMember]
        public string Subject { get; set; }

        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DisplayName("Текст")]
        [DataMember]
        public string Body { get; set; }

        public override List<string> Properties() => new List<string> { "SenderName", "DateDelivery",
            "Subject", "Body"};
    }
}
