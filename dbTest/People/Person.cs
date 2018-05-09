using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbTest
{
    public abstract class Person : IPerson
    {
        public int Tlf { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public uint Age { get; set; }
        public virtual string Status { get; set; }
    }
}
