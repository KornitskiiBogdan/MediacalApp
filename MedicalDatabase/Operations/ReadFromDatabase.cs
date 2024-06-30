using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalDatabase.Objects;

namespace MedicalDatabase.Operations
{
    public class ReadFromDatabase : DatabaseOperationBase
    {
        public ReadFromDatabase() : base()
        {

        }

        public MedicalMark[] ReadMarks()
        {
            string sqlExpression = $"SELECT * FROM MedicalMarks";

            SQLiteCommand command = new SQLiteCommand(sqlExpression, SqlConnection);

            var result = new List<MedicalMark>();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var name = (string)reader["Name"];
                        var id = (Int64)reader["Id"];
                        var unit = (string)reader["Unit"];
                        var nameSubGrop = (string)reader["NameSubGroup"];
                        var nameGroup = (string)reader["NameGroup"];
                        result.Add(new MedicalMark(id: id, name: name, unit: unit, nameSubGroup: nameSubGrop, nameGroup: nameGroup));
                    }
                }
            }

            return result.ToArray();
        }

        public MedicalReference[] ReadReferences()
        {
            string sqlExpression = $"SELECT * FROM MedicalReferences";

            SQLiteCommand command = new SQLiteCommand(sqlExpression, SqlConnection);

            var result = new List<MedicalReference>();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var name = (string)reader["Name"];
                        var id = (Int64)reader["Id"];
                        var value = (string)reader["Value"];
                        var idParent = (Int64)reader["IdParent"];
                        result.Add(new MedicalReference(id: id, parentId: idParent, name: name, value: value));
                    }
                }
            }

            return result.ToArray();
        }

        public MedicalValue[] ReadValues()
        {
            string sqlExpression = $"SELECT * FROM MedicalValues";

            SQLiteCommand command = new SQLiteCommand(sqlExpression, SqlConnection);

            var result = new List<MedicalValue>();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var date = (string)reader["Date"];
                        var id = (Int64)reader["Id"];
                        var value = (int)reader["Value"];
                        var idParent = (Int64)reader["IdParent"];
                        result.Add(new MedicalValue(id: id, parentId: idParent, date: date, value: value));
                    }
                }
            }

            return result.ToArray();
        }

    }
}
