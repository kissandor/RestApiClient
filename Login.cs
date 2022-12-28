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
using System.Text.Json;
using Newtonsoft.Json;

namespace restApiClient
{
    public partial class Login : Form
    {
        String URL = "http://localhost/RestApiJLPKL0/";
        String ROUTE = "login.php";

        public static string token = "";

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var client = new RestClient(URL);
            var request = new RestRequest(ROUTE, Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new User
            {
                name = textBox1.Text,
                password = textBox2.Text,

            });
            RestResponse response = client.Execute(request);
            String adat = response.Content;

            Resp resp = JsonConvert.DeserializeObject<Resp>(adat);


            if (resp.status == "1")
            {
                token = resp.token;
                this.Close();
            }
            else {
                textBox1.Text = "Hibas adat.";
                textBox2.Clear();
            }

            /*
            RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
            Resp jasonFormat = deserial.Deserialize<Resp>(response);
            if (jasonFormat.status == "1")
            {
                token = jasonFormat.token;
                this.Close();
            }
            */
        }
    }
}
