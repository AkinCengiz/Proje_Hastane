using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public class DatabaseConnectionSentence
    {
        public SqlConnection Connection()
        {
            SqlConnection connection =
                new SqlConnection("Data Source=DESKTOP-U0NLI58\\MSSQLSERVER01;Initial Catalog=HastaneProje;Integrated Security=True");
            connection.Open();
            return connection;
        }

    }
}
