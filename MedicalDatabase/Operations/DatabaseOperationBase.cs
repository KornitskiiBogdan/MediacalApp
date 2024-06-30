using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDatabase.Operations
{
    public abstract class DatabaseOperationBase : IDisposable, IAsyncDisposable
    {
        protected readonly SQLiteConnection SqlConnection;

        protected DatabaseOperationBase()
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName +
                       "\\MedicalDatabase\\MedicalDatabase.db";
            SqlConnection = new SQLiteConnection($"Data Source={path}");
            SqlConnection.Open();
        }


        public void Dispose()
        {
            SqlConnection.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await SqlConnection.DisposeAsync();
        }
    }
}
