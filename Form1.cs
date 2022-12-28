using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace restApiClient
{
    public partial class Form1 : Form
    {

        String URL = "http://localhost/RestApiJLPKL0/";
        String ROUTE = "index.php";
        //Boolean putInputEnabled = true;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getMethod(ROUTE);
            //putInputEnabled = false;
            //putInput();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String ROUTE = "index.php" + "?id=" + textBox5.Text;
            var client = new RestClient(URL);
            var request = new RestRequest(ROUTE, Method.GET);
            Subj subject = client.Execute<List<Subj>>(request).Data[0];

            textBox6.Text = subject.Subject;
            textBox7.Text = subject.Teacher;
            textBox8.Text = subject.Grade.ToString();
            //putInputEnabled = true;
            // putInput();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String ROUTE = "index.php" + "?token=" + Login.token;
            var client = new RestClient(URL);
            var request = new RestRequest(ROUTE, Method.POST);
            request.RequestFormat = DataFormat.Json;
            try
            {
                request.AddBody(new Subj
                {
                    Subject = textBox2.Text,
                    Teacher = textBox3.Text,
                    Grade = int.Parse(textBox4.Text)
                });
                IRestResponse response = client.Execute(request);
                getMethod();
            }
            catch
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            var client = new RestClient(URL);
            String ROUTE = "index.php/{id}";
            var request = new RestRequest(ROUTE, Method.DELETE);
            request.AddParameter("id", textBox1.Text);
            request.AddParameter("token", Login.token);
            IRestResponse restResponse = client.Execute(request);
            textBox1.Clear();
            getMethod();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String ROUTE = "index.php" + "?id=" + textBox5.Text + "&token=" + Login.token;
            var client = new RestClient(URL);
            var request = new RestRequest(ROUTE, Method.PUT);

            request.RequestFormat = DataFormat.Json;
            try
            {
                request.AddBody(
                    new Subj
                    {
                        Subject = textBox6.Text,
                        Teacher = textBox7.Text,
                        Grade = int.Parse(textBox8.Text)
                    }
                );

                IRestResponse restResponse = client.Execute(request);
                getMethod();
            }
            catch { }

            //putInput();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            String[] items = listBox1.SelectedItem.ToString().Split(',');
            textBox5.Text = items[0].Trim();
            textBox6.Text = items[1].Trim();
            textBox7.Text = items[2].Trim();
            textBox8.Text = items[3].Trim();
            // putInputEnabled = true;
            // putInput();
        }

        private void getMethod(String route = "index.php")
        {
            var client = new RestClient(URL);
            var request = new RestRequest(route, Method.GET);

            IRestResponse<List<Subj>> restResponse = client.Execute<List<Subj>>(request);

            listBox1.Items.Clear();
            foreach (Subj subject in restResponse.Data)
            {
                listBox1.Items.Add(subject.Id + ",  " + subject.Subject + ",  " + subject.Teacher + ",  " + subject.Grade);
            }
        }

        /*  private void putInput() 
          {
              if (putInputEnabled)
              {
                  textBox6.Enabled = true;
                  textBox7.Enabled = true;
                  textBox8.Enabled = true;
                  button5.Enabled = true;
                  putInputEnabled = false;
              }
              else
              {
                  textBox6.Enabled = false;
                  textBox6.Clear();
                  textBox7.Enabled = false;
                  textBox7.Clear();
                  textBox8.Enabled = false;
                  textBox8.Clear();
                  button5.Enabled = false;
                  putInputEnabled = true;
              }
          }
        */
        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
        }
    }
}
