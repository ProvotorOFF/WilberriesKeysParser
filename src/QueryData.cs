using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlConn;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace SqlConn
{
    class QueryData
    {
        public static string[][] dataRead(MySqlConnection conn, string sql, int columns)
        {
            string[][] dataBase = new string[0][];
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] row = new string[0];
                        for (int i = 0; i < columns;  ++i) {
                            row = row.Concat(new[] { reader.GetString(i) }).ToArray();
                        }
                        dataBase = dataBase.Concat(new[] { row }).ToArray();
                    }
                    Console.WriteLine("Получение данных из MySQL завершено.");
                }
            }
            return dataBase;
        }
        public static void dataWrite (MySqlConnection conn, string[][] data)
        {
            for (int i = 0; i < data.Length; ++i)
            {
                string sql = "Update wildberriesKeys set results ='" + data[i][1] + "' where vendorCode='" + data[i][0] + "'";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Запись в базу данных MySQL завершена.");
        }
    }
}