﻿using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Data;

namespace DevelopersHub.RealtimeNetworking.Server
{

    public enum RequestsID
    {
        AUTH = 1  // 登录认证
    }

    class Database
    {

        #region MySQL
        
        private static MySqlConnection _mysqlConnection;
        private const string _mysqlServer = "127.0.0.1";
        private const string _mysqlUsername = "root";
        private const string _mysqlPassword = "";
        private const string _mysqlDatabase = "clash_of_clans";  // 设置数据库名

        public static MySqlConnection mysqlConnection
        {
            get
            {
                if (_mysqlConnection == null || _mysqlConnection.State == ConnectionState.Closed)
                {
                    try
                    {
                        _mysqlConnection = new MySqlConnection("SERVER=" + _mysqlServer + "; DATABASE=" + _mysqlDatabase + "; UID=" + _mysqlUsername + "; PASSWORD=" + _mysqlPassword + ";");
                        _mysqlConnection.Open();
                        Console.WriteLine("Connection established with MySQL database.");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Failed to connect the MySQL database.");
                    }
                }
                else if (_mysqlConnection.State == ConnectionState.Broken)
                {
                    try
                    {
                        _mysqlConnection.Close();
                        _mysqlConnection = new MySqlConnection("SERVER=" + _mysqlServer + "; DATABASE=" + _mysqlDatabase + "; UID=" + _mysqlUsername + "; PASSWORD=" + _mysqlPassword + ";");
                        _mysqlConnection.Open();
                        Console.WriteLine("Connection re-established with MySQL database.");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Failed to connect the MySQL database.");
                    }
                }
                return _mysqlConnection;
            }
        }

        public static void Demo_MySQL_1()
        {
            string query = String.Format("UPDATE table SET int_column = {0}, string_column = '{1}', datetime_column = NOW();", 123, "Hello World");
            using (MySqlCommand command = new MySqlCommand(query, mysqlConnection))
            {
                command.ExecuteNonQuery();
            }
        }

        public static void Demo_MySQL_2()
        {
            string query = String.Format("SELECT column1, column2 FROM table WHERE column3 = {0} ORDER BY column1 DESC;", 123);
            using (MySqlCommand command = new MySqlCommand(query, mysqlConnection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int column1 = int.Parse(reader["column1"].ToString());
                            string column2 = reader["column2"].ToString();
                        }
                    }
                }
            }
        }
        
        public static void AuthenticatePlayer(int clientID, string device)
        {

            // 注意 SQL 注入
            string query = "SELECT id FROM accounts WHERE device_id = @DeviceId; ";
            using (MySqlCommand command1 = new MySqlCommand(query, mysqlConnection))
            {
                command1.Parameters.AddWithValue("@DeviceId", device);
                using (MySqlDataReader reader1 = command1.ExecuteReader())
                {
                    if (reader1.HasRows)
                    {
                        while (reader1.Read())
                        {
                            long account_id = long.Parse(reader1["id"].ToString());
                            Console.WriteLine($"{device} is found, account_id:{account_id} ");
                            AuthenticationResponse(clientID, account_id);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{device} is not exist , start create...");
                        query = "INSERT INTO accounts (device_id) VALUES(@DeviceId) ;";
                        using (MySqlCommand command2 = new MySqlCommand(query, mysqlConnection))
                        {
                            reader1.Close();
                            command2.Parameters.AddWithValue("@DeviceId", device);

                            command2.ExecuteNonQuery();  // 更新数据库
                            Console.WriteLine($"{device} create success");
                            query = "SELECT id FROM accounts WHERE device_id = @DeviceId ;";
                            using (MySqlCommand command3 = new MySqlCommand(query, mysqlConnection))
                            {
                                command3.Parameters.AddWithValue("@DeviceId", device);

                                using (MySqlDataReader reader2 = command3.ExecuteReader())
                                {
                                    if (reader2.HasRows)
                                    {
                                        Console.WriteLine($"{device} is found");

                                        while (reader2.Read())
                                        {
                                            long account_id = long.Parse(reader2["id"].ToString());
                                            Console.WriteLine($"After create {device} is found, account_id:{account_id} ");
                                            AuthenticationResponse(clientID, account_id);

                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"After create {device} is not found ");

                                        AuthenticationResponse(clientID, false, device);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        private static void AuthenticationResponse(int clientID, long accountID)
        {
            Sender.TCP_Send(clientID, (int)RequestsID.AUTH, accountID);
        }

        private static void AuthenticationResponse(int clientID, bool isSuccess, string device)
        {
            Console.WriteLine($"no {device} and  create fail AuthenticationResponse");

            Sender.TCP_Send(clientID, (int)RequestsID.AUTH, -1);

        }


        #endregion

        #region SQL
        /*
        private static SqlConnection _sqlConnection;
        private const string _sqlServer = "server";
        private const string _sqlDatabase = "database";

        public static SqlConnection sqlConnection
        {
            get
            {
                if (_sqlConnection == null || _sqlConnection.State == ConnectionState.Closed)
                {
                    try
                    {
                        var connectionString = @"Server=localhost\" + _sqlServer + ";Database=" + _sqlDatabase + ";Initial Catalog=" + _sqlDatabase + ";Trusted_Connection=True;MultipleActiveResultSets=true";
                        _sqlConnection = new SqlConnection(connectionString);
                        _sqlConnection.Open();
                        Console.WriteLine("Connection established with SQL database.");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Failed to connect the SQL database.");
                    }
                }
                else if (_sqlConnection.State == ConnectionState.Broken)
                {
                    try
                    {
                        _sqlConnection.Close();
                        var connectionString = @"Server=localhost\" + _sqlServer + ";Database=" + _sqlDatabase + ";Initial Catalog=" + _sqlDatabase + ";Trusted_Connection=True;MultipleActiveResultSets=true";
                        _sqlConnection = new SqlConnection(connectionString);
                        _sqlConnection.Open();
                        Console.WriteLine("Connection re-established with SQL database.");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Failed to connect the SQL database.");
                    }
                }
                return _sqlConnection;
            }
        }

        public static void Demo_SQL_1()
        {
            string query = String.Format("UPDATE database.table SET int_column = {0}, string_column = '{1}', datetime_column = GETUTCDATE();", 123, "Hello World");
            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                command.ExecuteNonQuery();
            }
        }

        public static void Demo_SQL_2()
        {
            string query = String.Format("SELECT column1, column2 FROM database.table WHERE column3 = {0} ORDER BY column1 DESC;", 123);
            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int column1 = int.Parse(reader["column1"].ToString());
                            string column2 = reader["column2"].ToString();
                        }
                    }
                }
            }
        }
        */
        #endregion

    }
}