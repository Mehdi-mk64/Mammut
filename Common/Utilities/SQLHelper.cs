using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{

    public delegate void ReadRowHandler(System.Data.SqlClient.SqlDataReader reader);
   


    public sealed class SQLHelper
    {
        private static string connrctionString;
        private static SQLHelper _instance;
        private System.Data.SqlClient.SqlConnection _connection;
        public System.Data.SqlClient.SqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new System.Data.SqlClient.SqlConnection()
                    {
                        ConnectionString = connrctionString
                    };
                }
                return _connection;
            }
        }

        public static SQLHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SQLHelper(connrctionString);
                return _instance;

            }
        }

        private SQLHelper(string connectionString)
        {
            connrctionString = connectionString;
        }


        private System.Data.SqlClient.SqlCommand CreateCommand(string commandText)
        {
            return CreateCommand(commandText, System.Data.CommandType.Text);
        }

        private System.Data.SqlClient.SqlCommand CreateCommand(string commandText, System.Data.CommandType commandType)
        {
            return CreateCommand(commandText, commandType, null);

        }


        private System.Data.SqlClient.SqlCommand CreateCommand(string commandText, System.Data.CommandType commandType, params System.Data.SqlClient.SqlParameter[] parameters)
        {

            if (Connection.State == System.Data.ConnectionState.Closed)
                Connection.Open();

            System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand()
            {
                Connection = Connection,

                CommandText = commandText,
                CommandType = commandType
            };

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }


            return command;


        }




        public System.Data.DataTable GetDataTable(string commandText)
        {
            return GetDataTable(commandText, System.Data.CommandType.Text);
        }

        public System.Data.DataTable GetDataTable(string commandText, System.Data.CommandType commandType)
        {
            return GetDataTable(commandText, commandType, null);
        }
        public System.Data.DataTable GetDataTable(string commandText, System.Data.CommandType commandType, params System.Data.SqlClient.SqlParameter[] parameters)
        {


            try
            {
                using (System.Data.SqlClient.SqlCommand command = CreateCommand(commandText, commandType, parameters))
                {
                    System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.Load(reader);
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }





        public void GetDataReader(string commandText, ReadRowHandler handler)
        {

            GetDataReader(commandText, System.Data.CommandType.Text, handler);
        }

        public void GetDataReader(string commandText, System.Data.CommandType commandType, ReadRowHandler handler)
        {
            GetDataReader(commandText, commandType, handler, null);
        }

        public void GetDataReader(string commandText, System.Data.CommandType commandType, ReadRowHandler handler, params System.Data.SqlClient.SqlParameter[] parameters)
        {

            try
            {
                using (System.Data.SqlClient.SqlCommand command = CreateCommand(commandText, commandType, parameters))
                {
                    System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        handler?.Invoke(reader);
                    }
                    reader.Close();
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
                }
            }
            catch (Exception ex)
            {

                throw ;
            }


        }



        public T GetScalarData<T>(string commandText)
            where T : IEquatable<T>
        {
            return GetScalarData<T>(commandText, System.Data.CommandType.Text);
        }

        public T GetScalarData<T>(string commandText, System.Data.CommandType commandType)
            where T : IEquatable<T>
        {
            return GetScalarData<T>(commandText, commandType, null);
        }
        public T GetScalarData<T>(string commandText, System.Data.CommandType commandType, params System.Data.SqlClient.SqlParameter[] parameters)
            where T : IEquatable<T>
        {
            try
            {
                using (System.Data.SqlClient.SqlCommand command = CreateCommand(commandText, commandType, parameters))
                {
                    object obj = command.ExecuteScalar();
                    if (typeof(T) != obj.GetType())
                    {
                        throw new Exception("نوع داده ارسالی با داده دریافت شده همخوانی ندارد");
                    }
                    return (T)(obj);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }





        public void ExecuteNonQuery(string commandText)

        {
            ExecuteNonQuery(commandText, System.Data.CommandType.Text);
        }

        public void ExecuteNonQuery(string commandText, System.Data.CommandType commandType)
        {
            ExecuteNonQuery(commandText, commandType, null);
        }
        public void ExecuteNonQuery(string commandText, System.Data.CommandType commandType, params System.Data.SqlClient.SqlParameter[] parameters)
        {

            try
            {

                using (System.Data.SqlClient.SqlCommand command = CreateCommand(commandText, commandType, parameters))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }














    }
}
