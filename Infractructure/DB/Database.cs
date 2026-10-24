using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infractructure.DB
{
    public class Database
    {
        private readonly string _database;

        public Database(string database)
        {
            _database = database;
        }

        public SqlConnection Connection()
        {
            return new SqlConnection(_database);
        }
    }
}
