using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratorProject.DataAcessFun
{
    internal class clsGetList
    {


        static string GenerateHeaderFun(string TableName)
        {

            string Text = "static public DataTable GetList" + TableName + " ()";
            return Text;
        }

        static public string GenerateGetListFun(string TableName)
        {

            string text = "";

            text += GenerateHeaderFun(TableName);
            text += "\n{";
            text += "\n\tSqlConnection connection = new SqlConnection(ClsSettings.ConnectionString);";
            text += "\n\tstring Query = \"select * from " + TableName + "\";";
            text += "\n\tSqlCommand Command = new SqlCommand(Query,connection);";
            text += "\n\tDataTable Table = new DataTable() ;";
            text += "\n\tSqlDataReader Reader = null;";
            text += "\n\ttry";
            text += "\n\t{";
            text += "\n\t   connection.Open();";
            text += "\n\t   Reader = Command.ExecuteReader();";
            text += "\n\t   if (Reader.HasRows)\n\t   {";
            text += "\n\t\tTable.Load(Reader);\n\t   }";
            text += "\n\t}";
            text += "\n\tcatch";
            text += "\n\t{";
            text += "\n\t   Table = null;";
            text += "\n\t}";
            text += "\n\tfinally";
            text += "\n\t{";
            text += "\n\t   connection.Close();";
            text += "\n\t   if (Reader != null) {Reader.Close ();}";
            text += "\n\t}";
            text += "\n\treturn Table;";
            text += "\n\t}\n\n";

            return text;
        }

    }
}
