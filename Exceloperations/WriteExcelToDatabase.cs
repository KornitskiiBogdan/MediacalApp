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
        private readonly ReadFromDatabase _readFromDatabase;
        private readonly DeleteFromDatabase _deleteFromDatabase;
        public WriteExcelToDatabase()
        {
            _writeToDatabase = new WriteToDatabase();
            _readFromDatabase = new ReadFromDatabase();
            _deleteFromDatabase = new DeleteFromDatabase();
        }

        public bool WriteToDatabase(List<ExcelMedicalGroup> groups)
        {
            List<MedicalReference> references = new List<MedicalReference>();
            List<MedicalMark> marks = new List<MedicalMark>();

            var databaseMarks = _readFromDatabase.ReadMarks();
            foreach (var group in groups)
            {
                foreach (var subGroup in group.SubGroups)
                {
                    foreach (var m in subGroup.Elements)
                    {
                        var databaseMark = databaseMarks.First(x => x.Name == m.Name);
                        foreach (var r in m.ReferenceValues)
                        {
                            references.Add(new MedicalReference(0, databaseMark.Id, r.Name, r.Value));
                        }
                        marks.Add(new MedicalMark(0, m.Name, m.Unit ?? String.Empty, subGroup.Name, group.Name));
                    }
                }
            }
            //_deleteFromDatabase.Delete(NameTableInDatabase.MedicalReferences);
            return _writeToDatabase.Write(references.ToArray());
            //return _writeToDatabase.Write(marks.ToArray());

        }

    }
}
