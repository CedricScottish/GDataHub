using GDataHub.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GDataHub
{
    public class MsSqlDataBase : IDataBase
    {
        private static MsSqlDataBase _instance;
        private static object syncLock = new object();
        private static SqlConnection dbConnection;
        private SqlDataAdapter dbDataAdapter;
        private SqlCommand dbCommand;
        private string connectionString;

        public MsSqlDataBase()
        { }

        public MsSqlDataBase(string connString)
        {
            mData.dbConnectionStr = connString;
            connectionString = connString;
            dbConnection = new SqlConnection(connString);
        }

        //singleton
        public static MsSqlDataBase GetDataBase()
        {
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new MsSqlDataBase();
                    }
                }
            }
            return _instance;
        }

        public DataTable ExecuteQueryDataTable(string sqlString)
        {
            DataTable dt = new DataTable("Table");

            dbDataAdapter = new SqlDataAdapter(sqlString, dbConnection);
            dbDataAdapter.Fill(dt);

            return dt;
        }

        public DataTable ExecuteQueryDataTable(string sqlString, IDataParameter[] parameters)
        {
            DataTable dt = new DataTable("Table");
            dbDataAdapter = new SqlDataAdapter(sqlString, dbConnection);
            dbDataAdapter.SelectCommand.CommandTimeout = 0;

            foreach (IDataParameter parameter in parameters)
            {
                dbDataAdapter.SelectCommand.Parameters.Add(parameter);
            }
            dbDataAdapter.Fill(dt);

            return dt;
        }

        public DataSet ExecuteQueryDataSet(string sqlString, string tableName)
        {
            DataSet ds = new DataSet();

            dbDataAdapter = new SqlDataAdapter(sqlString, dbConnection);
            dbDataAdapter.Fill(ds, tableName);

            return ds;
        }

        public DataSet ExecuteQueryDataSet(string sqlString, string tableName, IDataParameter[] parameters)
        {
            DataSet ds = new DataSet();

            dbDataAdapter = new SqlDataAdapter(sqlString, dbConnection);
            dbDataAdapter.SelectCommand.CommandTimeout = 0;

            foreach (IDataParameter parameter in parameters)
            {
                dbDataAdapter.SelectCommand.Parameters.Add(parameter);
            }
            dbDataAdapter.Fill(ds, tableName);

            return ds;
        }

        public int ExecuteNonQuery(string sqlString)
        {
            dbCommand = new SqlCommand(sqlString, dbConnection);

            Open();
            int i = dbCommand.ExecuteNonQuery();
            Close();

            return i;
        }

        public int ExecuteNonQuery(string sqlString, IDataParameter[] parameters)
        {
            dbCommand = new SqlCommand(sqlString, dbConnection);

            foreach (IDataParameter parameter in parameters)
            {
                dbCommand.Parameters.Add(parameter);
            }

            Open();
            int i = dbCommand.ExecuteNonQuery();
            Close();

            return i;
        }

        public bool ExecuteQueriesWithTransaction(List<SqlQueryWithParameters> sqlQueryWithParameters)
        {
            try
            {
                Open();

                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (SqlQueryWithParameters query in sqlQueryWithParameters)
                    {
                        dbCommand = new SqlCommand(query.sqlString, dbConnection);
                        dbCommand.Parameters.Clear();

                        foreach (IDataParameter parameter in query.parameters)
                        {
                            dbCommand.Parameters.Add(parameter);
                        }
                        dbCommand.ExecuteNonQuery();
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                Close();
                return false;
            }
            finally
            {
                Close();
            }

            return true;
        }

        public object ExecuteScalar(string sqlString)
        {
            object returnObject = null;

            dbCommand = new SqlCommand(sqlString, dbConnection);
            Open();
            returnObject = dbCommand.ExecuteScalar();
            Close();
            return returnObject;
        }

        public object ExecuteScalar(string sqlString, IDataParameter[] parameters)
        {
            object returnObject = null;

            dbCommand = new SqlCommand(sqlString, dbConnection);
            dbCommand.Parameters.Clear();

            foreach (IDataParameter parameter in parameters)
            {
                dbCommand.Parameters.Add(parameter);
            }

            Open();
            returnObject = dbCommand.ExecuteScalar();
            Close();

            return returnObject;
        }

        public IDataReader ExecuteDataReader(string sqlString)
        {
            SqlDataReader retDataReader = null;

            dbCommand = new SqlCommand(sqlString, dbConnection);
            Open();
            retDataReader = dbCommand.ExecuteReader();
            Close();

            return retDataReader;
        }

        public IDataReader ExecuteDataReader(string sqlString, IDataParameter[] parameters)
        {
            SqlDataReader retDataReader = null;

            dbCommand = new SqlCommand(sqlString, dbConnection);

            foreach (IDataParameter parameter in parameters)
            {
                dbCommand.Parameters.Add(parameter);
            }

            Open();
            retDataReader = dbCommand.ExecuteReader();
            Close();

            return retDataReader;
        }

        public object[] ExecuteScalarWithTransaction(List<SqlQueryWithParameters> sqlQueryWithParameters)
        {
            object[] results = new object[sqlQueryWithParameters.Count];
            int counter = 0;

            try
            {
                Open();

                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (SqlQueryWithParameters query in sqlQueryWithParameters)
                    {
                        dbCommand = new SqlCommand(query.sqlString, dbConnection);
                        dbCommand.Parameters.Clear();

                        foreach (SqlParameter parameter in query.parameters)
                        {
                            dbCommand.Parameters.Add(parameter);
                        }
                        results[counter] = dbCommand.ExecuteScalar();

                        counter++;

                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                Close();
                return null;
            }
            finally
            {
                Close();
            }

            return results;
        }

        public void Open()
        {
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }
        }

        public void Close()
        {
            if (dbConnection.State != ConnectionState.Closed)
            {
                dbConnection.Close();
            }
        }

    }
}
