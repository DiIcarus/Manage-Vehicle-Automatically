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
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

namespace windowsApp
{
    public partial class frmUserManager : DevExpress.XtraEditors.XtraForm
    {
        public frmUserManager()
        {
            InitializeComponent();
        }
        public class ResponseTicketRegister
        {
            public int status;
            public string message;
            public string end_date_time;
            public string id_tickets;
            public string id_vehicle;
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
                httpWebRequest.Method = Program.METHOD;
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
        void setDefaultInputState(bool state)
        {
            txtVehicleId.Enabled
                = txtName.Enabled
                = txtGmail.Enabled
                = txtPhone.Enabled
                = txtTicketAvailable.Enabled
                = txtSharingCounter.Enabled
                = txtLastCheckIn.Enabled
                = txtLastCheckout.Enabled
                =  state;
        }
        void setDecisionButton(bool state)
        {
            btnCancel.Enabled
               = btnSave.Enabled
               = state;
        }
        void setCRUDButton(bool insert, bool update, bool delete){
            this.btnInsert.Enabled = insert;
            this.btnUpdate.Enabled = update;
            this.btnDelete.Enabled = delete;
            this.btnRefresh.Enabled
                = this.btnRegisterTicket.Enabled
                = this.btnStream.Enabled
                = insert&&update&&delete;
        }
        void setInsertState()
        {
            setDefaultInputState(true);
            setDecisionButton(true);
            setCRUDButton(false, false, false);
        }
        void setUpdateState()
        {
            setDefaultInputState(true);
            setDecisionButton(true);
            setCRUDButton(false, false, false);
        }
        void setOnloadState()
        {
            setDefaultInputState(false);
            setDecisionButton(false);
            setCRUDButton(true, true, true);
        }
        void setDecisionState()
        {
            setOnloadState();
        }
        public class Student
        {
            public int Id;
            public string Name;
            public string Address;
        }
        bool checkToken()
        {
            if (Program.access_token.Length == 0)
                return false;
            return true;
        }
        void frmOnClose()
        {
            this.Hide();
            this.Close();
        }
        private void frmUserManager_Load(object sender, EventArgs e)
        {
            //Validate UI
            //Load data into data gridview
            //
            if (!checkToken()) frmOnClose();
            setOnloadState();
            Student[] students = {
                    new Student { Id = 1, Name = "Joe Rattz"            ,Address="Sriram Apartments"},
                    new Student { Id = 6, Name = "Ulyses Hutchens"      ,Address="Sriram Apartments"},
                    new Student { Id = 19, Name = "Bob Tanko"           ,Address="Sriram Apartments"},
                    new Student { Id = 45, Name = "Erin Doutensal"      ,Address="Sriram Apartments"},
                    new Student { Id = 1, Name = "Joe Rattz"            ,Address="Sriram Apartments"},
                    new Student { Id = 12, Name = "Bob Mapplethorpe"    ,Address="Sriram Apartments"},
                    new Student { Id = 17, Name = "Anthony Adams"       ,Address="Sriram Apartments"},
                    new Student { Id = 32, Name = "Dignan Stephens Mark",Address="Sriram Apartments"},
                    new Student { Id = 1232, Name = "Dignan Stephens"   ,Address="Sriram Apartments Henry Labamba Beligi"},
                    new Student { Id = 132, Name = "Neha Dhupia"        ,Address="Sriram Apartments 123456"},
                    new Student { Id = 132, Name = ""                   ,Address="Sriram Apartments 123456"},
                    new Student { Id = 133, Name = ""                   ,Address="Sriram Apartments 123456"},
                    new Student { Id = 134, Name = "Neha Dhupia"        ,Address=""},
                    new Student { Id = 134, Name = "Shradha Kapoor"     ,Address="Mumbai"}
                };
            DataTable dt = new DataTable("MyDataTable");
            dt.Columns.Add("MyColumn");
            dt.Columns.Add("MyColumn1");
            dt.Columns.Add("MyColumn2");
            Type type = typeof(Student);
            int a = type.GetProperties().Count();
            MessageBox.Show(Convert.ToString(a));
            foreach (Student value in students)
            {
                DataRow row = dt.NewRow();
                row[0] = value.Id;
                row[1] = value.Name;
                row[2] = value.Address;
                dt.Rows.Add(row);
            }
            //dataGridView.DataSource = dt;
            gridControl.DataSource = dt;
            txtVehicleId.Text = gridView.GetSelectedRows().ToString();
        }

        private void btnInsert_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setInsertState();
        }

        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setUpdateState();
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtVehicleId.Text = gridView.GetDataRow(gridView.GetSelectedRows()[0])[1].ToString();
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setDecisionState();
        }

        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setDecisionState();
        }

        private void btnStream_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //check exist
            frmCapture f = new frmCapture();
            f.Show();
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            Program.frm_sign_in.Show();
            this.Close();
        }

        private void btnRegisterTicket_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //txtVehicleId.Text = "98A212354";
            string vehicle_id = txtVehicleId.Text.Trim();
            string duration_time = Program.DURATION_TIME_REGISTER.ToString().Trim();
            PostServer api = new PostServer(vehicle_id, duration_time);
            this.response = api.fetch();
            if (this.response.status == 400)
            {
                MessageBox.Show(this.response.message);
                return;
            }
            else if (this.response.status == 200)
            {
                MessageBox.Show(this.response.message);
                frmUserManager f = new frmUserManager();
                f.Show();
                this.Hide();
                //this.Close();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmInsertKey f = new frmInsertKey();
            f.Show();
        }
    }
}