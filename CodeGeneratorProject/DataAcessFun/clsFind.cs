using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratorProject.DataAcessFun
{
    internal class clsFind
    {
        static internal string GenerateFunctionName(string TableSingularName, string FindBy)
        {
            return "Find" + TableSingularName + "By" + FindBy;
        }

        static internal string GenerateParamaters(DataTable TableProperty, string FindBy)
        {
            int counter = 0;

            string text = " (";

            foreach (DataRow param in TableProperty.Rows)
            {
                if (param[0].ToString() != FindBy)
                    text += "ref ";
                text += ClsGenerator.GenerateDataTypes(param[1].ToString()) + " " + param[0].ToString() + ",";
            }
            return text.Substring(0, text.Length - 1) + ")";
        }
        static  string GenerateHeaderFun (string TableSingularName, string FindBy,DataTable TableProperty)
        {

            return "static public bool " + GenerateFunctionName(TableSingularName,FindBy) + GenerateParamaters(TableProperty, FindBy);
        }
        
        
        static public string GenerateQuery(string TableName, string FindBy)
        {
            string Query = "Select * from " + TableName + " where " + FindBy + " = @"+ FindBy;
            return Query;
        }

        
        static string AssignParameters (DataTable Parameters,string FindBy)
        {
            string text = "";

            for (int i = 0; i < Parameters.Rows.Count; i++)
            {
                if (Parameters.Rows[i].ToString() != FindBy)
                {
                    if (Parameters.Rows[i][2].ToString() == "YES")
                    {
                        text += "\n\t\t if (reader[\"" + Parameters.Rows[i][0].ToString() + "\"] == DBNull.Value)";
                        text += "\n\t\t {\n\t\t   " + Parameters.Rows[i][0].ToString() + " = "
                            + ClsGenerator.CheckDataTypeAndReturnValue(Parameters.Rows[i][1].ToString()) + "; \n\t\t }";
                        text += "\n\t\t else\n\t\t    ";
                    }
                    else text += "\n\t\t ";
                    text += Parameters.Rows[i][0].ToString() + " = (" + ClsGenerator.GenerateDataTypes(Parameters.Rows[i][1].ToString())
                        + ")reader[\"" 
                        + Parameters.Rows[i][0].ToString() +"\"];";                   
                }
            }
            return text;
        }
        static public string GenerateFunFind(string TableName,string TableSingularName,string FindBy, object TableProperty)
        {
            DataTable Table = TableProperty as DataTable;
            string Body = "\n";
            Body += GenerateHeaderFun(TableSingularName, FindBy, Table);
            Body += "\n{\n";
            Body += "\t SqlConnection connection = new SqlConnection (ClsSettings.ConnectionString);";
            Body += "\n\tstring Query = @\"" + GenerateQuery(TableName, FindBy) + "\";";
            Body += "\n\tSqlCommand cmd = new SqlCommand(Query,connection);";
            Body += "\n\tcmd.Parameters.AddWithValue(\"" + "@"+ FindBy +"\","+FindBy+");";
            Body += "\n\tbool isFound = false;";
            Body += "\n\tSqlDataReader reader = null;";
            Body += "\n\ttry\n\t {\n\tconnection.Open ();";
            Body += "\n\treader = cmd.ExecuteReader ();";
            Body += "\n\tif (reader.Read( ))\n\t   {";
            Body += "\n\t\t isFound = true;";
            Body += AssignParameters(Table, FindBy);
            Body += "\n\t }\n\t}";
            Body += "\n\tcatch\n\t {";
            Body += "\n\t\tisFound = false;\n\t }";
            Body += "\n\tfinally\n\t {";
            Body += "\n\t\tconnection.Close ();";
            Body += "\n\t\tif (reader != null) {reader.Close ();}\n\t }";
            Body += "\n\treturn isFound;\n\t}\n\n";

            return Body;
        }
    }
}
