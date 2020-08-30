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
    public partial class frmUser : DevExpress.XtraEditors.XtraForm
    {
        public frmUser()
        {
            InitializeComponent();
        }
        public class UserInfo
        {
            public string id_user;
            public string gmail;
            public string phone_number;
            public long dob;
            public string password;
            public string name;
        }
        public class ResponseTicketRegister
        {
            public int status;
            public string message;
            public UserInfo[] user;
        }
        private ResponseTicketRegister response;
        public class PostServer
        {
            private string vehicle_id;
            private string time_duration;
            public class RequestTicketRegister
            {
                public string vehicle_id;
                public string time_duration;
            }

            public PostServer(string vehicle_id, string time_duration)
            {
                this.vehicle_id = vehicle_id.Trim();
                this.time_duration = time_duration;
            }
            public ResponseTicketRegister fetch()
            {
                var request = new RequestTicketRegister
                {
                    vehicle_id = this.vehicle_id,
                    time_duration = this.time_duration,
                };

                string jsonStr = new JavaScriptSerializer().Serialize(request);
                JObject json = JObject.Parse(jsonStr);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Program.HOST + Program.API_REGISTER_MONTH_TICKET);
                //httpWebRequest.PreAuthenticate = true;
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
                    ResponseTicketRegister response = JsonConvert.DeserializeObject<ResponseTicketRegister>(result);
                    return response;
                }
            }
        }
        public ResponseTicketRegister GetInfoAllUser()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Program.HOST + "/user/users");
            httpWebRequest.ContentType = Program.HEADER_CONTENT_TYPE;
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("Authorization", Program.access_token);

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();//string json
                ResponseTicketRegister response = JsonConvert.DeserializeObject<ResponseTicketRegister>(result);
                return response;
            }
        }
        public void loadGridControl(ResponseTicketRegister response)
        {
            DataTable dt = new DataTable("MyDataTable");
            dt.Columns.Add("Id User");
            dt.Columns.Add("Name");
            dt.Columns.Add("Gmail");
            dt.Columns.Add("Phone Number");
            dt.Columns.Add("Password");
            dt.Columns.Add("DoB");
            foreach (UserInfo value in response.user)
            {
                DataRow row = dt.NewRow();
                row[0] = value.id_user;
                row[1] = value.name;
                row[2] = value.gmail;
                row[3] = value.phone_number;
                row[4] = value.password;
                row[5] = value.dob;
                dt.Rows.Add(row);
            }
            gridControl.DataSource = dt;
            txtName.Text = gridView.GetSelectedRows().ToString();
            //////////txtVehicleId.Text = gridView.GetDataRow(gridView.GetSelectedRows()[0])[1].ToString();

        }
        private void frmUser_Load(object sender, EventArgs e)
        {
            this.response = GetInfoAllUser();
            loadGridControl(this.response);
        }
    }
}
//GET
//HttpClient client = new HttpClient();
//client.BaseAddress = new Uri("http://127.0.0.1:5000/");
//HttpResponseMessage response = client.GetAsync("/").Result;
//string emp = response.Content.ReadAsStringAsync().Result;

//JavaScriptSerializer json_serializer = new JavaScriptSerializer();
//Employee response = json_serializer.Deserialize<Employee>(emp.Trim());