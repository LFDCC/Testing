using System;
using SocketServer.Socket;

namespace SocketServer
{
    internal static class Program
    {
        private static void Main()
        {
            var f = new SocketMain();
            Console.ReadLine();
            //AgentService.ServiceMain(); 
        }
    }
}