using DinerBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace DinerBusinessLogic.ViewModels
{
    [DataContract]
    public class MessageInfoViewModel : BaseViewModel
    {
        [DataMember]
        public string MessageId { get; set; }
        [DisplayName("Отправитель")]
        [Column(title: "Отправитель", width: 150)]
        [DataMember]
        public string SenderName { get; set; }
        [DisplayName("Дата письма")]
        [Column(title: "Дата письма", width: 100)]
        [DataMember]
        public DateTime DateDelivery { get; set; }
        [DisplayName("Заголовок")]
        [Column(title: "Заголовок", width: 150)]
        [DataMember]
        public string Subject { get; set; }
        [DisplayName("Текст")]
        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string Body { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "SenderName",
            "DateDelivery",
            "Subject",
            "Body"
        };
    }
}