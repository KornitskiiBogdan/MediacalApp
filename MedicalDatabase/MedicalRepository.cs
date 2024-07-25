using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalDatabase.Operations;

namespace MedicalDatabase
{
    public class MedicalRepository
    {
        public MedicalRepository()
        {
            Reader = new ReadFromDatabase();
            Writer = new WriteToDatabase();
            DeleteFromDatabase = new DeleteFromDatabase();
        }

        public ReadFromDatabase Reader { get; }

        public WriteToDatabase Writer { get; }

        public DeleteFromDatabase DeleteFromDatabase { get; }
    }
}
