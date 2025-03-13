using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratorProject.BusinessLayerFun
{
    internal class clsGetListBus
    {
        static public string GenerateFun(string TableSingleName, string TableName)
        {
            string Text = "static public DataTable GetList" + TableName + "()\r\n        {\r\n            return clsData" + TableSingleName + ".GetList" + TableName + "();\r\n        }";
            return Text;
        }
    }
}
