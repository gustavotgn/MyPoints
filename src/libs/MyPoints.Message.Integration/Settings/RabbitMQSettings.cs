﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyPoints.Message.Integration.Settings
{
    public class RabbitMQSettings
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
