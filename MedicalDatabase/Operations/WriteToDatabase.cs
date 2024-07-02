using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MedicalDatabase.Objects;

namespace MedicalDatabase.Operations
{
    public class WriteToDatabase : DatabaseOperationBase
    {
        internal WriteToDatabase() : base()
        {

        }

        private bool Write(string insertCommand, int countElements)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = SqlConnection;
            command.CommandText = insertCommand;

            return command.ExecuteNonQuery() == countElements;
        }

        public bool Write(MedicalReference[] references)
        {
            return Write(CreateInsertCommand(references), references.Length);
        }

        public bool Write(MedicalMark[] marks)
        {
            return Write(CreateInsertCommand(marks), marks.Length);
        }

        public bool Write(MedicalValue[] values)
        {
            return Write(CreateInsertCommand(values), values.Length);
        }

        private string CreateInsertCommand(MedicalMark[] marks)
        {
            string command = $"INSERT INTO MedicalMarks (Name, Unit, NameSubGroup, NameGroup) VALUES";
            for (int i = 0; i < marks.Length; i++)
            {
                var mark = marks[i];
                if (i == marks.Length - 1)
                {
                    command += $" ('{mark.Name}', '{mark.Unit}', '{mark.NameSubGroup}', '{mark.NameGroup}')";
                }
                else
                {
                    command += $" ('{mark.Name}', '{mark.Unit}', '{mark.NameSubGroup}', '{mark.NameGroup}'),";
                }
            }

            return command;
        }

        private string CreateInsertCommand(MedicalReference[] refereces)
        {
            string command = $"INSERT INTO MedicalReferences (Name, Value, IdParent) VALUES";
            for (int i = 0; i < refereces.Length; i++)
            {
                var referece = refereces[i];
                if (i == refereces.Length - 1)
                {
                    command += $" ('{referece.Name}', '{referece.Value}', '{referece.ParentId}')";
                }
                else
                {
                    command += $" ('{referece.Name}', '{referece.Value}', '{referece.ParentId}'),";
                }
            }

            return command;
        }

        private string CreateInsertCommand(MedicalValue[] values)
        {
            string command = $"INSERT INTO MedicalValues (Date, Value, IdParent) VALUES";
            for (int i = 0; i < values.Length; i++)
            {
                var value = values[i];
                if (i == values.Length - 1)
                {
                    command += $" ('{value.Date}', '{value.Value}', '{value.ParentId}')";
                }
                else
                {
                    command += $" ('{value.Date}', '{value.Value}', '{value.ParentId}'),";
                }
            }

            return command;
        }
    }


}
