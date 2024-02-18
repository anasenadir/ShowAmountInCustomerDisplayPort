using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace TotalAmount.data
{
    class PostgressDatabase
    {
        private static readonly object padlock = new();
        private static PostgressDatabase? instance = null;
        private NpgsqlConnection? connection;
        public static PostgressDatabase getInstance()
        {
            lock (padlock)
            {
                instance ??= new PostgressDatabase();
                return instance;
            }
        }

        public void Initialize(String host, String username, String password, String database)
        {
            connection ??= new NpgsqlConnection(
                $"Host={host};Username={username};Password={password};Database={database}"
            );

            connection.Open();
        }


        public bool ConnectionIsAlive()
        {
            if (connection == null)
            {
                return false;
            }
            return false;
        }


        public NpgsqlConnection GetConnection()
        {
            if (connection == null)
            {
                throw new InvalidOperationException(
                    "Connection has not been initialized. Call Initialize method first."
                );
            }

            return connection;
        }
    }
}
