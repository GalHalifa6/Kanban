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
    public class BoardDTO
    {
        public int id { get; private set; }
        public string name { get; private set; }
        public int nextTaskID { get; private set; }
        public string owner { get; private set; }
        public ColumnDTO backlog { get; private set; }
        public ColumnDTO inProgress { get; private set; }
        public ColumnDTO done { get; private set; }
        public HashSet<string> users { get; private set; }
        public BoardDTO(int id, string name, string owner, int nextTaskID)
        {
            this.id = id;
            this.name = name;
            this.owner = owner;
            this.nextTaskID = nextTaskID;

            backlog = new ColumnDTO();
            inProgress = new ColumnDTO();
            done = new ColumnDTO();

            users = new HashSet<string>();
        }


        public BoardDTO(int id, string name, string owner, int nextTaskID, ColumnDTO backlog, ColumnDTO inProgress, ColumnDTO done, HashSet<string> users)
        {
            this.id = id;
            this.name = name;
            this.owner = owner;
            this.nextTaskID = nextTaskID;

            this.backlog = backlog;
            this.inProgress = inProgress;
            this.done = done;

            this.users = users;
        }

        private Response GeneralNonQuery(string query, string goodMsg, string badMsg)
        {
            if (!DBConnector.GetInstance().ExecuteNonQuery(query))
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
            string boardsUpdate = $"UPDATE Boards SET owner = '{newOwner}' WHERE id = {id}";
            Response r1 = GeneralNonQuery(boardsUpdate, "Owner was changed successfully", "Something went wrong");
            UsersBoardsDTO ub = new UsersBoardsDTO();
            Response r2 = ub.AddUserToBoard(owner, id);
            Response r3 = ub.RemoveUserFromBoard(newOwner, id);
            if (!r1.ErrorOccured() && !r2.ErrorOccured() && !r3.ErrorOccured())
            {
                owner = newOwner;
            }
            return r1;
        }

        internal Response RemoveBoard()
        {
            string query = $"DELETE FROM Boards WHERE id = {id}";
            return GeneralNonQuery(query, "Board was removed successfully", "Something went wrong");

        }

        internal Response AddUser(string email)
        {
            return new UsersBoardsDTO().AddUserToBoard(email, id);
        }

        internal Response RemoveUser(string email)
        {
            return new UsersBoardsDTO().RemoveUserFromBoard(email, id);
        }

        internal Response AdvanceTask(ColumnDTO currentColDTO, ColumnDTO nextColDTO, TaskDTO taskDTO)
        {
            nextColDTO.AddTask(taskDTO);
            currentColDTO.RemoveTask(taskDTO);
            return new TasksColumnsBoardsDTO().AdvanceTask(id, currentColDTO.ordinal, nextColDTO.ordinal, taskDTO.id);
        }
    }
}