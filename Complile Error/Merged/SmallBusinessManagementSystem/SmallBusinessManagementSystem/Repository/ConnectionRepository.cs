﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallBusinessManagementSystem.Model;
using System.Data;
using System.Data.SqlClient;

namespace SmallBusinessManagementSystem.Repository
{
    public class ConnectionRepository
    {
        private SqlConnection _sqlConnection;
        private SqlCommand _sqlCommand;

        public string GetConnectionString()
        {
            return @"Server=DESKTOP-QMNU0HM\RAIHANPLAYGROUND; Database=Project1DB; Integrated Security = True";
        }

        public bool ConnectAndMakeSqlCommand(string commandString)
        {

            try
            {
                string _connectionString = @"Server=DESKTOP-QMNU0HM\RAIHANPLAYGROUND; Database=Project1DB; Integrated Security = True";
                _sqlConnection = new SqlConnection(_connectionString);
                _sqlCommand = new SqlCommand(commandString,_sqlConnection);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public void ExecuteNonQuery(string commandString)
        {
            ConnectAndMakeSqlCommand(commandString);
            _sqlConnection.Open();
            _sqlCommand.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        public SqlDataReader ExecuteQueryAndGetSqlDataReader(string commandString)
        {
            ConnectAndMakeSqlCommand(commandString);
            _sqlConnection.Open();
            SqlDataReader sqlDataReader = _sqlCommand.ExecuteReader();
            return sqlDataReader;
        }

        public void CloseConnection()
        {
            _sqlConnection.Close();
        }

    }
}
