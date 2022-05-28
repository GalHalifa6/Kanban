using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    // this kind of dto actaully plays the role of both dao and dto.
    // it holds both the access to the db and the info about the object, and each dto is owned by a bo
    internal class BoardDTO
    {
        public int id { get; private set; }
        public string name { get; private set; }
        public int nextTaskID { get; private set; }
        public string owner { get; private set; }

        public BoardDTO(int id, string name, string owner, int nextTaskID)
        {
            this.id = id;
            this.name = name;
            this.owner = owner;
            this.nextTaskID = nextTaskID;
        }

        private Response GeneralNonQuery(string query, string goodMsg, string badMsg)
        {
            if (!DBConnector.instance.ExecuteNonQuery(query))
            {
                return new Response(badMsg, true);
            }
            return new Response(goodMsg);
        }

        public Response AddBoard(string email, int id, string name)
        {
            string query = $"INSERT INTO Boards(id, name, nextTaskID, owner) VALUES({id},'{name}',{0},'{email}')";
            return GeneralNonQuery(query, "Board was added successfully", "A board with this id already exists");
        }

        internal Response ChangeOwner(string newOwner)
        {
            string query = $"UPDATE Boards SET owner = '{newOwner}' WHERE id = {id}";
            owner = newOwner;
            return GeneralNonQuery(query, "Owner was changed successfully", "Something went wrong");
        }

        internal Response RemoveBoard()
        {
            string query = $"DELETE FROM Boards WHERE id = {id}";
            return GeneralNonQuery(query, "Board was removed successfully", "Something went wrong");

        }

        internal Response AddUser(string email)
        {
            return new UsersInBoardsDTO().AddUserToBoard(email, id);
        }

        internal Response RemoveUser(string email)
        {
            return new UsersInBoardsDTO().RemoveUserFromBoard(email, id);
        }
    }
}
