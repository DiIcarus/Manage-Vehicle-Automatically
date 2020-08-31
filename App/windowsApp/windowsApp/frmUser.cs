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
        public string state = "";
        public frmUser()
        {
            InitializeComponent();
        }
        public string convertTimestampToString(double timestamp){
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(timestamp);
            string printDate = dateTime.ToShortDateString() + " " + dateTime.ToShortTimeString();
            return printDate.Split(' ')[0];
        }
        void setDecisionButton(bool state)
        {
            btnCancel.Enabled=btnSave.Enabled = state;
        }
        void setCRUDButton(bool state)
        {
            btnDelete.Enabled = btnInsert.Enabled = btnUpdate.Enabled= state;
        }
        void setTextInput(bool state)
        {
            txtDoB.Enabled = txtGmail.Enabled = txtIdUser.Enabled = txtName.Enabled = txtPassword.Enabled = txtPhoneNumber.Enabled = state;
        }
        public class UserInfo
        {
            public string id_user;
            public string gmail;
            public string phone_number;
            public string dob;
            public string password;
            public string name;
        }
        public class PostRequest
        {
            public string gmail;
            public string phone_number;
            public string dob;
            public string password;
            public string name;
        }
        public class PostResponse
        {
            public int status;
            public string message;
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
        public PostResponse PostInfoAllUser()
        {
            var request = new PostRequest
            {
                gmail = txtGmail.Text.Trim(),
                phone_number = txtPhoneNumber.Text.Trim(),
                dob = txtDoB.Text.Trim(),
                password = txtPassword.Text.Trim(),
                name = txtName.Text.Trim(),
            };
            string jsonStr = new JavaScriptSerializer().Serialize(request);
            JObject json = JObject.Parse(jsonStr);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Program.HOST + "/register");
            httpWebRequest.ContentType = Program.HEADER_CONTENT_TYPE;
            httpWebRequest.Method = "POST";
            //httpWebRequest.Headers.Add("Authorization", Program.access_token);

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
        public PostResponse PutInfoAllUser()
        {
            var request = new UserInfo
            {
                id_user=txtIdUser.Text.Trim(),
                gmail = txtGmail.Text.Trim(),
                phone_number = txtPhoneNumber.Text.Trim(),
                dob = txtDoB.Text.Trim(),
                password = txtPassword.Text.Trim(),
                name = txtName.Text.Trim(),
            };
            string jsonStr = new JavaScriptSerializer().Serialize(request);
            JObject json = JObject.Parse(jsonStr);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Program.HOST + "/user/users");
            httpWebRequest.ContentType = Program.HEADER_CONTENT_TYPE;
            httpWebRequest.Method = "PUT";
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
        public PostResponse DeleteInfoAllUser()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Program.HOST + "/user/users"+"?id="+ gridView.GetDataRow(gridView.GetSelectedRows()[0])[0]);
            httpWebRequest.ContentType = Program.HEADER_CONTENT_TYPE;
            httpWebRequest.Method = "DELETE";
            httpWebRequest.Headers.Add("Authorization", Program.access_token);

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();//string json
                PostResponse response = JsonConvert.DeserializeObject<PostResponse>(result);
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
                row[5] = convertTimestampToString(Convert.ToDouble(value.dob));
                dt.Rows.Add(row);
            }
            gridControl.DataSource = dt;
            gridControl.RefreshDataSource();
        }
        private void frmUser_Load(object sender, EventArgs e)
        {
            setCRUDButton(true);
            setDecisionButton(false);
            setTextInput(false);
            this.response = GetInfoAllUser();
            loadGridControl(this.response);
            txtVehicleId.Enabled = false;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            setTextInput(true);
            setCRUDButton(false);
            setDecisionButton(true);
            txtGmail.Focus();
            txtName.Text = "Unknown";
            txtDoB.Text = "12345";
            this.state = "insert";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            setTextInput(false);
            setCRUDButton(true);
            setDecisionButton(false);
            txtName.Text = "";
            txtDoB.Text = "";
            txtVehicleId.Enabled = false;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (this.state)
            {
                case "insert":
                    PostResponse resAdd = PostInfoAllUser();
                    if (resAdd.status == 201)
                    {
                        MessageBox.Show("Register Successfully");
                        response = GetInfoAllUser();
                        loadGridControl(response);
                    }
                    else
                    {
                        MessageBox.Show(resAdd.message);
                    }
                    break;
                case "update":
                    PostResponse resUpdate = PutInfoAllUser();
                    if (resUpdate.status == 201)
                    {
                        MessageBox.Show("Edit Successfully");
                        response = GetInfoAllUser();
                        loadGridControl(response);
                    }
                    else
                    {
                        MessageBox.Show(resUpdate.message);
                    }
                    break;
                case "register_ticket":
                    DataRow row = gridView.GetDataRow(gridView.GetSelectedRows()[0]);
                    var request = new PostRequestRegisterVehicle
                    {
                        user_ids = row[0].ToString(),
                        vehicle_id = txtVehicleId.Text.Trim()

                    };
                    PostResponse res = PostRegisterVehicle(request);
                    MessageBox.Show(res.message);
                    break;
            }
            setTextInput(false);
            setCRUDButton(true);
            setDecisionButton(false);
            txtVehicleId.Enabled = false;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            txtVehicleId.Enabled = false;
            setTextInput(true);
            setCRUDButton(false);
            setDecisionButton(true);
            DataRow row = gridView.GetDataRow(gridView.GetSelectedRows()[0]);
            txtIdUser.Text= row[0].ToString();
            txtName.Text = row[1].ToString();
            txtGmail.Text = row[2].ToString();
            txtPhoneNumber.Text = row[3].ToString();
            txtPassword.Text= row[4].ToString();
            txtDoB.Text = convertTimestampToString(Convert.ToDouble(row[5]));
            this.state = "update";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            PostResponse resAdd = DeleteInfoAllUser();
            response = GetInfoAllUser();
            loadGridControl(response);
        }
        public class PostRequestRegisterVehicle
        {
            public string vehicle_id;
            public string user_ids;
        }
        public PostResponse PostRegisterVehicle(PostRequestRegisterVehicle request)
        {
            string jsonStr = new JavaScriptSerializer().Serialize(request);
            JObject json = JObject.Parse(jsonStr);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Program.HOST + "/register-vehicle");
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
        private void btnVehicleRegister_Click(object sender, EventArgs e)
        {
            this.state = "register_ticket";
            //setTextInput(true);
            setCRUDButton(false);
            setDecisionButton(true);
            txtVehicleId.Enabled = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            response = GetInfoAllUser();
            loadGridControl(response);
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