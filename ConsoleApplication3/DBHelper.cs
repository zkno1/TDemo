using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    public static class DBHelper
    {
        private static readonly string connstr = "server=localhost;Data Source=127.0.0.1;User Id=root;Password=;Database=Demotest;";
        public static List<T> Query<T>(string sql, Func<MySqlDataReader, T> handler, params MySqlParameter[] ps)
        {
            List<T> result = new List<T>();
            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddRange(ps);
                using (MySqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        T instance = handler(sdr);
                        result.Add(instance);
                    }
                }
            }
            return result;
        }
        public static bool Execute(string sql, params MySqlParameter[] p)
        {

            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                conn.Open();
                 MySqlCommand cmd = new  MySqlCommand(sql, conn);
                cmd.Parameters.AddRange(p);
                return cmd.ExecuteNonQuery() > 0 ? true : false;
            }
        }
        public static Object QueryScalar(string sql, params MySqlParameter[] ps)
        {

            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddRange(ps);
                object result = cmd.ExecuteScalar();
                return result;
            }

        }
    }
}
