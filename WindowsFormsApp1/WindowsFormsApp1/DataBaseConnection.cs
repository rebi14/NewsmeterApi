using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class DataBaseConnection
    {
        private SqlConnection connection = null;
        public SqlConnection _Connection
        {
            get
            {

                if (connection == null)
                    connection = new SqlConnection("Data Source=;Initial Catalog=News;Integrated Security=True");
                return connection;

            }
            private set
            {

            }
        }





        public DataBaseConnection()
        {

        }
    }
}
