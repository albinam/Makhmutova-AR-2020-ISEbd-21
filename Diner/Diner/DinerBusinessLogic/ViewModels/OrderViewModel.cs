using DinerBusinessLogic.Attributes;
using DinerBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace DinerBusinessLogic.ViewModels
{
    [DataContract]
    public class OrderViewModel : BaseViewModel
    {
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public int SnackId { get; set; }
        [DataMember]
        public int? ImplementerId { get; set; }
        [Column(title: "Клиент", width: 150)]
        [DisplayName("Клиент")]
        [DataMember]
        public string ClientFIO { get; set; }
        [Column(title: "Закуска", width: 100) ]
        [DisplayName("Закуска")]
        [DataMember]
        public string SnackName { get; set; }
        [Column(title: "Исполнитель", width: 100)]
        [DisplayName("Исполнитель")]
        [DataMember]
        public string ImplementerFIO { get; set; }
        [Column(title: "Количество", width: 100)]
        [DisplayName("Количество")]
        [DataMember]
        public int Count { get; set; }
        [Column(title: "Сумма", width: 50)]
        [DisplayName("Сумма")]
        [DataMember]
        public decimal Sum { get; set; }
        [Column(title: "Статус", width: 100)]
        [DisplayName("Статус")]
        [DataMember]
        public OrderStatus Status { get; set; }
        [Column(title: "Дата создания", width: 100)]
        [DisplayName("Дата создания")]
        [DataMember]
        public DateTime DateCreate { get; set; }
        [Column(title: "Дата выполнения", width: 100)]
        [DisplayName("Дата выполнения")]
        [DataMember]
        public DateTime? DateImplement { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "ClientFIO",
            "SnackName",
            "ImplementerFIO",
            "Count",
            "Sum",
            "Status",
            "DateCreate",
            "DateImplement"
        };
    }
}