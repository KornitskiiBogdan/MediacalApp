using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDatabase
{
    public class WriteToDatabase : IDisposable, IAsyncDisposable
    {
        private readonly SQLiteConnection _sqlConnection;
        public WriteToDatabase()
        {
            _sqlConnection = new SQLiteConnection("Data Source=MedicalDatabase.db");
            _sqlConnection.Open();
        }

        public bool Write(MedicalMark[] marks)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = _sqlConnection;
            command.CommandText = CreateInsertCommand(marks);

            return command.ExecuteNonQuery() == marks.Length;
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

        public void Dispose()
        {
            _sqlConnection.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await _sqlConnection.DisposeAsync();
        }
    }

    
}
