using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDatabase.Operations
{
    public class ReadFromDatabase : DatabaseOperationBase
    {
        public ReadFromDatabase() : base()
        {

        }

        public void Read(NameTableInDatabase table)
        {
            string sqlExpression = $"SELECT * FROM {table}";

            SQLiteCommand command = new SQLiteCommand(sqlExpression, SqlConnection);

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {

                }
            }
        }

    }
}
