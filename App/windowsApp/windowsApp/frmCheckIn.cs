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

namespace windowsApp
{
    public partial class frmCheckIn : DevExpress.XtraEditors.XtraForm
    {
        public class CheckInInfo
        {
            public string id_check_in;
            public string vehicle_id;
            public string key_code;
            public string dates;
        }
        public class ResponseChecked
        {
            public int status;
            public string message;
            public CheckInInfo[] info;
        }
        private ResponseChecked response;
        public ResponseChecked GetInfoAllCheckedIn()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Program.HOST + "/user/check-in");
            httpWebRequest.ContentType = Program.HEADER_CONTENT_TYPE;
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("Authorization", Program.access_token);

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();//string json
                ResponseChecked response = JsonConvert.DeserializeObject<ResponseChecked>(result);
                return response;
            }
        }
        public void loadGridControl(ResponseChecked response)
        {
            if (response.info == null)
            {
                return;
            }
            DataTable dt = new DataTable("MyDataTable");
            dt.Columns.Add("Id Check In");
            dt.Columns.Add("Vehicle Id");
            dt.Columns.Add("Key Code");
            dt.Columns.Add("Date");
            foreach (CheckInInfo value in response.info)
            {
                DataRow row = dt.NewRow();
                row[0] = value.id_check_in;
                row[1] = value.vehicle_id;
                row[2] = value.key_code;
                row[3] = value.dates;
                dt.Rows.Add(row);
            }
            gridControl.DataSource = dt;
            gridControl.RefreshDataSource();
        }
        public frmCheckIn()
        {
            InitializeComponent();
        }

        private void frmCheckIn_Load(object sender, EventArgs e)
        {
            this.response = GetInfoAllCheckedIn();
            loadGridControl(this.response);
        }
    }
}