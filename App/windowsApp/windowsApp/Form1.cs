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
namespace windowsApp
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        private FilterInfoCollection cameras;
        private VideoCaptureDevice cam;
        private Bitmap img;

        public Form1()
        {
            InitializeComponent();
            cameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo info in cameras)
            {
                cbxCamera.Items.Add(info.Name);
            }
            cbxCamera.SelectedIndex = 0;  
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(cam != null && cam.IsRunning)
            {
                cam.Stop();
            }cam = new VideoCaptureDevice(cameras[cbxCamera.SelectedIndex].MonikerString);
            cam.NewFrame += Cam_NewFrame;
            cam.Start();
        }
        string convertBitmap2Base64(Bitmap bImage)
        {
            System.IO.MemoryStream ms = new MemoryStream();
            bImage.Save(ms, ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            var SigBase64 = Convert.ToBase64String(byteImage); // Get Base64
            return SigBase64;
        }
        void postServer(string Base64)
        {
            var obj = new B64
            {
                base64 = Base64,
            };
            string json = new JavaScriptSerializer().Serialize(obj);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5000/testme");
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
            }
        }
        private void Cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            pictureBox.Image = bitmap;
            this.img = bitmap;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if(cam !=null && cam.IsRunning)
            {
                cam.Stop();
            }
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (cam != null && cam.IsRunning)
            {
                cam.Stop();
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            string str = convertBitmap2Base64(this.img);
            postServer(str);

        }
        public class B64
        {
            public string base64;
        }
    }
}
