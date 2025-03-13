using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace CodeGeneratorProject.DataAcessFun
{
    internal class clsAddNew
    {
        static string GenerateHeaderFun(string TableSingleName, DataTable TableProperty)
        {
            string Text = "static public int AddNew" + TableSingleName + " (";

            for (int i = 1; i < TableProperty.Rows.Count; i++)
            {
                Text += ClsGenerator.GenerateDataTypes(TableProperty.Rows[i][1].ToString()) + " " + TableProperty.Rows[i][0].ToString() + ",";
            }
            Text = Text.Substring(0, Text.Length - 1) + ")";
            return Text;
        }

        static string GenerateQuery(string TableName, string TableSingleName, DataTable TableProperty)
        {
            string Query = "string Query = @\"INSERT INTO [dbo].[";
            Query += TableName + "](";

            for (int i = 1; i < TableProperty.Rows.Count; i++)
            {            
                Query += "[" + TableProperty.Rows[i][0].ToString() + "],";
            }
            Query = Query.Substring(0,Query.Length - 1) + ")VALUES\n";
            Query += "\t\t(";

            for (int i = 1; i < TableProperty.Rows.Count; i++)
            {
                Query += "@" + TableProperty.Rows[i][0].ToString() + ",";
            }
            Query = Query.Substring(0, Query.Length - 1) + ")\n\t\tselect SCOPE_IDENTITY();\";";

            return Query;

        }

        static string GenerateCommandParameters (DataTable TableProperty)
        {
            string Text = "";
            for (int i = 1; i < TableProperty.Rows.Count; i++)
            {
                if (TableProperty.Rows[i][2].ToString() == "YES")
                {
                    Text += "\n\tif (" + TableProperty.Rows[i][0].ToString() + " == " +
                        ClsGenerator.CheckDataTypeAndReturnValue(TableProperty.Rows[i][1].ToString()) + ")\n\t{\n\t";
                    Text += "\n\tCommand.Parameters.AddWithValue(\"@" +
                        TableProperty.Rows[i][0].ToString() + "\",DBNull.Value);\n\t}";
                    Text += "\n\telse\n\t  ";
                }
                else Text += "\n\t";

                Text += "Command.Parameters.AddWithValue(\"@" + TableProperty.Rows[i][0].ToString() + "\"," +
                    TableProperty.Rows[i][0].ToString() +");";
            }
            return Text;
        }
        static public string GenerateFun(string TableName,string TableSingleName, object TableProperty)
        {
            DataTable Table = new DataTable();
            Table = TableProperty as DataTable;
            string Text = GenerateHeaderFun(TableSingleName, Table);

            Text += "\n{";
            Text += "\n\tSqlConnection connection = new SqlConnection (ClsSettings.ConnectionString);";

            Text += "\n\t" + GenerateQuery(TableName, TableSingleName, Table);

            Text += "\n\n\tSqlCommand Command = new SqlCommand(Query , connection);";

            Text +=  GenerateCommandParameters(Table);

            Text += "\n\tint ID = -1;";

            Text += "\n\ttry\n\t {\n\tconnection.Open ();";
            Text += "\n\tobject Result = Command.ExecuteScalar();";
            Text += "\n\tif (Int32.TryParse(Result.ToString(),out int ResultID))\n\t   {";
            Text += "\n\t\t ID = ResultID;";
            
            Text += "\n\t }\n\t}";
            Text += "\n\tcatch\n\t {";
            Text += "\n\t\tID = -1;\n\t }";
            Text += "\n\tfinally\n\t {";
            Text += "\n\t\tconnection.Close ();";
            Text += "\n\t }";
            Text += "\n\treturn ID;\n\t}\n\n";

            return Text;
        }
    }
}
