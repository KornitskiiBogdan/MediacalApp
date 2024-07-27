using System.Data;
using System.Data.SQLite;
using MedicalDatabase.Objects;

namespace MedicalDatabase.Operations
{
    public class WriteToDatabase : DatabaseOperationBase
    {
        public WriteToDatabase() : base()
        {

        }

        private bool Write(string insertCommand, int countElements)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = SqlConnection;
            command.CommandText = insertCommand;
            return command.ExecuteNonQuery() == countElements;
        }

        private bool Write(string insertCommand, int countElements, SQLiteParameter[] parameters)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = SqlConnection;
            command.CommandText = insertCommand;
            //TODO Проверить на нескольких файлах
            command.Parameters.AddRange(parameters);
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

        public bool Write(MedicalDocument[] documents)
        {
            return Write(CreateInsertCommand(documents, out var parameters), documents.Length, parameters);
        }

        private string CreateInsertCommand(MedicalDocument[] documents, out SQLiteParameter[] parameters)
        {
            string command = $"INSERT INTO MedicalDocuments (Name, Date, Image, Width, Height) VALUES";
            parameters = new SQLiteParameter[documents.Length];
            for (int i = 0; i < documents.Length; i++)
            {
                var mark = documents[i];
                if (i == documents.Length - 1)
                {
                    command += $" ('{mark.Name}', '{mark.Date}', @Image{i}, '{mark.Width}', '{mark.Height}')";
                }
                else
                {
                    command += $" ('{mark.Name}', '{mark.Date}', @Image{i}, '{mark.Width}', '{mark.Height}'),";
                }
                var param = new SQLiteParameter(DbType.Binary, mark.Image);
                param.ParameterName = $"@Image{i}";
                parameters[i] = param;
            }

            return command;
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
