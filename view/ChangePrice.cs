using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace administrator
{
    public partial class ChangePrice : Form
    {

        Socket socket = Connection.GetSocket();
        static string? service;
        List<string> serv;
        Dictionary<string, int>? dictionary = new Dictionary<string, int>();
        public ChangePrice()
        {
            InitializeComponent();
            try
            {
                Socket socket = Connection.GetSocket();
                Request request = new Request("PriceOut", "");
                string mes = JsonSerializer.Serialize(request);
                var messageBytes = Encoding.UTF8.GetBytes(mes);
                socket.Send(messageBytes, SocketFlags.None);
                var buffer = new byte[1_1000];
                var received = socket.Receive(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, received);
                Response? respon = JsonSerializer.Deserialize<Response>(response);
                dictionary = JsonSerializer.Deserialize<Dictionary<string, int>>(JsonSerializer.Deserialize<List<string>>(respon.data)[0]);
                List<string>? serv = JsonSerializer.Deserialize<List<string>>(JsonSerializer.Deserialize<List<string>>(respon.data)[1]);
                foreach (string line in serv)
                {
                    comboBox1.Items.Add(line);
                }
                /*label1.Text = "Стрижка";
                textBox1.Text = dictionary["Стрижка"].ToString();
                label2.Text = "Укладка волос";
                textBox2.Text = dictionary["Укладка волос"].ToString();
                label3.Text = "Окрашивание волос";
                textBox3.Text = dictionary["Окрашивание волос"].ToString();
                label4.Text = "Маникюр";
                textBox4.Text = dictionary["Маникюр"].ToString();
                label5.Text = "Массаж";
                textBox5.Text = dictionary["Массаж"].ToString();
                label6.Text = "Макияж";
                textBox6.Text = dictionary["Макияж"].ToString();*/
            }
            catch (System.Net.Sockets.SocketException) { MessageBox.Show("Соединение с сервером прервано."); }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox1.Text, out int price1) && price1 > 0)
            {
                dictionary[service] = price1;
                try
                {
                    Request request = new Request("PriceIn", JsonSerializer.Serialize(dictionary));
                    string mes = JsonSerializer.Serialize(request);
                    var messageBytes = Encoding.UTF8.GetBytes(mes);
                    socket.Send(messageBytes, SocketFlags.None);
                    MessageBox.Show("Изменения успешно сохранены.");
                }
                catch (System.Net.Sockets.SocketException) { MessageBox.Show("Соединение с сервером прервано."); }
            }
            else
            {
                MessageBox.Show("Введено некорректное значение!");
                textBox1.Text = dictionary[service].ToString();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = dictionary[comboBox1.Text.Trim()].ToString();
            service = comboBox1.Text.Trim();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
