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

        public MedicalReference[] ReadAllReferences()
        {
            return ReadReferences($"SELECT * FROM MedicalReferences");
        }

        public MedicalReference[] ReadReferences(MedicalMark mark)
        {
            return ReadReferences($"SELECT * FROM MedicalReferences WHERE IdParent = {mark.Id}");
        }

        private MedicalReference[] ReadReferences(string sqlExpression)
        {
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

        public MedicalValue[] ReadAllValues()
        {
            return ReadValues($"SELECT * FROM MedicalValues");
        }

        public MedicalValue[] ReadValues(MedicalMark mark)
        {
            return ReadValues($"SELECT * FROM MedicalValues WHERE IdParent = {mark.Id}");
        }

        private MedicalValue[] ReadValues(string sqlExpression)
        {
            SQLiteCommand command = new SQLiteCommand(sqlExpression, SqlConnection);

            var result = new List<MedicalValue>();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var date = (long)reader["Date"];
                        var id = (Int64)reader["Id"];
                        var value = reader["Value"];
                        var idParent = (Int64)reader["IdParent"];
                        result.Add(new MedicalValue(id: id, parentId: idParent, date: date, value: Convert.ToSingle(value)));
                    }
                }
            }

            return result.ToArray();
        }

    }
}
