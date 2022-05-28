using IntroSE.Kanban.Backend.BusinessLayer;
using System;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class UsersInBoardsDTO
    {
        public UsersInBoardsDTO()
        {
        }


        private Response GeneralNonQuery(string query, string goodMsg, string badMsg)
        {
            if (!DBConnector.instance.ExecuteNonQuery(query))
            {
                return new Response(badMsg, true);
            }
            return new Response(goodMsg);
        }
        internal Response AddUserToBoard(string email, int id)
        {
            string query = $"INSERT INTO UsersInBoards(boardID, email) VALUES({id},{email})";
            return GeneralNonQuery(query, "User added successfully", "Something went wrong");
        }

        internal Response RemoveUserFromBoard(string email, int id)
        {
            string query = $"DELETE FROM UsersInBoards WHERE boardID = {id} AND email = {email}";
            return GeneralNonQuery(query, "User removed successfully", "Something went wrong");
        }
    }
}