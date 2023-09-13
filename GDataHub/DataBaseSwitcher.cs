using GDataHub.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data.OracleClient;

namespace GDataHub
{

    public class DataBaseSwitcher
    {
        public static IDataBase GetDataBase(Util.DBType pDbType, string pConnectionString)
        {
            mData.dbType = pDbType;
            switch (pDbType)
            {
                case Util.DBType.SQL:
                    mData.db = new MsSqlDataBase(pConnectionString);
                    return mData.db;

                case Util.DBType.ORACLE:
                    mData.db = new OracleDataBase(pConnectionString);
                    return mData.db;
            }
            return null;
        }

        public static IDataParameter GetDataParameter(string parameterName, SqlDbType type)
        {
            switch (mData.dbType)
            {
                case Util.DBType.SQL:
                    return new SqlParameter("@" + parameterName, type);

                case Util.DBType.ORACLE:
                    return new OracleParameter(":" + parameterName, ToOracleType(type));
            }
            return null;
        }

        public static IDataParameter GetDataParameter(string parameterName, SqlDbType pDataType, int pSize)
        {
            switch (mData.dbType)
            {
                case Util.DBType.SQL:
                    return new SqlParameter("@" + parameterName, pDataType, pSize);

                case Util.DBType.ORACLE:
                    return new OracleParameter(":" + parameterName, ToOracleType(pDataType), pSize);
            }
            return null;
        }

        public static OracleType ToOracleType(SqlDbType paramType)
        {
            switch (paramType)
            {
                case SqlDbType.BigInt:
                    return OracleType.Number;
                case SqlDbType.Binary:
                    return OracleType.Blob;
                case SqlDbType.Bit:
                    return OracleType.Byte;
                case SqlDbType.NChar:
                    return OracleType.NChar;
                case SqlDbType.Char:
                    return OracleType.Char;
                case SqlDbType.Date:
                    return OracleType.DateTime;
                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                    return OracleType.DateTime;
                case SqlDbType.Decimal:
                    return OracleType.Number;
                case SqlDbType.Float:
                    return OracleType.Float;
                case SqlDbType.Image:
                    return OracleType.Blob;
                case SqlDbType.Int:
                    return OracleType.Int32;
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    return OracleType.Number;
                case SqlDbType.NVarChar:
                    return OracleType.NVarChar;
                case SqlDbType.VarChar:
                    return OracleType.VarChar;
                case SqlDbType.SmallInt:
                    return OracleType.Int16;
                case SqlDbType.NText:
                case SqlDbType.Text:
                    return OracleType.LongVarChar;
                case SqlDbType.Time:
                    return OracleType.Timestamp;
                case SqlDbType.Timestamp:
                    return OracleType.Timestamp;
                case SqlDbType.TinyInt:
                    return OracleType.Byte;
                case SqlDbType.UniqueIdentifier:
                    return OracleType.VarChar;
                case SqlDbType.VarBinary:
                    return OracleType.LongRaw;
                case SqlDbType.Xml:
                    return OracleType.LongVarChar;
            }
            return OracleType.VarChar;
        }



    }
}
