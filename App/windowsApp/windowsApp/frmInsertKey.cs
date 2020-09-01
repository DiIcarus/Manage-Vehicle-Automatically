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

namespace windowsApp
{
    public partial class frmInsertKey : DevExpress.XtraEditors.XtraForm
    {
        private string type;
        public string vehicle_id;
        public frmInsertKey()
        {
            InitializeComponent();
        }
        private void postServerCheckIn(RequestCheckIn obj)
        {
            string json = new JavaScriptSerializer().Serialize(obj);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5000/check-in-insert-data");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                MessageBox.Show(result);
                MessageBox.Show("Vehicle Passs");
            }
        }
        private void postServerCheckOut(RequestCheckOut obj)
        {
            string json = new JavaScriptSerializer().Serialize(obj);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5000/check-out-insert-data");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                MessageBox.Show(result);
                MessageBox.Show("Vehicle Passs");
            }
        }
        private void postServerGenKey(RequestGenKey obj)
        {
            string json = new JavaScriptSerializer().Serialize(obj);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5000/request-send-code");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                MessageBox.Show(result);
            }
        }
        private void rdCheckIn_CheckedChanged(object sender, EventArgs e)
        {
            lb1.Text = "Gmail:";
            lb2.Text = "Password:";
            type = "check-in";
        }

        private void rdCheckOut_CheckedChanged(object sender, EventArgs e)
        {
            lb1.Text = "Key Code:";
            lb2.Text = "Share Code:";
            type = "check-out";
        }

        private void btnGenKey_Click(object sender, EventArgs e)
        {
            postServerGenKey(new RequestGenKey { vehicle_id = vehicle_id });
        }

        private void btnSummit_Click(object sender, EventArgs e)
        {
            switch (type)
            {
                case "check-in":
                    postServerCheckIn(new RequestCheckIn { 
                        vehicle_id=vehicle_id,
                        phone_number="",
                        password=txtShareCode.Text.Trim(),
                        gmail=txtKeyCode.Text.Trim()
                    });
                    break;
                case "check-out":
                    postServerCheckOut(new RequestCheckOut
                    {
                        vehicle_id = vehicle_id,
                        key_code= txtKeyCode.Text.Trim(),
                        share_code= txtShareCode.Text.Trim(),
                    });
                    break;
            }
        }

        private void frmInsertKey_Load(object sender, EventArgs e)
        {

        }
    }
    public class RequestCheckIn
    {
        public string vehicle_id;
        public string password;
        public string phone_number;
        public string gmail;
    }
    public class ResponseCheckIn
    {
        public int status;
        public string message;
        public string vehicle_id;
    }
    public class RequestCheckOut
    {
        public string vehicle_id;
        public string key_code;
        public string share_code;
    }
    public class ResponseCheckOut
    {

    }
    public class RequestGenKey
    {
        public string vehicle_id;
    }
    public class ResponseRequestSendCode
    {
        public int status;
        public string message;
        public string share_key;
    }
}