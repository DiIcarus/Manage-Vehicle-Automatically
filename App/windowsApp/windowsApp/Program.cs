using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;

namespace windowsApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///
        public static string access_token = "";
        public static string HOST = "http://127.0.0.1:5000";
        public static string API_SIGN_IN = "/login";
        public static string API_REGISTER_MONTH_TICKET = "/register-ticket";
        public static string HEADER_CONTENT_TYPE = "application/json";
        public static string METHOD = "POST";
        public static int DURATION_TIME_REGISTER = 1000*60*60*24;
        public static bool Capture_response = false;
        public static frmSignIn frm_sign_in;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            Application.Run(new frmCapture());
        }
    }
}
