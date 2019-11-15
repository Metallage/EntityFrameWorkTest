using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.IO;



namespace EntityFrameWorkTest.DBContext
{
    /// <summary>
    /// EF6 не умеет создавать базы в SQLite
    /// </summary>
    public static class DBCreator
    {

        public static bool DBExists(string path)
        {
            return File.Exists(path);
        }

        public static void CreateDb(string path) => CreateDataBase(path);


        private static void CreateFile(string path)
        {
            SQLiteConnection.CreateFile(path);
        }

        private static void EnableForeignKeys(SQLiteConnection connection)
        {
            if (connection.State == ConnectionState.Open)
            {
                SQLiteCommand enFk = new SQLiteCommand(connection);
                enFk.CommandText = "PRAGMA foreign_keys=on;";
                enFk.ExecuteNonQuery();
            }
        }

        private static void CreateDataBase(string path)
        {
            CreateFile(path);

            List<string> createStrings = new List<string>();
            string createUserString = "CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, " +
                    "Fio TEXT NOT NULL, Telephone TEXT )";
            createStrings.Add(createUserString);
            string createCompString = "CREATE TABLE IF NOT EXISTS Computers (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, " +
                    "NsName TEXT NOT NULL UNIQUE, Ip TEXT NOT NULL UNIQUE, UserId INTEGER, "+
                    "FOREIGN KEY(UserId) REFERENCES User(Id))";
            createStrings.Add(createCompString);
            string createSoftString = "CREATE TABLE IF NOT EXISTS Software (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, " +
                    "Name TEXT NOT NULL UNIQUE, Version TEXT NOT NULL)";
            createStrings.Add(createSoftString);
            string createInstallString = "CREATE TABLE IF NOT EXISTS Installs (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, " +
                    "CompId INTEGER NOT NULL, SoftId INTEGER NOT NULL, version TEXT NOT NULL, " +
                    "FOREIGN KEY (CompId) REFERENCES Computer(Id), FOREIGN KEY (SoftId) REFERENCES Software(Id))";
            createStrings.Add(createInstallString);

            using (SQLiteConnection sq = new SQLiteConnection($"DataSource={path};Version=3;"))
            {
                sq.Open();
                try
                {
                    EnableForeignKeys(sq);
                    CreateTables(sq, createStrings);
                }
                finally
                {
                    sq.Close();
                }
            }

        }
        private static void CreateTables(SQLiteConnection sQLiteConnection, List<string> createStrings)
        {
            
            if (sQLiteConnection.State == ConnectionState.Open)
            {
                SQLiteCommand transaction = new SQLiteCommand(sQLiteConnection);
                transaction.CommandText = "BEGIN TRANSACTION";
                transaction.ExecuteNonQuery();

                foreach (string createString in createStrings)
                {
                    transaction.CommandText = createString;
                    transaction.ExecuteNonQuery();
                }

                transaction.CommandText = "COMMIT";
                transaction.ExecuteNonQuery();
            }

        }

    }
}
