using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net.Http.Headers;

namespace CodeDataAcess
{
    public class ClsGeneratorData
    {
        static public DataTable GetDataBases()
        {
            SqlConnection connection = new SqlConnection(ClsSettings.ConnectionString);
            string Query = "SELECT name FROM sys. databases where not" +
                " (name = 'model' or name = 'master' or name = 'msdb' or name = 'tempdb');";

            SqlCommand cmd = new SqlCommand(Query, connection);
            SqlDataReader Reader = null;
            DataTable dataTable = new DataTable();
            try
            {
                connection.Open();
                Reader = cmd.ExecuteReader();
                if (Reader != null)
                {
                    dataTable.Load(Reader);
                }

            }
            catch
            {

            }
            finally
            {
                Reader.Close();
                connection.Close();
            }
            return dataTable;
        }


        static public DataTable GetTablesFromDataBase(string DataBase)
        {
            string connectionstring = "Server=.;Database=@DataBase;User Id=sa;Password=sa123456;";

            connectionstring = connectionstring.Replace("@DataBase", DataBase);
            SqlConnection connection = new SqlConnection(connectionstring);
            string Query = @"select * from (SELECT TABLE_NAME
                            FROM INFORMATION_SCHEMA.TABLES) t where not t.TABLE_NAME = 'sysdiagrams'";

            SqlCommand cmd = new SqlCommand(Query, connection);
            SqlDataReader Reader = null;
            DataTable dataTable = new DataTable();

            try
            {
                connection.Open();

                Reader = cmd.ExecuteReader();
                if (Reader != null)
                {
                    dataTable.Load(Reader);
                }
            }
            catch
            {

            }
            finally
            {
                Reader.Close();
                connection.Close();
            }
            return dataTable;
        }

        static public DataTable GetColumnNames(string DataBase, string TableName)
        {
            string connectionstring = "Server=.;Database=@DataBase;User Id=sa;Password=sa123456;";

            connectionstring = connectionstring.Replace("@DataBase", DataBase);
            SqlConnection connection = new SqlConnection(connectionstring);

            string Query = @"SELECT COLUMN_NAME
                             FROM INFORMATION_SCHEMA.COLUMNS 
                             WHERE TABLE_NAME = @TableName";

            SqlCommand cmd = new SqlCommand(Query, connection);
            cmd.Parameters.AddWithValue("@TableName", TableName);
            SqlDataReader Reader = null;
            DataTable dataTable = new DataTable();

            try
            {
                connection.Open();

                Reader = cmd.ExecuteReader();
                if (Reader != null)
                {
                    dataTable.Load(Reader);
                }
            }
            catch
            {

            }
            finally
            {
                Reader.Close();
                connection.Close();
            }
            return dataTable;

        }

        static public DataTable GetTableProPerty(string DataBase, string TableName)
        {
            string connectionstring = "Server=.;Database=@DataBase;User Id=sa;Password=sa123456;";

            connectionstring = connectionstring.Replace("@DataBase", DataBase);
            SqlConnection connection = new SqlConnection(connectionstring);

            string Query = @"SELECT COLUMN_NAME,DATA_TYPE,IS_NULLABLE
                             FROM INFORMATION_SCHEMA.COLUMNS
                             WHERE TABLE_NAME = @TableName";

            SqlCommand cmd = new SqlCommand(Query, connection);
            cmd.Parameters.AddWithValue("@TableName", TableName);
            SqlDataReader Reader = null;
            DataTable dataTable = new DataTable();

            try
            {
                connection.Open();

                Reader = cmd.ExecuteReader();
                if (Reader != null)
                {
                    dataTable.Load(Reader);
                }
            }
            catch
            {

            }
            finally
            {
                Reader.Close();
                connection.Close();
            }
            return dataTable;
        }

        static public List<string> GetDataTypes(string DataBase, string TableName)
        {
            string connectionstring = "Server=.;Database=@DataBase;User Id=sa;Password=sa123456;";

            connectionstring = connectionstring.Replace("@DataBase", DataBase);
            SqlConnection connection = new SqlConnection(connectionstring);

            string Query = @"SELECT DATA_TYPE
                             FROM INFORMATION_SCHEMA.COLUMNS
                             WHERE TABLE_NAME = @TableName";

            SqlCommand cmd = new SqlCommand(Query, connection);
            cmd.Parameters.AddWithValue("@TableName", TableName);
            SqlDataReader Reader = null;
            List<string> DataTypes = new List<string>();

            try
            {
                connection.Open();

                Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    DataTypes.Add(Reader.GetString(0));
                }
            }
            catch
            {

            }
            finally
            {
                Reader.Close();
                connection.Close();
            }
            return DataTypes;
        }

    }
}
