using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace windowsApp
{
    public partial class frmCapture : DevExpress.XtraEditors.XtraForm
    {
        public class ResponseCheckIn
        {
            public string vehicle_id;
            public int status;
            public string message;
            public string name;
            public string gmail;
            public string phone_number;
            public string ticket_available;
        }
        public class ResponseCheckOut
        {
            public string vehicle_id;
            public int status;
            public string message;
            public string name;
            public string gmail;
            public string phone_number;
            public string ticket_available;
        }
        private ResponseCheckIn res_check_in;
        private ResponseCheckOut res_check_out;
        private FilterInfoCollection cameras;
        private VideoCaptureDevice cam;
        private Bitmap img;
        private Thread thPostServer;
        private string typeCheck=""; 
        public frmCapture()
        {
            InitializeComponent();
            cameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo info in cameras)
            {
                cbxCamera.Items.Add(info.Name);
            }
            cbxCamera.SelectedIndex = 0;
        }
        private void Cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            pictureBox.Image = bitmap;
            this.img = bitmap;
        }
        public class B64
        {
            public string base64;
        }
        private ResponseCheckOut postServerCheckOut(string Base64)
        {
            var obj = new B64
            {
                base64 = Base64,
            };
            string json = new JavaScriptSerializer().Serialize(obj);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5000/check-out-with-bot");
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
                ResponseCheckOut response = JsonConvert.DeserializeObject<ResponseCheckOut>(result);
                return response;
            }
        }

        private ResponseCheckIn postServerCheckIn(string Base64)
        {
            var obj = new B64
            {
                base64 = Base64,
            };
            string json = new JavaScriptSerializer().Serialize(obj);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5000/check-in-with-bot");
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
                ResponseCheckIn response = JsonConvert.DeserializeObject<ResponseCheckIn>(result);
                return response;
            }
        }
        string convertBitmap2Base64(Bitmap bImage)
        {
            MemoryStream ms = new MemoryStream();
            bImage.Save(ms, ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            var SigBase64 = Convert.ToBase64String(byteImage);
            return SigBase64;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (cam != null && cam.IsRunning)
            {
                cam.Stop();
            }
            cam = new VideoCaptureDevice(cameras[cbxCamera.SelectedIndex].MonikerString);
            cam.NewFrame += Cam_NewFrame1;
            cam.Start();
            timerRequest.Start();
        }

        private void Cam_NewFrame1(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            pictureBox.Image = bitmap;
            this.img = bitmap;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.OnClosed(e);
            if (cam != null && cam.IsRunning)
            {
                cam.Stop();
                timerRequest.Stop();
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            string str = convertBitmap2Base64(this.img);
            switch (typeCheck)
            {
                case "check-in":
                    this.res_check_in = postServerCheckIn(str);
                    txtVehicleId.Text = res_check_in.vehicle_id;
                    Program.vehicle_id= res_check_in.vehicle_id;
                    txtOwnerName.Text = res_check_in.name;
                    txtEmail.Text = res_check_in.gmail;
                    txtTicketAvailable.Text = res_check_in.ticket_available;
                    txtPhoneNumber.Text = res_check_in.phone_number;
                    if (this.res_check_in.status == 200)
                    {
                        MessageBox.Show("Vehicle" + res_check_in.vehicle_id + "not found,please type input!!");
                        frmInsertKey f = new frmInsertKey();
                        f.vehicle_id = res_check_in.vehicle_id;
                        f.Show();
                    }
                    if(res_check_in.status == 201)
                    {
                        MessageBox.Show("Success," +res_check_in.vehicle_id);
                    }
                    if (res_check_in.status == 400)
                    {
                        MessageBox.Show("Fail !!");
                    }
                    break;
                case "check-out":
                    this.res_check_out = postServerCheckOut(str);
                    txtVehicleId.Text = res_check_out.vehicle_id;
                    Program.vehicle_id = res_check_out.vehicle_id;
                    txtOwnerName.Text = res_check_out.name;
                    txtEmail.Text = res_check_out.gmail;
                    txtTicketAvailable.Text = res_check_out.ticket_available;
                    txtPhoneNumber.Text = res_check_out.phone_number;
                    if (this.res_check_out.status == 200)
                    {
                        MessageBox.Show("Vehicle" + res_check_out.vehicle_id + "not found,please type input!!");
                        frmInsertKey f = new frmInsertKey();
                        f.vehicle_id = res_check_out.vehicle_id;
                        f.Show();
                    }
                    if (res_check_out.status == 201)
                    {
                        MessageBox.Show("Success," + res_check_out.vehicle_id);
                    }
                    if (res_check_out.status == 400)
                    {
                        MessageBox.Show("Fail !!");
                    }
                    break;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            txtCurrentTime.Text = DateTime.Now.ToString("h:mm:ss tt");
        }

        private void frmCapture_Load(object sender, EventArgs e)
        {
            timer.Start();
            txtCurrentTime.Enabled = false;
        }

        private void timerRequest_Tick(object sender, EventArgs e)
        {
            //string str = convertBitmap2Base64(this.img);
            //postServer(str);
            //thPostServer = new Thread(() => postServer(str));
            //thPostServer.Start();
        }

        private void btnInsertKey_Click(object sender, EventArgs e)
        {
            frmInsertKey f = new frmInsertKey();
            f.Show();
        }

        private void rdCheckIn_CheckedChanged(object sender, EventArgs e)
        {
            typeCheck = "check-in";
        }

        private void rdCheckOut_CheckedChanged(object sender, EventArgs e)
        {
            typeCheck = "check-out";
        }
    }
}