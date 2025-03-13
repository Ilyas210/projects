using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratorProject.BusinessLayerFun
{
    internal class clsUpdateBus

    {
        static string GenerateHeaderFun(string TableSingleName)
        {
            return "\nbool _Update" + TableSingleName + " ()";
        }

        static public string GenerateFun(string TableSingleName, object TableProperty)
        {
            DataTable Table = TableProperty as DataTable;


            string Text = GenerateHeaderFun(TableSingleName) + "\n{\n";
            Text += "  return " + "clsData" + TableSingleName + ".Update" + TableSingleName
                + "(";

            for (int i = 0; i < Table.Rows.Count; i++)
            {
                Text += Table.Rows[i][0].ToString() + ",";
            }

            Text = Text.Substring(0, Text.Length - 1) + ");\n}";
         
            return Text;
        }



    }
}
