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
            txtVehicleId.Text = "98A212354";
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
    }
}