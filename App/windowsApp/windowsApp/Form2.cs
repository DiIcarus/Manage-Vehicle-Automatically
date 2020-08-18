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
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;

namespace windowsApp
{
    public partial class Form2 : DevExpress.XtraEditors.XtraForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var obj = new Lad
            {
                firstName = "Markoff",
                lastName = "Chaney",
                dateOfBirth = new MyDate
                {
                    year = 1901,
                    month = 4,
                    day = 30
                }
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
        void get()
        {
            //var responseString = await response.Content.ReadAsStringAsync();


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://127.0.0.1:5000/");
            HttpResponseMessage response = client.GetAsync("/").Result;
            string emp = response.Content.ReadAsStringAsync().Result;

            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            Employee routes_list = json_serializer.Deserialize<Employee>(emp.Trim());
            MessageBox.Show(routes_list.name);
            MessageBox.Show(routes_list.age.ToString());
            MessageBox.Show(routes_list.city);
        }
    }
    public class Employee
    {
        public string name;
        public int age;
        public string city;
        Employee()
        {
            name = "son";
            age = 33;
            city = "Ha Noi";
        }
        
        string getName()
        {
            return name;
        }
        void setName(string test)
        {
            this.name = test;
        }
        int getAge()
        {
            return age;
        }
        void setAge(int test)
        {
            this.age = test;
        }
        string getCity()
        {
            return city;
        }
        void setCity(string test)
        {
            this.city = test;
        }
    }
    public class MyDate
    {
        public int year;
        public int month;
        public int day;
    }

    public class Lad
    {
        public string firstName;
        public string lastName;
        public MyDate dateOfBirth;
    }
}