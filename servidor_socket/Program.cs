﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using SuperSocket.SocketBase;
using SuperWebSocket;
using System.Net;
using System.Net.Sockets;


namespace servidor_socket
{
    public class Program
    {


        static void Main(String[] args)
        {
            servidor server = new servidor();
        }

    }
}
