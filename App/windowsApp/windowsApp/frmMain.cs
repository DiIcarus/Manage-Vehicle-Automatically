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

namespace windowsApp
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        private Form form;
        public frmMain()
        {
            InitializeComponent();
        }
        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }
        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            Program.frm_sign_in.Show();
            this.Close();
        }

        private void btnUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            form = this.CheckExists(typeof(frmUser));
            if (form == null)
            {
                frmUser f = new frmUser();
                //f.Text = "Nhap de";
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                form.Activate();
            }
        }

        private void btnVehicle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            form = this.CheckExists(typeof(frmVehicle));
            if (form == null)
            {
                frmVehicle f = new frmVehicle();
                //f.Text = "Nhap de";
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                form.Activate();
            }
        }

        private void btnCheckIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            form = this.CheckExists(typeof(frmCheckIn));
            if (form == null)
            {
                frmCheckIn f = new frmCheckIn();
                //f.Text = "Nhap de";
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                form.Activate();
            }
        }

        private void btnCheckOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            form = this.CheckExists(typeof(frmCheckOut));
            if (form == null)
            {
                frmCheckOut f = new frmCheckOut();
                //f.Text = "Nhap de";
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                form.Activate();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            form = this.CheckExists(typeof(frmTicket));
            if (form == null)
            {
                frmTicket f = new frmTicket();
                //f.Text = "Nhap de";
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                form.Activate();
            }
        }

        private void btnCamera_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCapture f = new frmCapture();
            f.Show();
        }

        private void btnInsertKey_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmInsertKey f = new frmInsertKey();
            f.Show();
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
        private void frmMain_Load(object sender, EventArgs e)
        {
            if (!checkToken()) frmOnClose();
        }
    }
}