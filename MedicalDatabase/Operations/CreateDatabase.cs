using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDatabase.Operations
{
    public class CreateDatabase : DatabaseOperationBase
    {
        public void Create()
        {
            SQLiteCommand commandMarks = new SQLiteCommand();
            commandMarks.Connection = SqlConnection;
            commandMarks.CommandText = $"CREATE TABLE MedicalMarks1(Id INTEGER NOT NULL UNIQUE PRIMARY KEY AUTOINCREMENT, " +
                                       $"Name TEXT NOT NULL, " +
                                       $"Unit TEXT, " +
                                       $"NameSubGroup TEXT NOT NULL," +
                                       $" NameGroup TEXT NOT NULL)";
            commandMarks.ExecuteNonQuery();

            SQLiteCommand commandReference = new SQLiteCommand();
            commandReference.Connection = SqlConnection;
            commandReference.CommandText = $"CREATE TABLE MedicalReferences1(Id INTEGER NOT NULL UNIQUE PRIMARY KEY AUTOINCREMENT, " +
                                           $"Name TEXT NOT NULL, " +
                                           $"IdParent INTEGER NOT NULL, " +
                                           $"Value TEXT)";
            commandReference.ExecuteNonQuery();

            SQLiteCommand commandValues = new SQLiteCommand();
            commandValues.Connection = SqlConnection;
            commandValues.CommandText = $"CREATE TABLE MedicalValues1(Id INTEGER NOT NULL UNIQUE PRIMARY KEY AUTOINCREMENT, " +
                                        $"Date INTEGER NOT NULL, " +
                                        $"Value REAL NOT NULL, " +
                                        $"IdParent INTEGER NOT NULL)";
            commandValues.ExecuteNonQuery();
        }
    }
}
