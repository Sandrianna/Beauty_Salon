﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace administrator
{
    internal class Response
    {
        public string command { get; set; }
        public string data { get; set; }

        public Response(string command, string data)
        {
            this.command = command;
            this.data = data;
        }
    }
}
