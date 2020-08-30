﻿using System;
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

namespace windowsApp
{
    public partial class frmCapture : DevExpress.XtraEditors.XtraForm
    {
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
        private void postServer(string Base64)
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
            }
            //thPostServer.Abort();
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
            postServer(str);
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