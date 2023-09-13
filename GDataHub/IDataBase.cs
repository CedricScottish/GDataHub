using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDataHub
{

    public interface IDataBase
    {
        /// <summary>
        /// Verilen SQL Stringe Karşılık Düşen Verileri DataTable Olarak Getirir...
        /// </summary>
        /// <param name="sqlString"> SQL Sorgusu</param>
        /// <returns> Sonuç DataTable</returns>
        DataTable ExecuteQueryDataTable(string sqlString);

        /// <summary>
        /// Parametreleriyle Birlikte Verilen SQL Stringe  Karşılık Düşen Verileri DataTable Olarak Getirir...
        /// </summary>
        /// <param name="sqlString"> SQL Sorgusu</param>
        /// <param name="parameters"> DataBaseSwitcher.GetDataParameter(..) ile Oluşturulmuş Parametre Dizisi </param>
        /// <returns> Sonuç DataTable</returns>
        DataTable ExecuteQueryDataTable(string sqlString, IDataParameter[] parameters);

        /// <summary>
        /// Verilen SQL Stringe Karşılık Düşen Verileri DataSet İçerisinde TableName'de  Olarak Getirir...
        /// </summary>
        /// <param name="sqlString">SQL Sorgusu</param>
        /// <param name="tableName">Table Name </param>
        /// <returns>Sonuç DataSet</returns>
        DataSet ExecuteQueryDataSet(string sqlString, string tableName);

        /// <summary>
        /// Parametreleriyle Birlikte Verilen SQL Stringe Karşılık Düşen Verileri DataSet İçerisinde TableName'de  Olarak Getirir...
        /// </summary>
        /// <param name="sqlString">SQL Sorgusu</param>
        /// <param name="tableName">Table Name</param>
        /// <param name="parameters">DataBaseSwitcher.GetDataParameter(..) ile Oluşturulmuş Parametre Dizisi</param>
        /// <returns>Sonuç DataSet</returns>
        DataSet ExecuteQueryDataSet(string sqlString, string tableName, IDataParameter[] parameters);

        /// <summary>
        /// Verilen SQL Stringi Çalıştırır ve Etkilenen Kayıt Sayısını Döndürür...
        /// </summary>
        /// <param name="sqlString">SQL Sorgusu</param>
        /// <returns>Etkilenen Kayıt Sayısı</returns>
        int ExecuteNonQuery(string sqlString);

        /// <summary>
        /// Parametreleriyle Birlikte Verilen SQL Stringi Çalıştırır ve Etkilenen Kayıt Sayısını Döndürür...
        /// </summary>
        /// <param name="sqlString">SQL Sorgusu</param>
        /// <param name="parameters">DataBaseSwitcher.GetDataParameter(..) ile Oluşturulmuş Parametre Dizisi</param>
        /// <returns>Etkilenen Kayıt Sayısı</returns>
        int ExecuteNonQuery(string sqlString, IDataParameter[] parameters);

        /// <summary>
        /// Verilen Sorguları Parametereleriyle Transaction İçerisinde Çalıştırır...
        /// </summary>
        /// <param name="sqlQueryWithParameters">Sql ve Parametrelerden Oluşan Yapı (SqlQueryWithParameters)</param>
        /// <returns>Başarılı/Başarısız</returns>
        bool ExecuteQueriesWithTransaction(List<Utils.SqlQueryWithParameters> sqlQueryWithParameters);

        /// <summary>
        /// Verilen SQL Stringi Çalıştırır, Sorgu Sonucu Dönen Tablonun İlk Satır, İlk Sütunundaki Değeri Döndürür...
        /// </summary>
        /// <param name="sqlString">SQL Sorgusu</param>
        /// <returns>Dönüş Değeri</returns>
        object ExecuteScalar(string sqlString);

        /// <summary>
        ///  Verilen SQL Stringi Çalıştırır, Sorgu Sonucu Dönen Tablonun İlk Satır, İlk Sütunundaki Değeri Döndürür...
        /// </summary>
        /// <param name="sqlString">SQL Sorgusu</param>
        /// <param name="parameters">DataBaseSwitcher.GetDataParameter(..) ile Oluşturulmuş Parametre Dizisi</param>
        /// <returns>Dönüş Değeri</returns>
        object ExecuteScalar(string sqlString, IDataParameter[] parameters);

        /// <summary>
        /// Verilen SQL Stringi Çalıştırır,DataReader Nesnesi Döndürür
        /// </summary>
        /// <param name="sqlString">SQL Sorgusu</param>
        /// <returns>DataReader Nesnesi Döndürür</returns>
        IDataReader ExecuteDataReader(string sqlString);

        /// <summary>
        /// Parametreleriyle Birlikte Verilen SQL Stringi Çalıştırır,DataReader Nesnesi Döndürür
        /// </summary>
        /// <param name="sqlString">SQL Sorgusu</param>
        /// <param name="parameters">DataBaseSwitcher.GetDataParameter(..) ile Oluşturulmuş Parametre Dizisi</param>
        /// <returns>DataReader Nesnesi Döndürür</returns>
        IDataReader ExecuteDataReader(string sqlString, IDataParameter[] parameters);

        /// <summary>
        /// Verilen Sorguları Parametereleriyle Transaction İçerisinde Çalıştırır, Object Tipinde Dizi Döndürür.
        /// </summary>
        /// <param name="sqlQueryWithParameters">Sql ve Parametrelerden Oluşan Yapı (SqlQueryWithParameters)</param>
        /// <returns>Sorgu Sayısınca Object Tipinde Dizi Döndürür </returns>
        object[] ExecuteScalarWithTransaction(List<Utils.SqlQueryWithParameters> sqlQueryWithParameters);

        /// <summary>
        /// DataBase Connectionı Açar.
        /// </summary>
        void Open();

        /// <summary>
        /// DataBase Connectionı Kapar.
        /// </summary>
        void Close();


    }
}
