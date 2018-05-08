using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            InitializeComponent();
            dataGridView1.DataSource = students;
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();

            students =  db.GetPeople(textBox1.Text);
            dataGridView1.DataSource = students;
        }
    }
}
