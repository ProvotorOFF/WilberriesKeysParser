using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SqlConn
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "provotorov.beget.tech";
            int port = 3306;
            string database = "provotorov_calc";
            string username = "provotorov_calc";
            string password = "QsG%bq89";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }

    }
}
