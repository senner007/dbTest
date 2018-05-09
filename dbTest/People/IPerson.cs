using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbTest
{
    public interface IPerson
    {
        int Tlf { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        uint Age { get; set; }
        string Status { get; set; }
    }
}
