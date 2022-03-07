﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SqlConn;
using parser;


namespace WilberriesParser_beta
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            string[][] input = QueryData.dataRead(conn, "SELECT * FROM wildberriesKeys WHERE 1", 3);
            string[][] output = new string[0][];
            conn.Close();
            Parser.wildberriesParse(input);
            Console.Read();
        }
    }
}