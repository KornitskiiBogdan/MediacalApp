using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelOperations
{
    public record ExcelMedicalGroup(string Name, List<ExcelMedicalSubGroup> SubGroups);

    public record ExcelMedicalSubGroup(string Name, List<ExcelMedicalElement> Elements);

    public record ExcelMedicalElement(string Name, string? Unit, List<ExcelMedicalReferenceValues> ReferenceValues);

    public record ExcelMedicalReferenceValues(string Name, string? Value);

    public class ExcelRead
    {
        public const string Path = @"C:\Users\Bogdan\Downloads\referensy_1.xlsx";
        public List<ExcelMedicalGroup> Read(string path)
        {
            List<ExcelMedicalGroup> medicalGroups = new List<ExcelMedicalGroup>();

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            List<string> nameReferences = new List<string>();
            int indexEndReference = 0;
            ExcelMedicalGroup? currentGroup = null;
            {
                int indexColumnHeaderUnit = -1;
                for (int j = 1; j <= colCount; j++)
                {
                    var valueInCell = ReadValueInCell(xlRange, 1, j);
                    if (valueInCell == null)
                    {
                        continue;
                    }
                    if (j == 1)
                    {
                        currentGroup = new ExcelMedicalGroup(valueInCell, new List<ExcelMedicalSubGroup>());
                        medicalGroups.Add(currentGroup);
                    }
                    else if (valueInCell == "Единицы измерения")
                    {
                        indexColumnHeaderUnit = j;
                    }

                    if (indexColumnHeaderUnit != -1 && j > indexColumnHeaderUnit && valueInCell != "Источник")
                    {
                        nameReferences.Add(valueInCell);
                        indexEndReference = j;
                    }

                }
            }

            if (currentGroup is null)
            {
                return medicalGroups;
            }

            ExcelMedicalSubGroup? currentSubGroup = null;
            ExcelMedicalElement? currentElement = null;
            int indexReferences = 0;
            for (int i = 2; i <= rowCount; i++)
            {
                var name = ReadValueInCell(xlRange, i, 1);
                bool isGroup = ReadValueInCell(xlRange, i, 4) == "cat" && name is not null;
                if (isGroup)
                {
                    currentGroup = new ExcelMedicalGroup(name, new List<ExcelMedicalSubGroup>());
                    medicalGroups.Add(currentGroup);
                    continue;
                }

                if (name != null)
                {
                    currentSubGroup = new ExcelMedicalSubGroup(name, new List<ExcelMedicalElement>());
                    currentGroup.SubGroups.Add(currentSubGroup);
                }
                for (int j = 3; j <= colCount; j++)
                {
                    var valueInCell = ReadValueInCell(xlRange, i, j);

                    if (j == 3)
                    {
                        currentElement = new ExcelMedicalElement(valueInCell ?? string.Empty, ReadValueInCell(xlRange, i, j + 1),
                            new List<ExcelMedicalReferenceValues>());
                        indexReferences = 0;
                        currentSubGroup?.Elements.Add(currentElement);
                    }

                    if (j > 4 && j <= indexEndReference)
                    {
                        currentElement?.ReferenceValues.Add(new ExcelMedicalReferenceValues(nameReferences[indexReferences++], valueInCell));
                    }
                }
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            return medicalGroups;
        }

        private string? ReadValueInCell(Excel.Range xlRange, int indexRow, int indexColumn)
        {
            if (xlRange.Cells[indexRow, indexColumn] != null && xlRange.Cells[indexRow, indexColumn].Value2 != null)
            {
                return xlRange.Cells[indexRow, indexColumn].Value2.ToString();
            }
            return null;
        }
    }
}
