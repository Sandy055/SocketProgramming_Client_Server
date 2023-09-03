using SuperSimpleTcp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace SocketProgramming_Client_Server
{
    public partial class Form1 : Form
    {



        public Form1()
        {
            InitializeComponent();
        }

        SimpleTcpServer server;
        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;

            server = new SimpleTcpServer(textBox1.Text);
            server.Events.DataReceived += Events_DataReceived;


            server.Events.ClientConnected += Events_ClientConnected;
            server.Events.ClientDisconnected += Events_ClientDisconnected;




        }


        private void Events_ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                textBox2.Text += $"{e.IpPort} diconnected.{Environment.NewLine}";
                lstClientIP.Items.Remove(e.IpPort);
            });
        }
        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                byte[] dataBytes = e.Data.ToArray();
                string dataString = Encoding.UTF8.GetString(dataBytes);
                textBox2.Text += $"{e.IpPort}: {dataString}{Environment.NewLine}";
            });
        }


        private void Events_ClientConnected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                textBox2.Text += $"{e.IpPort} connected.{Environment.NewLine}";
                lstClientIP.Items.Add(e.IpPort);
            });

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void buttonstart_Click(object sender, EventArgs e)
        {
            server.Start();
            textBox2.Text += $"Starting...{Environment.NewLine}";
            buttonstart.Enabled = false;
            button1.Enabled = true;

        }

        private void lstClientIP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (server.IsListening)
            {
                if (!string.IsNullOrEmpty(textBox3.Text) && lstClientIP.SelectedItem != null)
                {
                    string clientIp = lstClientIP.SelectedItem.ToString();
                    try
                    {
                        server.Send(clientIp, textBox3.Text);
                        textBox2.Text += $"Server to {clientIp}: {textBox3.Text}{Environment.NewLine}";
                        textBox3.Text = string.Empty;
                    }
                    catch (Exception ex)
                    {
                        textBox2.Text += $"Error sending message: {ex.Message}{Environment.NewLine}";
                    }
                }
                else
                {
                    textBox2.Text += "Please select a client and enter a message before sending.{Environment.NewLine}";
                }
            }
            else
            {
                textBox2.Text += "Server is not listening. Start the server first.{Environment.NewLine}";
            }
        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
