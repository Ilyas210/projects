using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratorProject.DataAcessFun
{
    internal class clsDelete
    {
        static string GenerateHeaderFun(string TableSingleName, string PrimaryKey, string DataType)
        {
            string Text = "static public bool Delete" + TableSingleName + " (";

            Text += ClsGenerator.GenerateDataTypes(DataType) + " " + PrimaryKey + ")";


            return Text;
        }

        static string GenerateQuery(string TableName, string PrimaryKey )
        {
            string Text = "string Query = \"DELETE FROM [dbo].[";
            Text += TableName + "] Where " + PrimaryKey + " = @" + PrimaryKey + ";\";";
            return Text;
        }

        static public string GenerateDeleteFun(string TableName, string TableSingleName, object TableProperty)
        {
            DataTable Table = TableProperty as DataTable;

            string PrimaryKey = Table.Rows[0][0].ToString();
            string Text = GenerateHeaderFun(TableSingleName, PrimaryKey, Table.Rows[0][1].ToString()) ;

            Text += "\n{";
            Text += "\n\tSqlConnection connection = new SqlConnection (ClsSettings.ConnectionString);";

            Text += "\n\t" + GenerateQuery(TableName, PrimaryKey);

            Text += "\n\tSqlCommand Command = new SqlCommand(Query , connection);";

            Text += "\n\t  Command.Parameters.AddWithValue(\"@" + PrimaryKey + "\"," + PrimaryKey + ");"; 
            Text += "\n\tint Result = 0;";

            Text += "\n\ttry\n\t {\n\tconnection.Open ();";
            Text += "\n\t  Result = Command.ExecuteNonQuery();";
            
            Text += "\n\t}";
            Text += "\n\tcatch\n\t {";
            Text += "\n\t\tResult = 0;\n\t }";
            Text += "\n\tfinally\n\t {";
            Text += "\n\t\tconnection.Close ();";
            Text += "\n\t }";
            Text += "\n\treturn Result > 0;\n\t}\n\n";

            return Text;
        }
    }
}
