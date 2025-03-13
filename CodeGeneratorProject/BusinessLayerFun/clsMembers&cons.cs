using CodeGeneratorProject.DataAcessFun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratorProject.BusinessLayerFun
{
    internal class clsMembers_cons
    {

        static string GenerateClassName (string TableSingleName)
        {
            return "cls" + TableSingleName + "\n{";
        }
        static string GenerateMembers (DataTable TableProberty)
        {
            string Text = "";
            foreach (DataRow Row in TableProberty.Rows)
            {
                Text += "\n\tpublic " + ClsGenerator.GenerateDataTypes( Row[1].ToString()) + " " + Row[0].ToString() + "{ get; set; }"; 
            }
            Text += "\n\tenMode _Mode;";
            return Text;
        }

        static string GenerateConstructor1 (string TableSingName,DataTable TableProperty)
        {
            string Text = "";
            Text += "\tcls" + TableSingName + "(";
            foreach (DataRow Row in TableProperty.Rows)
            {
                Text += ClsGenerator.GenerateDataTypes(Row[1].ToString()) + " " +Row[0].ToString() + "," ;
            }
            Text = Text.Substring(0,Text.Length - 1) + ")\n\t{";

            foreach (DataRow Row in TableProperty.Rows)
            {
                Text += "\n\t  this." + Row[0].ToString() + " = " + Row[0].ToString() + ";";
            }
            Text += "\n\t  _Mode = enMode.Update;";
            Text += "\n\t}";
            return Text;
        }

        static string GenerateConstructor2(string TableSingName, DataTable TableProperty)
        {
            string Text = "";
            Text += "\n\tpublic cls" + TableSingName + "()\n\t{";

            foreach (DataRow Row in TableProperty.Rows)
            {
                Text += "\n\t  this." + Row[0].ToString() + " = " + ClsGenerator.CheckDataTypeAndReturnValue(Row[1].ToString()) + ";";
            }
            Text += "\n\t  _Mode = enMode.AddNew;\n\t}";
            return Text;
        }

        static public string GenerateMembers_Constructors (string TableSingleName,object TableProperty)
        {
            DataTable dataTable = TableProperty as DataTable;
            string Text = "";
            Text += "\n\tenum enMode { AddNew = 1, Update = 2 };\n";
            Text += GenerateMembers(dataTable) + "\n\n";

            Text += GenerateConstructor1(TableSingleName, dataTable);
            Text += GenerateConstructor2(TableSingleName, dataTable);
            return Text;
        }

    }
}
