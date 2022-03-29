using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SqlConn;
using parser;
using Settings;

namespace WilberriesParser_beta
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            string[][] input = QueryData.dataRead(conn, "SELECT * FROM wildberriesKeys WHERE 1", 3);
            conn.Close();
            string[][] output = Parser.wildberriesParse(input);
            conn.Open();
            QueryData.dataWrite(conn, output);
            conn.Close();
            Environment.FailFast("");
        }
    }
}