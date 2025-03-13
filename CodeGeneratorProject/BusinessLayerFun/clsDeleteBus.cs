using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGeneratorProject.BusinessLayerFun
{
    internal class clsDeleteBus
    {

        static public string GenerateFun(string TableSingleName, object tableProperty)
        {
            DataTable table = tableProperty as DataTable;
            string Text = "static public bool Delete"+ "(int " + table.Rows[0][0].ToString() + ")\r\n        {\r\n            return clsData" + TableSingleName + ".Delete"+TableSingleName +"(" + table.Rows[0][0].ToString() + ");\r\n        }";
            return Text;
        }
    }
}
