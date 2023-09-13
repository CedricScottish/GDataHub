using GDataHub.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDataHub
{
    public class OracleDataBase : IDataBase
    {
        private static OracleDataBase _instance;
        private static object syncLock = new object();
        private static OracleConnection dbConnection;
        private OracleDataAdapter dbDataAdapter;
        private OracleCommand dbCommand;
        private string connectionString;

        public OracleDataBase()
        { }

        public OracleDataBase(string connString)
        {
            mData.dbConnectionStr = connString;
            connectionString = connString;
            dbConnection = new OracleConnection(connString);
        }

        //singleton
        public static OracleDataBase GetDataBase()
        {
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new OracleDataBase();
                    }
                }
            }
            return _instance;
        }

        public DataTable ExecuteQueryDataTable(string sqlString)
        {
            DataTable dt = new DataTable("Table");

            dbDataAdapter = new OracleDataAdapter(sqlString, dbConnection);
            dbDataAdapter.Fill(dt);

            return dt;
        }

        public DataTable ExecuteQueryDataTable(string sqlString, IDataParameter[] parameters)
        {
            DataTable dt = new DataTable("Table");
            dbDataAdapter = new OracleDataAdapter(sqlString, dbConnection);
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

            dbDataAdapter = new OracleDataAdapter(sqlString, dbConnection);
            dbDataAdapter.Fill(ds, tableName);

            return ds;
        }

        public DataSet ExecuteQueryDataSet(string sqlString, string tableName, IDataParameter[] parameters)
        {
            DataSet ds = new DataSet();

            dbDataAdapter = new OracleDataAdapter(sqlString, dbConnection);
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
            dbCommand = new OracleCommand(sqlString, dbConnection);

            Open();
            int i = dbCommand.ExecuteNonQuery();
            Close();

            return i;
        }

        public int ExecuteNonQuery(string sqlString, System.Data.IDataParameter[] parameters)
        {
            dbCommand = new OracleCommand(sqlString, dbConnection);

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
            OracleTransaction transaction;

            transaction = dbConnection.BeginTransaction();

            try
            {
                foreach (SqlQueryWithParameters query in sqlQueryWithParameters)
                {
                    dbCommand = new OracleCommand(query.sqlString, dbConnection);
                    dbCommand.Parameters.Clear();

                    foreach (IDataParameter parameter in query.parameters)
                    {
                        dbCommand.Parameters.Add(parameter);
                    }
                    dbCommand.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false;
            }
            return true;
        }

        public object ExecuteScalar(string sqlString)
        {
            object returnObject = null;

            dbCommand = new OracleCommand(sqlString, dbConnection);
            Open();
            returnObject = dbCommand.ExecuteScalar();
            Close();
            return returnObject;
        }

        public object ExecuteScalar(string sqlString, System.Data.IDataParameter[] parameters)
        {
            object returnObject = null;

            dbCommand = new OracleCommand(sqlString, dbConnection);
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
            IDataReader retDataReader = null;

            dbCommand = new OracleCommand(sqlString, dbConnection);
            Open();
            retDataReader = dbCommand.ExecuteReader();
            Close();

            return retDataReader;
        }

        public IDataReader ExecuteDataReader(string sqlString, IDataParameter[] parameters)
        {
            IDataReader retDataReader = null;

            dbCommand = new OracleCommand(sqlString, dbConnection);

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
            OracleTransaction transaction;
            object[] results = new object[sqlQueryWithParameters.Count];
            int counter = 0;

            transaction = dbConnection.BeginTransaction();

            try
            {
                foreach (SqlQueryWithParameters query in sqlQueryWithParameters)
                {
                    dbCommand = new OracleCommand(query.sqlString, dbConnection);
                    dbCommand.Parameters.Clear();

                    foreach (IDataParameter parameter in query.parameters)
                    {
                        dbCommand.Parameters.Add(parameter);
                    }
                    results[counter] = dbCommand.ExecuteScalar();
                    counter++;
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return null;
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
