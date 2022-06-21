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
/*        public ColumnDTO backlog { get; private set; }
        public ColumnDTO inProgress { get; private set; }
        public ColumnDTO done { get; private set; }*/
        public HashSet<string> users { get; private set; }

        public BoardDTO(int id, string name, string owner, int nextTaskID, HashSet<string> users)
        {
            this.id = id;
            this.name = name;
            this.owner = owner;
            this.nextTaskID = nextTaskID;
            //HashSet<TaskDTO> tasks = new HashSet<TaskDTO>();
            this.users = users;
/*            backlog = new ColumnDTO("backlog", tasks);
            inProgress = new ColumnDTO("inProgress", tasks);
            done = new ColumnDTO("done", tasks);

            users = new HashSet<string>();*/
        }


        public BoardDTO(int id, string name, string owner, int nextTaskID, ColumnDTO backlog, ColumnDTO inProgress, ColumnDTO done, HashSet<string> users)
        {
            this.id = id;
            this.name = name;
            this.owner = owner;
            this.nextTaskID = nextTaskID;

/*            this.backlog = backlog;
            this.inProgress = inProgress;
            this.done = done;

            this.users = users;*/
        }

        private void GeneralNonQuery(string query, string badMsg)
        {
            if (!DBConnector.GetInstance().ExecuteNonQuery(query))
            {
                throw new Exception(badMsg);
            }
        }

        public void AddBoard(string email, int id, string name)
        {
            string query = $"INSERT INTO Boards(id, name, nextTaskID, owner) VALUES({id},'{name}',{0},'{email}')";
            GeneralNonQuery(query, "A board with this id already exists");
        }

        internal void ChangeOwner(string newOwner)
        {
            string boardsUpdate = $"UPDATE Boards SET owner = '{newOwner}' WHERE id = {id}";
            GeneralNonQuery(boardsUpdate, "Something went wrong");
            UsersBoardsDTO ub = new UsersBoardsDTO();
            ub.AddUserToBoard(owner, id);
            ub.RemoveUserFromBoard(newOwner, id);
            owner = newOwner;
        }

        internal void RemoveBoard()
        {
            string query = $"DELETE FROM Boards WHERE id = {id}";
            GeneralNonQuery(query, "Something went wrong");

        }

        internal void AddUser(string email)
        {
            new UsersBoardsDTO().AddUserToBoard(email, id);
        }

        internal void RemoveUser(string email)
        {
            new UsersBoardsDTO().RemoveUserFromBoard(email, id);
        }

        internal void AdvanceTask(ColumnDTO currentColDTO, ColumnDTO nextColDTO, TaskDTO taskDTO)
        {
            nextColDTO.AddTask(taskDTO);
            currentColDTO.RemoveTask(taskDTO.Id);
            new TasksColumnsBoardsDTO().AdvanceTask(id, currentColDTO.Ordinal, nextColDTO.Ordinal, taskDTO.Id);
        }
    }
}