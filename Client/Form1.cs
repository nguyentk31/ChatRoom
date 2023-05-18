using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Socket Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private NetworkStream ns;

        private delegate void infoDel(string msg);

        private void info(string msg)
        {
            if (richTextBox1.InvokeRequired)
            {
                infoDel infoDelMess = new infoDel(info);
                richTextBox1.Invoke(infoDelMess, new object[] { msg });
                return;
            }
            richTextBox1.Text += (msg + '\n');
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();
        }

        private void receiveMessage()
        {
            int length = 0;
            Byte[] bytereceived;
            string msg = string.Empty;
            while (true)
            {
                length = Client.Available;
                bytereceived = new byte[length];
                ns.Read(bytereceived, 0, length);
                if (length > 0)
                {
                    msg = Encoding.UTF8.GetString(bytereceived);
                    info(msg);
                }
            }
        }

        private void btSend_Click(object sender, EventArgs e)
        {
            string msg = tbName.Text + ": " + tbMess.Text;
            Byte[] byteSend = Encoding.UTF8.GetBytes(msg);
            ns.Write(byteSend, 0, byteSend.Length);
            info(msg);
            tbName.ReadOnly = true;
            tbMess.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse("10.0.120.172");
            Client.Connect(ip, 2003);
            if (Client.Connected)
            {
                ns = new NetworkStream(Client);
                Task t = new Task(() => receiveMessage());
                t.Start();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Byte[] byteSend = Encoding.UTF8.GetBytes("quit");
            ns.Write(byteSend, 0, byteSend.Length);
            ns.Close();
            Client.Close();
        }
    }
}