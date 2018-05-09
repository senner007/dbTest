using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbTest.People
{
    public class Merged : Person
    {
        public string Major { get; set; }
        public override string Status { get; set; }
        public string Company { get; set; }
        public int Salary { get; set; }
    }
}
