using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class DBConnector
    {
        private SQLiteConnection conn;
        public static DBConnector instance { get; private set; }
        //relative path 
        string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "kanban.db"));

        private DBConnector()
        {
            conn = new SQLiteConnection($"Data Source= {path}; Version = 3; New = True; Compress = True; ");
            try
            {
                conn.Open();
                CreateTables();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
            finally
            {
                conn.Close();
            }
        }

        public static DBConnector GetInstance()
        {
            if (instance == null)
                instance = new DBConnector();
            return instance;
        }

        private void CreateTables()
        {
            SQLiteCommand cmd = conn.CreateCommand();
            string query = "CREATE TABLE IF NOT EXISTS UserBoards(" +
                "userEmail VARCHAR(200)," +
                "boardID INTEGER," +
                "PRIMARY KEY (userEmail,boardID)," +
                //"FOREIGN KEY (userEmail) REFERENCES Users(id)," +
                "FOREIGN KEY (boardID) REFERENCES Boards(id)" +
                ")";

            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            query = "CREATE TABLE IF NOT EXISTS Boards(" +
                "id INTEGER," +
                "name VARCHAR(200)," +
                "nextTaskID INTEGER," +
                "owner VARCHAR(200)," +
                "PRIMARY KEY (id)," +
                "FOREIGN KEY (owner) REFERENCES Users(id) ON DELETE CASCADE" +
                ")";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            query = "CREATE TABLE IF NOT EXISTS TasksColumnsBoards(" +
                "boardID INTEGER," +
                "columnOrdinal INTEGER," +
                "taskID INTEGER," +
                "PRIMARY KEY (boardID, taskID)," +
                "FOREIGN KEY (boardID) REFERENCES Boards(id) ON DELETE CASCADE," +
                // Don't forget commas when removing these comment lines
                "FOREIGN KEY (columnOrdinal) REFERENCES Columns(columnOrdinal) ON DELETE CASCADE" +
                //"FOREIGN KEY taskID REFERENCES Tasks(id) ON DELETE CASCADE" +
                ")";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            query = "CREATE TABLE IF NOT EXISTS Columns(" +
                "boardID INTEGER," +
                "columnOrdinal INTEGER," +
                "maxTasks INTEGER," +
                "PRIMARY KEY (boardID, columnName)," +
                "FOREIGN KEY (boardID) REFERENCES Boards(id)" +
                ")";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
        }


        public SQLiteDataReader ExecuteQuery(string nq)
        {
            
            try
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = nq;
                SQLiteDataReader res = cmd.ExecuteReader();
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool ExecuteNonQuery(string nq){

            try
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = nq;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }


    }
}
