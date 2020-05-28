using DinerBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DinerBusinessLogic.ViewModels
{
    /// <summary>
    /// Базовый класс для View-моделей
    /// </summary>
    [DataContract]
    public abstract class BaseViewModel
    {
        [Column(visible: false)]
        [DataMember]
        public int Id { get; set; }
        public abstract List<string> Properties();
    }
}
