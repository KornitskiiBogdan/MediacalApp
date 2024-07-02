using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDatabase.Operations
{
    public class DeleteFromDatabase : DatabaseOperationBase
    {
        internal DeleteFromDatabase() : base()
        {

        }

        public void Delete(NameTableInDatabase nameTable)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = SqlConnection;
            command.CommandText = $"DELETE FROM {nameTable}";
            command.ExecuteNonQuery();
        }
    }
}
