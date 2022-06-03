using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class BoardControllerDTO
    {
        public BoardControllerDTO()
        {

        }


        public HashSet<BoardDTO> LoadData()
        {
            string from = "SELECT * FROM UsersInBoards JOIN TasksColumnsBoardsDTO ON UsersInBoards.boardID = TasksColumnsBoardsDTO.boardID";
            string query = $"SELECT * FROM Boards JOIN ({from}) AS T1 ON Boards.id = T1.boardID";
            SQLiteDataReader res = DBConnector.GetInstance().ExecuteQuery(query);
            HashSet<BoardDTO> boards = new HashSet<BoardDTO>();
            while (res.Read())
            {

                // TODO: TaskControllerDTO should load all tasks to itself, then ColumnControllerDTO does the same, and then here
                int id = res.GetInt32(res.GetOrdinal("id"));
                string name = res.GetString(res.GetOrdinal("name"));
                int nextTaskID = res.GetInt32(res.GetOrdinal("nextTaskID"));
                string owner = res.GetString(res.GetOrdinal("owner"));

            }
            return boards;
        }
    }
}