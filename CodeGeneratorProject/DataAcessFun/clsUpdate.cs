using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratorProject.DataAcessFun
{
    internal class clsUpdate
    {
        static string GenerateHeaderFun(string TableSingleName, DataTable TableProperty)
        {
            string Text = "static public bool Update" + TableSingleName + " (";

            foreach (DataRow Row in TableProperty.Rows)
            {
                Text += ClsGenerator.GenerateDataTypes(Row[1].ToString()) + " " + Row[0].ToString() + ",";
            }
            Text = Text.Substring(0, Text.Length - 1) + ")";
            return Text;
        }

        static string GenerateQuery(string TableName, string TableSingleName, DataTable TableProperty)
        {
            string Text = "string Query = @\"UPDATE [dbo].[" + TableName + "]";
            Text += "\n\t\tSET ";

            for (int i = 1; i < TableProperty.Rows.Count; i++)
            {
                Text += "[" + TableProperty.Rows[i][0].ToString() + "] = @" + TableProperty.Rows[i][0].ToString() + "\n\t\t,";
            }

            Text = Text.Substring(0, Text.Length - 1) + "\n\t    Where ";

            Text += TableProperty.Rows[0][0].ToString() + " = @" + TableProperty.Rows[0][0].ToString() + ";\";";

            return Text;
        }
        static public string GenerateUpdateFun(string TableName, string TableSingleName, object TableProperty,string FindBy)
        {
            DataTable Table = new DataTable();
            Table = TableProperty as DataTable;
            string Text = GenerateHeaderFun(TableSingleName, Table);

            Text += "\n{";
            Text += "\n\tSqlConnection connection = new SqlConnection (ClsSettings.ConnectionString);";

            Text += "\n\t" + GenerateQuery(TableName, TableSingleName, Table);

            Text += "\n\n\tSqlCommand Command = new SqlCommand(Query , connection);";


            Text += "\n\t  Command.Parameters.AddWithValue(\"@" + FindBy + "\"," +
                FindBy + ");";
            Text += ClsGenerator.GenerateCommandParameters(Table);

            Text += "\n\tint Result = -1;";

            Text += "\n\ttry\n\t {\n\tconnection.Open ();";
            Text += "\n\tResult = Command.ExecuteNonQuery();";
            
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
