using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class Server
    {
        public Server(IPEndPoint ipep)
        {
            this.ipendpoint = ipep;
            this.serverSk.Bind(ipep);
        }

        private struct Client
        {
            private Socket clientSk;
            private NetworkStream networkSt;

            public Client(Socket sk, NetworkStream ns) 
            {
                this.clientSk = sk;
                this.networkSt = ns;
            }
            public Socket ClientSk {get => clientSk;}
            public NetworkStream NetworkSt {get => networkSt;}
        }
        private static int clientSkCount = 0;
        private readonly int clientSkCountMAX = 3;

        private  Socket serverSk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private List<Client> clients = new List<Client>();

        private IPEndPoint ipendpoint;

        private void SendMessage(Client receiver, string msg)
        {
            byte[] byteSend = Encoding.UTF8.GetBytes(msg);
            receiver.NetworkSt.Write(byteSend, 0, byteSend.Length);
        }

        public void startListening(string mess)
        {
            serverSk.Listen(1);
            Console.WriteLine(mess);
            while (clientSkCount < clientSkCountMAX)
            {
                Socket clientSk = serverSk.Accept();
                if (clientSk.Connected)
                {
                    NetworkStream ns = new NetworkStream(clientSk);
                    clients.Add(new Client(clientSk, ns));
                    int index = clientSkCount++;
                    SendMessage(clients[index], "You joined chat room.");
                    BroadcastMessage(clients[index], "A new client joined our chat room.");
                    Console.WriteLine(((IPEndPoint)(clients[index].ClientSk.RemoteEndPoint)).ToString() + " is connecting.");
                    
                    new Task(() => receiveMessage(clients[clientSkCount - 1])).Start();
                }
            }
            Console.WriteLine("Server is suspending because too many clients are connecting.");
        }

        public void Stop()
        {
            serverSk.Close();
        }

        private void receiveMessage(Client clt)
        {
            int length = 0;
            byte[] byteRecv;
            string mess = string.Empty;
            while (true)
            {
                length = clt.ClientSk.Available;
                byteRecv = new byte[length];
                clt.NetworkSt.Read(byteRecv, 0, length);
                if (length > 0)
                {
                    mess = Encoding.UTF8.GetString(byteRecv, 0, length);
                    if (mess == "quit")
                        break;
                    Console.WriteLine(mess);
                    new Task(() => BroadcastMessage(clt, mess)).Start();
                }
            }
            Console.WriteLine(((IPEndPoint)(clt.ClientSk.RemoteEndPoint)).ToString() + " quited.");
            clt.NetworkSt.Close();
            clt.ClientSk.Close();
            clients.Remove(clt);
            if (clientSkCount-- == clientSkCountMAX)
                new Task(() => startListening("Server is resuming:")).Start();
        }

        private void BroadcastMessage(Client sender, string msg)
        {
            foreach(Client clt in clients)
            {
                if (sender.Equals(clt))
                    continue;
                SendMessage(clt, msg);
            }
        }
    }
}