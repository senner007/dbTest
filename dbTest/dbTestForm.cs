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


namespace dbTest
{
    public partial class dbTestForm : Form
    {
        List<Student> students = new List<Student>();

        public dbTestForm()
        {

            //Skjul comuterens navn ved at indsætte dette i app.config connectionString 
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");        
            connectionStringsSection.ConnectionStrings["MyLocalSQL"].ConnectionString = 
                "Data Source=" + System.Environment.MachineName + " \\SQLEXPRESS;Initial Catalog=People;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");

            InitializeComponent();
            AllocConsole();
            dataGridView1.DataSource = students;

        }
        [DllImport("kernel32.dll", SetLastError = true)] // Console output
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void button1_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();

            students =  db.GetPeople(textBox1.Text);
            dataGridView1.DataSource = students;

        }
    }
}
