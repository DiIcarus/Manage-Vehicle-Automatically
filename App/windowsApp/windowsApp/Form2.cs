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
using Newtonsoft.Json;

namespace windowsApp
{
    public partial class Form2 : DevExpress.XtraEditors.XtraForm
    {
        public Form2()
        {
            InitializeComponent();
        }
        public class Student
        {
            public int Id;
            public string Name;
            public string Address;
        }
        public class Responnse
        {
            public string[] name;
            public int age;
            public string city;
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
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5000/gmail");
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
                Responnse response = JsonConvert.DeserializeObject<Responnse>(result);

                Student[] students = {
                    new Student { Id = 1, Name = "Joe Rattz"            ,Address="Sriram Apartments"},
                    new Student { Id = 6, Name = "Ulyses Hutchens"      ,Address="Sriram Apartments"},
                    new Student { Id = 19, Name = "Bob Tanko"           ,Address="Sriram Apartments"},
                    new Student { Id = 45, Name = "Erin Doutensal"      ,Address="Sriram Apartments"},
                    new Student { Id = 1, Name = "Joe Rattz"            ,Address="Sriram Apartments"},
                    new Student { Id = 12, Name = "Bob Mapplethorpe"    ,Address="Sriram Apartments"},
                    new Student { Id = 17, Name = "Anthony Adams"       ,Address="Sriram Apartments"},
                    new Student { Id = 32, Name = "Dignan Stephens Mark",Address="Sriram Apartments"},
                    new Student { Id = 1232, Name = "Dignan Stephens"   ,Address="Sriram Apartments Henry Labamba Beligi"},
                    new Student { Id = 132, Name = "Neha Dhupia"        ,Address="Sriram Apartments 123456"},
                    new Student { Id = 132, Name = ""                   ,Address="Sriram Apartments 123456"},
                    new Student { Id = 133, Name = ""                   ,Address="Sriram Apartments 123456"},
                    new Student { Id = 134, Name = "Neha Dhupia"        ,Address=""},
                    new Student { Id = 134, Name = "Shradha Kapoor"     ,Address="Mumbai"}
                };
                DataTable dt = new DataTable("MyDataTable");
                string[] data = response.name;
                dt.Columns.Add("MyColumn");
                dt.Columns.Add("MyColumn1");
                dt.Columns.Add("MyColumn2");
                Type type = typeof(Student);
                int a = type.GetProperties().Count();
                MessageBox.Show(Convert.ToString(a));
                foreach (Student value in students)
                {
                    DataRow row = dt.NewRow();
                    row[0] = value.Id;
                    row[1] = value.Name;
                    row[2] = value.Address;
                    dt.Rows.Add(row);
                }
                dataGridView.DataSource = dt;
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