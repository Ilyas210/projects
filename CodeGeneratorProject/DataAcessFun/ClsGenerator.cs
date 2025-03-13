using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratorProject.DataAcessFun
{
    internal class ClsGenerator
    {
        public static string ConnectionString = "Server=.;Database=@DataBase;User Id=sa;Password=sa123456;";

        static public string GenerateDataTypes(string DataType)
        {

            switch (DataType)
            {
                case "bit":
                    return "bool";
                case "nvarchar":
                    return "string";
                case "date":
                case "datetime":
                case "datetime2":
                case "smalldatetime":
                    return "DateTime";
                case "decimal":
                case "money":
                case "numeric":
                case "smallmoney":
                    return "Decimal";
                case "tinyint":
                    return "Byte";
                case "smallint":
                    return "short";
            }
            
            return DataType;
        }

        static public string CheckDataTypeAndReturnValue(string DataType)
        {
            
            switch (ClsGenerator.GenerateDataTypes(DataType))
            {
                case "int":
                case "float":
                case "double":
                case "Decimal":
                case "short":
                    return "-1";
                case "Byte":
                    return "0";
                case "string":
                    return "\"\"";
                case "DateTime":
                    return "DateTime.MinValue";
                case "bool":
                    return "false";
            }           
            return "\"\"";
        }

        static public string GenerateCommandParameters(DataTable TableProperty)
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
                    TableProperty.Rows[i][0].ToString() + ");";
            }
            return Text;
        }
    }
}

