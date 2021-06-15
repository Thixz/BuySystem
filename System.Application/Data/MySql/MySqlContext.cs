using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Data.MySql
{
    public class MySqlContext
    {
        private readonly string strConnection = "Server=localhost;DataBase=buysystem;Uid=root;Pwd=;SslMode=none";
        private MySqlConnection connection;

        public MySqlConnection Connect()
        {
            connection = new MySqlConnection(strConnection);
            connection.Open();
            return connection;
        }
    }
}
