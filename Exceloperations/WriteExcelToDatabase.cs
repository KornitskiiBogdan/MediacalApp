using ExcelOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalDatabase;

namespace ExcelOperations
{
    public class WriteExcelToDatabase
    {
        private readonly WriteToDatabase _writeToDatabase;
        public WriteExcelToDatabase()
        {
            _writeToDatabase = new WriteToDatabase();
        }

        public bool WriteToDatabase(List<ExcelMedicalGroup> groups)
        {
            List<MedicalMark> marks = new List<MedicalMark>();

            foreach (var group in groups)
            {
                foreach (var subGroup in group.SubGroups)
                {
                    foreach (var m in subGroup.Elements)
                    {
                        marks.Add(new MedicalMark(0, m.Name, m.Unit ?? String.Empty, subGroup.Name, group.Name));
                    }
                }
            }

            return _writeToDatabase.Write(marks.ToArray());

        }

    }
}
