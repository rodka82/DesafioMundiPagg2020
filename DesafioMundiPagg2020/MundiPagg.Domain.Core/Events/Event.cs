﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Domain.Core.Events
{
    public abstract class Event
    {
        public DateTime TimeStamp { get; protected set; }

        protected Event()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
