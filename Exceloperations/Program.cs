using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelOperations;

namespace ExcelOperations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var readExcel = new ExcelRead();
            var result = readExcel.Read(ExcelRead.Path);
            WriteExcelToDatabase write = new WriteExcelToDatabase();
            write.WriteToDatabase(result);
        }
    }
}
