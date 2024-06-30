using ExcelOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalDatabase;
using MedicalDatabase.Operations;
using MedicalDatabase.Objects;

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
            List<MedicalReference> references = new List<MedicalReference>();
            List<MedicalMark> marks = new List<MedicalMark>();

            foreach (var group in groups)
            {
                foreach (var subGroup in group.SubGroups)
                {
                    foreach (var m in subGroup.Elements)
                    {
                        foreach (var r in m.ReferenceValues)
                        {
                            references.Add(new MedicalReference(0, 0, r.Name, r.Value));
                        }
                        marks.Add(new MedicalMark(0, m.Name, m.Unit ?? String.Empty, subGroup.Name, group.Name));
                    }
                }
            }

            return _writeToDatabase.Write(references.ToArray());
            //return _writeToDatabase.Write(marks.ToArray());

        }

    }
}
