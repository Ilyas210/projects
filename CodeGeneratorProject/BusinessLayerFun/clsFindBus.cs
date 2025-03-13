using CodeGeneratorProject.DataAcessFun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CodeGeneratorProject.BusinessLayerFun
{
    internal class clsFindBus
    {
        static int SearchRow (DataTable TableProperty,string FindBy)
        {
            int i = 0;
            for (; i < TableProperty.Rows.Count; i++)
            {
                if (TableProperty.Rows[i][0].ToString() == FindBy)
                    return i;
            }
            return -1;
        }
        static string GenerateHeaderFun(string TableSingularName, string FindBy, DataTable TableProperty)
        {

            return "\nstatic public cls" + TableSingularName + " Find" + TableSingularName + "by" + FindBy + " ("
                + ClsGenerator.GenerateDataTypes(TableProperty.Rows[SearchRow(TableProperty,FindBy)][1].ToString()) + " "
                + TableProperty.Rows[0][0].ToString() + ")";
        }

        static string DefinitionVariables (string FindBy,DataTable TableProperty) 
        {
            string Text = "\n";

            for (int i = 0;i < TableProperty.Rows.Count;i++)
            {
                if (FindBy != TableProperty.Rows[i][0].ToString())
                {
                    Text += ClsGenerator.GenerateDataTypes(TableProperty.Rows[i][1].ToString())
                        + " " + TableProperty.Rows[i][0].ToString() + " = " + 
                        ClsGenerator.CheckDataTypeAndReturnValue(TableProperty.Rows[i][1].ToString())
                        + "; ";
                }
                
            }
            return Text;
       
        }

        static string GenerateArguments (string FindBy,DataTable TableProperty)
        {
            int counter = 0;

            string text = " (";

            foreach (DataRow param in TableProperty.Rows)
            {
                if (param[0].ToString() != FindBy)
                    text += "ref ";
                text += param[0].ToString() + ",";
            }
            return text.Substring(0, text.Length - 1) + ")";
        }

        static string FillConstructor(DataTable TableProperty)
        {
            string text = "(";
            foreach (DataRow param in TableProperty.Rows)
            {
                text += param[0].ToString() + ",";
            }
            return text.Substring(0, text.Length - 1) + ")";
        }
        static internal string GenerateFun (string TableSingularName, string FindBy, object TableProperty)
        {
            DataTable table = TableProperty as DataTable;
            string Text = GenerateHeaderFun(TableSingularName,FindBy, table) + "\n{";

            Text += DefinitionVariables(FindBy, table);

            Text += "\n\n   if (clsData" + TableSingularName + "." + "Find" + TableSingularName + "By" + FindBy +
                GenerateArguments(FindBy, table) + ")\n{\n";
            Text += "\treturn new cls" + TableSingularName + FillConstructor(table) + ";\n}";
            Text += "\nelse return null;\n}\n";

            return Text;
        }
    }
}
