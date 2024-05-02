using System;
using System.Collections;
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

namespace administrator
{
    public partial class AddService : Form
    {
        Dictionary<string, int>? dictionary;
        List<string>? serv;
        Socket socket = Connection.GetSocket();

        public AddService()
        {
            InitializeComponent();
            try
            {
                Request request = new Request("AllServicesOut", "");
                string mes = JsonSerializer.Serialize(request);
                var messageBytes = Encoding.UTF8.GetBytes(mes);
                socket.Send(messageBytes, SocketFlags.None);
                var buffer = new byte[1_1000];
                var received = socket.Receive(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, received);
                Response? respon = JsonSerializer.Deserialize<Response>(response);
                dictionary = JsonSerializer.Deserialize<Dictionary<string, int>>(JsonSerializer.Deserialize<List<string>>(respon.data)[0]);
                serv = JsonSerializer.Deserialize<List<string>>(JsonSerializer.Deserialize<List<string>>(respon.data)[1]);
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Соединение с сервером прервано.");
            }
        }

        private void AddService_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serv.Contains(textName.Text.Trim()))
            {
                MessageBox.Show("Такая услуга уже существует.");
                textName.Clear();
            }
            else if (textName.Text.Trim() != null && textName.Text.Trim() != "")
            {
                if (Int32.TryParse(textPrice.Text.Trim(), out int price))
                {
                    serv.Add(textName.Text.Trim());
                    dictionary.Add(textName.Text.Trim(), price);
                    MessageBox.Show("Услуга успешно добавлена.");
                    List<string> allData = new List<string>() { JsonSerializer.Serialize(dictionary), JsonSerializer.Serialize(serv) };
                    try
                    {
                        Request request = new Request("AllServicesIn", JsonSerializer.Serialize(allData));
                        string mes = JsonSerializer.Serialize(request);
                        var messageBytes = Encoding.UTF8.GetBytes(mes);
                        socket.Send(messageBytes, SocketFlags.None);
                        this.Hide();
                        ProfileForm pf = new ProfileForm();
                        pf.Show();
                    }
                    catch (System.Net.Sockets.SocketException) { MessageBox.Show("Соединение с сервером прервано."); }
                }
                else
                {
                    MessageBox.Show("Введена некорректная цена.");
                    textPrice.Clear();
                }
            }
            else MessageBox.Show("Введите название услуги.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ProfileForm pf = new ProfileForm();
            pf.Show();
        }
    }
}
