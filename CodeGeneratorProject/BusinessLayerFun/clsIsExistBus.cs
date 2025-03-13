using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratorProject.BusinessLayerFun
{
    internal class clsIsExistBus
    {
        static public string GenerateFun (string TableSingleName,object TableProperty)
        {
            DataTable Table = TableProperty as DataTable;
            string Text = "static public bool Is" + TableSingleName + "Exist(int " + Table.Rows[0][0] + ")\r\n        {\r\n            return clsData" + TableSingleName + ".Is" + TableSingleName + "Exist(" + Table.Rows[0][0] + ");\r\n        }";
            return Text;
        }
    }
}
