using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class BoardMapper
    {
        public BoardMapper() { }

        public HashSet<BoardDTO> LoadData()
        {
            SQLiteDataReader res = DBConnector.GetInstance().ExecuteQuery("SELECT * FROM Boards");
            HashSet<BoardDTO> data = new HashSet<BoardDTO>();
            while(res.Read())
            {
                int id = res.GetInt32(res.GetOrdinal("id"));
                string name = res.GetString(res.GetOrdinal("name"));
                int nextTaskID = res.GetInt32(res.GetOrdinal("nextTaskID"));
                string owner = res.GetString(res.GetOrdinal("owner"));
                data.Add(new BoardDTO(id, name, owner, nextTaskID));
            }
            return data;
        }
    }
}
