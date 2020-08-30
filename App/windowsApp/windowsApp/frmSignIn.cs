using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace windowsApp
{
    public partial class frmSignIn : DevExpress.XtraEditors.XtraForm
    {
        public frmSignIn()
        {
            InitializeComponent();
        }
        private void frmSignIn_Load(object sender, EventArgs e)
        {
            Program.frm_sign_in = this;
            txtGmail.Text = "diicarus.8398@gmail.com";
            txtPassword.Text = "123";
        }
        public class ResponseSignIn
        {
            public int status;
            public string message;
            public string access_token;
        }
        private ResponseSignIn response;
        public class PostServer
        {
            private string gmail;
            private string password;
            public class RequestSignIn
            {
                public string gmail;
                public string password;
            }
            
            public PostServer(string gmail, string password)
            {
                this.password = password.Trim();
                this.gmail = gmail.Trim();
            }
            public ResponseSignIn fetch()
            {
                var obj = new RequestSignIn {
                    gmail = this.gmail,
                    password = this.password,
                };

                string jsonStr = new JavaScriptSerializer().Serialize(obj);
                JObject json = JObject.Parse(jsonStr);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Program.HOST + Program.API_SIGN_IN);
                httpWebRequest.ContentType = Program.HEADER_CONTENT_TYPE;
                httpWebRequest.Method = Program.METHOD;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();//string json
                    ResponseSignIn response = JsonConvert.DeserializeObject<ResponseSignIn>(result);
                    return response;
                }
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            string gmail = txtGmail.Text.Trim(), password = txtPassword.Text.Trim();
            PostServer api = new PostServer(gmail, password);
            this.response = api.fetch();
            if (this.response.status == 400)
            {
                MessageBox.Show(this.response.message);
                return;
            }
            else if (this.response.status ==200)
            {
                Program.access_token = "Bearer "+ this.response.access_token;
                MessageBox.Show(this.response.message);
                frmMain f = new frmMain();
                f.Show();
                this.Hide();
                //this.Close();
            }
        }

        
    }
}