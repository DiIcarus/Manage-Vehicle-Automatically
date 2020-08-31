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
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace windowsApp
{
    public partial class frmVehicle : DevExpress.XtraEditors.XtraForm
    {
        public class VehicleInfo
        {
            public string user_id;
            public string name_owner;
            public string gmail;
            public string phone_number;
            public string vehicle_id;
            public bool ticket_available;
            public int check_in_counter;
            public int check_out_counter;
            public int ticket_counter;
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
            public VehicleInfo[] info;
        }
        private ResponseTicketRegister response;
        public frmVehicle()
        {
            InitializeComponent();
        }
        public ResponseTicketRegister GetInfoAllUser()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Program.HOST + "/user/vehicle");
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
            dt.Columns.Add("Vehicle Id");
            dt.Columns.Add("Name User");
            dt.Columns.Add("Gmail");
            dt.Columns.Add("Phone number");
            dt.Columns.Add("Ticket Available");
            dt.Columns.Add("Check in counter");
            dt.Columns.Add("Check out counter");
            dt.Columns.Add("Ticket counter");
            foreach (VehicleInfo value in response.info)
            {
                DataRow row = dt.NewRow();
                row[0] = value.vehicle_id;
                row[1] = value.name_owner;
                row[2] = value.gmail;
                row[3] = value.phone_number;
                row[4] = value.ticket_available;
                row[5] = value.check_in_counter;
                row[6] = value.check_out_counter;
                row[7] = value.ticket_counter;
                dt.Rows.Add(row);
            }

            gridControl.DataSource = dt;
            gridControl.RefreshDataSource();
        }
        private void frmVehicle_Load(object sender, EventArgs e)
        {
            this.response = GetInfoAllUser();
            loadGridControl(this.response);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            frmRegisterTicket f = new frmRegisterTicket();
            DataRow row = gridView.GetDataRow(gridView.GetSelectedRows()[0]);
            f.Text = row[0].ToString();
            f.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.response = GetInfoAllUser();
            loadGridControl(this.response);
        }
    }
}