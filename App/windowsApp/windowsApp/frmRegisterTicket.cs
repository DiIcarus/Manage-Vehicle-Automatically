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
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace windowsApp
{
    public partial class frmRegisterTicket : DevExpress.XtraEditors.XtraForm
    {
        public frmRegisterTicket()
        {
            InitializeComponent();
        }
        public class RequestregisterTicket
        {
            public string vehicle_id;
            public string duration;
        }
        public class PostResponse
        {
            public int status;
            public string message;
            public string end_date_time;
            public string id_tickets;
            public string id_vehicle;
        }
        public PostResponse PostInfoAllUser()
        {
            var request = new RequestregisterTicket
            {
                vehicle_id = this.Text.ToString().Trim(),
                duration = txtDuration.Text.Trim()
            };
            string jsonStr = new JavaScriptSerializer().Serialize(request);
            JObject json = JObject.Parse(jsonStr);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Program.HOST + "/register-ticket");
            httpWebRequest.ContentType = Program.HEADER_CONTENT_TYPE;
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", Program.access_token);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();//string json
                PostResponse response = JsonConvert.DeserializeObject<PostResponse>(result);
                return response;
            }
        }
        private void frmRegisterTicket_Load(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            PostResponse res = PostInfoAllUser();
            MessageBox.Show(res.message);
        }
    }
}