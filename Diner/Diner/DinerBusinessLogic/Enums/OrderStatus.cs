﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DinerBusinessLogic.Enums
{
    public enum OrderStatus
    {
        Принят = 0,
        Выполняется = 1,
        Готов = 2,
        Оплачен = 3,
        Требуются_продукты = 4
    }
}
