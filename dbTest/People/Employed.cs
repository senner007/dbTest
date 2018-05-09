using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbTest.People
{
    public class Employed : Person
    {
        public string Company { get; set; }
        public int Salary { get; set; }
        public override string Status { get; set; } = "Employed"; 
    }
}
