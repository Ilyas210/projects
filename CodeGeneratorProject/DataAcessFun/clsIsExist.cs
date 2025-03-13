using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratorProject.DataAcessFun
{
    internal class clsIsExist
    {
        static string GenerateHeaderFun(string TableSingularName, string PrimaryKey, string PKDataType)
        {          
            return "static public bool Is" + TableSingularName +"Exist (" + ClsGenerator.GenerateDataTypes(PKDataType) +
                " " + PrimaryKey + ")";
        }

        static string GenerateQuery (string TableName , string PrimaryKey)
        {
            string Text = " string Query = @\"select R = 1 from " + TableName
                + " where " + PrimaryKey + " = @" + PrimaryKey + ";\";";
            return Text;
        }
        static public string GenerateFun (string TableName, string TableSingleName, object TableProperty)
        {
            DataTable Table = TableProperty as DataTable;
            string PrimaryKey = Table.Rows[0][0].ToString();
            string Body = "\n";
            Body += GenerateHeaderFun(TableSingleName, PrimaryKey, Table.Rows[0][1].ToString());
            Body += "\n{\n";
            Body += "\t SqlConnection connection = new SqlConnection (ClsSettings.ConnectionString);";
            Body += "\n\t" + GenerateQuery(TableName, PrimaryKey);
            Body += "\n\tSqlCommand Command = new SqlCommand(Query,connection);";
            Body += "\n\t  Command.Parameters.AddWithValue(\"@" + PrimaryKey + "\"," + PrimaryKey + ");" ;
            Body += "\n\tbool isFound = false;";
            Body += "\n\tSqlDataReader reader = null;";
            Body += "\n\ttry\n\t {\n\tconnection.Open ();";
            Body += "\n\treader = Command.ExecuteReader ();";
            Body += "\n\tif (reader.Read( ))\n\t   {";
            Body += "\n\t\t isFound = true;";           
            Body += "\n\t }\n\t}";
            Body += "\n\tcatch\n\t {";
            Body += "\n\t\tisFound = false;\n\t }";
            Body += "\n\tfinally\n\t {";
            Body += "\n\t\tconnection.Close ();";
            Body += "\n\t\treader.Close ();\n\t }";
            Body += "\n\treturn isFound;\n\t}\n\n";

            return Body;
        }
    }
}
