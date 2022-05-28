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


        public SQLiteDataReader LoadData()
        {
            string from = "SELECT * FROM UsersInBoards JOIN TasksColumnsBoards ON UsersInBoards.boardID = TasksColumnsBoards.boardID";
            string query = $"SELECT * FROM ({from}) AS T1 JOIN Boards ON Boards.id = T1.boardID";
            return DBConnector.GetInstance().ExecuteQuery(query);
        }
    }
}
