using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        private static IPAddress ipv4(IPAddress[] list)
        {
            foreach(IPAddress ip in list)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip;
            // Không tìm thấy ipv4
            return list[0];
        }
        static void Main(string[] args)
        {
            IPHostEntry iphe = Dns.GetHostEntry(Dns.GetHostName());
            IPEndPoint ipep = new IPEndPoint(ipv4(iphe.AddressList), 2003);
            Server server = new Server(ipep);

            Task t = new Task(() => server.startListening($"Server is running on {ipep}:"));
            t.Start();

            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
            } while (key.Key != ConsoleKey.Q);

            server.Stop();
        }
    }
}