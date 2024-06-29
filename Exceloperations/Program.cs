using Microsoft.Office.Interop.Excel;

namespace Exceloperations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var excelRead = new ExcelRead();
            excelRead.Read("C:\\Users\\Bogdan\\Downloads\\referensy_1.xlsx");
        }
    }
}
