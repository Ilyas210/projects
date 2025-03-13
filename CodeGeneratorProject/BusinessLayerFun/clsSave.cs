using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratorProject.BusinessLayerFun
{
    internal class clsSave
    {
        static public string GenerateFun (string TableSingleName)
        {
            string Text = "\npublic bool Save()\r\n        {\r\n            switch (_Mode)\r\n            {\r\n                case enMode.AddNew:\r\n                    if (this._AddNew" + TableSingleName + "())\r\n                    {\r\n                        _Mode = enMode.Update;\r\n                        return true;\r\n                    }\r\n                    else return false;\r\n                case enMode.Update:\r\n                    return this._Update"+ TableSingleName +"();\r\n            }\r\n\r\n            return false;\r\n        }";
            
           return Text;
        }
    }
}
