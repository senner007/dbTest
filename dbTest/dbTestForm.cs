using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper; // right click references, manage NuGet - install
using dbTest.People;

namespace dbTest
{

    public partial class dbTestForm : Form
    {
        public string GetCnString(string name)
        {
            // hent cnstring fra App.config
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
            // <connectionStrings>
            //    < add name="MyLocalSQL" connectionString="" providerName="System.Data.SqlClient" />
            //</connectionStrings>
        }
        public List<Merged> GetLikeFirstNameSortBy(string firstName, string sort)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GetCnString("MyLocalSQL")))
            {
                //var output = connection.Query<Merged>($"select distinct s.FirstName, s.Major, e.FirstName FROM students as s, employed as e").ToList();
                return connection.Query<Merged>($"select Status, Tlf, FirstNAme, LastName, Age, Major, '' as Company, '' as Salary from students " +
                                                      $"where FirstName LIKE '%{ firstName }%'" +
                                                      $"UNION " +
                                                      $"select Status, Tlf, FirstName, LastName, Age, '' as Major, Company, Salary from employed " +
                                                      $"where FirstName LIKE '%{ firstName }%' ORDER BY " + sort).ToList();

            }
        }

        public List<Merged> GetLikeAgeSortBy(uint age, string sort)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GetCnString("MyLocalSQL")))
            {
                //var output = connection.Query<Merged>($"select distinct s.FirstName, s.Major, e.FirstName FROM students as s, employed as e").ToList();
                return  connection.Query<Merged>($"select Status, Tlf, FirstNAme, LastName, Age, Major, '' as Company, '' as Salary from students " +
                                                      $"where Age LIKE '%{ age }%' " +
                                                      $"UNION " +
                                                      $"select Status, Tlf, FirstName, LastName, Age, '' as Major, Company, Salary from employed " +
                                                      $"where Age LIKE '%{ age }%' ORDER BY " + sort).ToList();

            }
        }

        List<Merged> merged = new List<Merged>();

        public dbTestForm()
        {
            //Skjul comuterens navn ved at indsætte dette i app.config connectionString 
            //Åbn  People table i cnstring
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var cnStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            cnStringsSection.ConnectionStrings["MyLocalSQL"].ConnectionString =
                "Data Source=" + Environment.MachineName + " \\SQLEXPRESS;Initial Catalog=People;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");

            InitializeComponent(); // init form
            AllocConsole(); // console output
            this.ActiveControl = textBox1;

        }
        [DllImport("kernel32.dll", SetLastError = true)] // Console output
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetLikeFirstNameSortBy(textBox1.Text, "LastName"); // søg i tekstboks - case insensitive; // indsæt liste
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBox2.Text, out uint number))
                Console.WriteLine("hello from number");
                dataGridView1.DataSource = GetLikeAgeSortBy(number, "Age"); // søg i tekstboks - case insensitive
        }

    }  
}




//INSERT INTO[dbo].[employed]
//([Tlf], [FirstName], [LastName], [Age], [Status], [Company], [Salary]) VALUES(11111111, N'Douglas', N'Crockford', 63, N'Employed', N'Yahoo', 1000)
//INSERT INTO[dbo].[employed]
//([Tlf], [FirstName], [LastName], [Age], [Status], [Company], [Salary]) VALUES(22222222, N'Thomas', N'A. Anderson', 53, N'Employed', N'', 100000)
//INSERT INTO[dbo].[employed]
//([Tlf], [FirstName], [LastName], [Age], [Status], [Company], [Salary]) VALUES(33333333, N'John', N'Resig', 34, N'Employed', N'', 50000000)

/*
 * 
 * 
 */

//INSERT INTO[dbo].[students]
//        ([Tlf], [FirstName], [LastName], [Age], [Status], [Major]) VALUES(44444444, N'John', N'Doe', 20, N'Student', N'Computer Science 101')
//INSERT INTO[dbo].[students]
//        ([Tlf], [FirstName], [LastName], [Age], [Status], [Major]) VALUES(55555555, N'Jane', N'Doe', 21, N'Student', N'Computer Science 201')
