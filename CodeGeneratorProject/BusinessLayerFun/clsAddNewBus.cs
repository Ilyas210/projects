using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratorProject.BusinessLayerFun
{
    internal class clsAddNewBus
    {
        static string GenerateHeaderFun (string TableSingleName)
        {
            return "\nbool _AddNew" + TableSingleName + " ()";
        }

        static public string GenerateFun (string TableSingleName,object TableProperty)
        {
            DataTable Table = TableProperty as DataTable;


            string Text = GenerateHeaderFun (TableSingleName) + "\n{\n";
            Text += "  " + Table.Rows[0][0].ToString() + " = " + "clsData" + TableSingleName + ".AddNew" + TableSingleName
                + "(";

           for (int i = 1; i < Table.Rows.Count; i++)
            {
                Text += Table.Rows[i][0].ToString() + ",";
            }

            Text = Text.Substring(0,Text.Length - 1) + ");";

            Text += "\n  return " + Table.Rows[0][0].ToString() + " != -1;\n}";

            return Text;
        }

    }
}
