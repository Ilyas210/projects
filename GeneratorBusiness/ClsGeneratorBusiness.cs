using CodeDataAcess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorBusiness
{
    public class ClsGeneratorBusiness
    {
       
        
        
        static public DataTable GetDataBases ()
        {            
            return ClsGeneratorData.GetDataBases ();
        }

        static public DataTable GetTables (string Database)
        {
            return ClsGeneratorData.GetTablesFromDataBase (Database);
        }

        static public DataTable GetColumns (string DataBase,string TableName)
        {
            return ClsGeneratorData.GetColumnNames(DataBase, TableName);
        }
        static public DataTable GetTableProperty (string DataBase, string TableName)
        {
            return ClsGeneratorData.GetTableProPerty(DataBase, TableName);
        }
        static public List <string> GetDataTypes (string DataBase, string TableName)
        {
            return ClsGeneratorData.GetDataTypes(DataBase, TableName);
        }
    }
}
