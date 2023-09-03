
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperSimpleTcp;

namespace SocketProgramming_Client_Server
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        SimpleTcpClient client;

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient(textBox1.Text);
            client.Events.DataReceived += Events_DataReceived;

            client.Events.Connected += Events_Connected;
            client.Events.Disconnected += Events_Disconnected;
            button1.Enabled = false; 
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

        private void button1_Click(object sender, EventArgs e)
        {
            if(client.IsConnected)
            {
                if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    client.Send(textBox3.Text);
                    textBox2.Text += $"Me: {textBox3.Text}{Environment.NewLine}";
                    textBox3.Text = string.Empty;

                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Events_Disconnected(Object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                textBox2.Text += $"Server disconnected.{Environment.NewLine}";
            });
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }
        
        private void Events_DataReceived(object sender, SuperSimpleTcp.DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                byte[] dataBytes = e.Data.ToArray();
                string dataString = Encoding.UTF8.GetString(dataBytes);
                textBox2.Text += $"Server: {dataString}{Environment.NewLine}";
            });
        }



        private void Events_Connected(Object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                textBox2.Text += $"Server connected.{Environment.NewLine}";
            });
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                client.Connect();
                button1.Enabled = true;
                buttonConnect.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    } 
}
