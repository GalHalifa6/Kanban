using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class UsersBoardsDTO
    {
        public UsersBoardsDTO()
        {
        }


        private Response GeneralNonQuery(string query, string goodMsg, string badMsg)
        {
            if (!DBConnector.GetInstance().ExecuteNonQuery(query))
            {
                return new Response(badMsg, true);
            }
            return new Response(goodMsg);
        }
        internal Response AddUserToBoard(string email, int id)
        {
            string query = $"INSERT INTO UsersBoards(boardID, userEmail) VALUES({id},'{email}')";
            return GeneralNonQuery(query, "User added successfully", "Something went wrong");
        }

        internal Response RemoveUserFromBoard(string email, int id)
        {
            string query = $"DELETE FROM UsersBoards WHERE boardID = {id} AND userEmail = '{email}'";
            return GeneralNonQuery(query, "User removed successfully", "Something went wrong");
        }

        public Dictionary<int, HashSet<string>> LoadData()
        {
            string query = "SELECT * FROM UsersBoards";
            SQLiteDataReader res = DBConnector.GetInstance().ExecuteQuery(query);
            Dictionary<int,HashSet<string>> dict = new Dictionary<int, HashSet<string>>();
            while (res.Read())
            {
                int boardID = res.GetInt32(res.GetOrdinal("boardID"));
                string email = res.GetString(res.GetOrdinal("userEmail"));
                if (!dict.ContainsKey(boardID))
                {
                    dict.Add(boardID, new HashSet<string>());
                }
                dict[boardID].Add(email);
            }
            return dict;
        }
    }
}