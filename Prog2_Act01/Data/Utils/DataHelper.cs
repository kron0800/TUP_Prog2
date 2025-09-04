using System.Data;
using Microsoft.Data.SqlClient;

namespace Prog2_Act01.Data.Utils
{
    public class DataHelper
    {
        private static DataHelper _instance;
        private SqlConnection _connection;
        private SqlTransaction _transaction;

        private DataHelper(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public static DataHelper GetInstance(string? connectionString = null)
        {
            if (_instance == null)
            {
                if (connectionString == null) { throw new Exception("Missing connectionString"); }
                _instance = new DataHelper(connectionString);
            }
            return _instance;
        }

        public void BeginTransaction()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
            _transaction = null;
            _connection.Close();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction = null;
            _connection.Close();
        }

        public void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();
                var cmd = new SqlCommand(query, _connection, _transaction);
                cmd.CommandType = CommandType.Text;
                dt.Load(cmd.ExecuteReader());
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                dt = null;
            }
            //finally
            //{
            //    _connection.Close();
            //}
            return dt;
        }

        public DataTable ExecuteSPReader(string sp, List<Parameters>? parameters = null) // TODO: agregar parametros
        {
            DataTable dt = new DataTable();
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();
                var cmd = new SqlCommand(sp, _connection, _transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                if(parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                    }
                }

                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                dt = null;
            }
            return dt;
        }

        public int ExecuteSPNonQuery(string sp, List<Parameters>? parameters = null) // TODO: agregar parametros
        {
            int result = 0;
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();
                var cmd = new SqlCommand(sp, _connection, _transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                    }
                }

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = -1;
            }
            return result;
        }
    }
}
