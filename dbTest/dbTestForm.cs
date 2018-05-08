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
        public List<Student> GetPeople(string firstName)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GetCnString("MyLocalSQL")))
            {
                var output = connection.Query<Student>($"select * from Students where FirstName = '{ firstName }'").ToList();
                return output;
            }
        }

        List<Student> students = new List<Student>();

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

        }
        [DllImport("kernel32.dll", SetLastError = true)] // Console output
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            students = GetPeople(textBox1.Text); // søg i tekstboks - case insensitive
            dataGridView1.DataSource = students; // indsæt liste
        }

        //use People;

        //Drop Table students;

        //CREATE TABLE students(
        //    Tlf int not null PRIMARY KEY,
        //    FirstName varchar (50)
        //);

        //INSERT INTO students VALUES
        //(11111111, 'Douglas Obi-Wan Crockford'),
        //(22222222, 'Thomas A. Anderson');
        //(33333333, 'John  Resig');
    }
}
