using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace dbTest
{
    public class DataAccess
    {
        public List<Student> GetPeople(string firstName)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("MyLocalSQL")))
            {
               var output = connection.Query<Student>($"select * from Students where FirstName = '{ firstName }'").ToList();
                return output;
            }
        }
    }
}
