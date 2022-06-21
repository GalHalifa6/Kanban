using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class Board
    {
        private string name;
        public string Name
        {
            get => name;
            set => name = value;
        }
        private Column backlog;
        public Column Backlog { get => backlog;}

        private Column inProgress;
        public Column InProgress { get => inProgress;}
        private Column done;
        public Column Done { get => done; }

        private int nextTaskID;

        private int id;
        public int Id { get => id; }

        private string owner;
        public string Owner { get => owner; }
        

        private HashSet<string> usernames;

        private BoardDTO dto;


        private log4net.ILog logger = Utility.Logger.GetLogger();

        public Board(string boardName, int id, string creatorName)
        {
            name = boardName;
            backlog = new Column("backlog", id);
            inProgress = new Column("in progress", id);
            done = new Column("done", id);
            this.id = id;
            nextTaskID = 0;
            usernames = new HashSet<string>();
            owner = creatorName;
            dto = new BoardDTO(id, name, creatorName, nextTaskID, usernames);
        }

        public Board(BoardDTO boardDTO)
        {
            this.dto = boardDTO;
            this.name = boardDTO.Name;
            this.owner = boardDTO.Owner;
            this.usernames = boardDTO.Users;
            this.nextTaskID = boardDTO.NextTaskID;
            this.id = boardDTO.Id;
        }

        public List<Task> getInProgressTasks()
        {
            return inProgress.GetTasksList();
        }

        /// <summary>
        /// retrieves a column based on the column ordinal
        /// </summary>
        /// <param name="columnNumber"> 0 = backlog, 1 = in progress, 2 = done</param>
        /// <returns> column object based on the given ordinal </returns>
        public Column GetColumn(int columnNumber)
        {
            if (columnNumber > 2 || columnNumber < 0)
                return null;
            if (columnNumber == 0)
            {
                return backlog;
            }
            else if (columnNumber == 1)
            {
                return inProgress;
            }
            else  // (columnNumber == 2)
            {
                return done;
            }
        }

        internal void LimitColumnTasks(int columnNumber, int newLimit)
        {
            if (newLimit < -1)
            {
                logger.Warn("Failed to limit column tasks due to invalid limit");
                throw new Exception("Invalid limitation of tasks");
            }
            if (newLimit % 1 != 0)
            {
                logger.Warn("Failed to limit column tasks due to invalid limit");
                throw new Exception("Invalid limitation of tasks");
            }
            if (newLimit == -1)
            {
                newLimit = int.MaxValue;
            }

            Column col = GetColumn(columnNumber);
            if (col == null)
            {
                logger.Warn("Failed to limit column tasks due to invalid column ordinal");
                throw new Exception("Invalid column");
            }
            col.SetMax(newLimit);
            logger.Info("Max tasks limited to " + newLimit);
        }

        internal void SetOwner(string email)
        {
            throw new NotImplementedException();
        }

        internal void AddBoard() { 
            dto.AddBoard(owner, id, name);
            backlog.AddColumnToDB();
            inProgress.AddColumnToDB();
            done.AddColumnToDB();
        }

        /// <summary>
        /// delete a board and all of it's contents
        /// </summary>
        /// <param name="email">deleter's email</param>
        /// <returns></returns>
        internal void RemoveBoard(string email)
        {
            if (email != owner)
            {
                logger.Info("Non owner attempted to delete board");
                throw new Exception("Only board owner can delete a board");
            }
            dto.RemoveBoard();
        }

        internal int GetColumnLimit(string boardName, int columnNumber)
        {
            Column col = GetColumn(columnNumber);
            if (col == null)
                throw new Exception("Invalid column");
            if (col.maxTasks == int.MaxValue)
                return 1;
            return col.maxTasks;
        }
        /// <summary>
        /// Add a task to this board
        /// </summary>
        /// <param name="email">The user to add the task to</param>
        /// <param name="title">The title of the task</param>
        /// <param name="description">The description of the task</param>
        /// <param name="dueDate">The due date of the task</param>
        /// <exception cref="Exception"></exception>
        internal void AddTask(string email, string title, string description, DateTime dueDate)
        {
            if (IsInBoard(email))
            {
                backlog.AddTask(id, 0,nextTaskID, title, description, dueDate);
                nextTaskID++;
                logger.Info("Task was successfully added");
            }
            else
            {
                logger.Warn("Cannot add a task since the email provided is not registered in the board");
                throw new Exception("Cannot add a task since the email provided is not registered in the board");
            }
        }

        private bool IsInBoard(string email)
        {
            return usernames.Contains(email) || owner == email;
        }



        /*internal Response RemoveTask(string Title)
        {
            if (!(backlog.removeTask(Title) || inProgress.removeTask(Title) || done.removeTask(Title)))
            {
                logger.Warn("An attempt to remove a non-existing task was made");
                return new Response("The board " + boardName + " doesn't have a task named " + Title, true);
            }
            logger.Info("Task was removed successfully");
            return new Response("Task was removed successfully");
        }*/
        internal void AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            
            Column c = GetColumn(columnOrdinal);
            if (c == null)
            {
                logger.Warn("Cannot advance task since an invalid column ordinal was entered");
                throw new Exception("Task could not advance because of a wrong column ordinal");
            }
            Task t = c.GetTask(taskId);
            if (t == null)
            {
                logger.Warn("Cannot advance task because it doesn't exist");
                throw new Exception("Task could not advance because of a wrong task id");
            }
            if (!t.IsAssigned(email))
            {
                logger.Warn("Cannot advance task because the user advancing it is not assigned to it");
                throw new Exception("Cannot advance task because the user advancing it is not assigned to it");
            }
            if (c == backlog)
            {
                inProgress.AddTask(t);
                dto.AdvanceTask(c.dto, inProgress.dto, t.DTO);
                backlog.RemoveTask(t);
                logger.Info("Task " + t.Title + " advanced");
            }
            else if (c == inProgress)
            {
                done.AddTask(t);
                dto.AdvanceTask(c.dto, done.dto, t.DTO);
                inProgress.RemoveTask(t);
                logger.Info("Task " + t.Title + " advanced");
            }
            else // (c == done)
            {
                logger.Warn("Failed to advance task because the task is already done");
                throw new Exception("Failed to advance task because the task is already done");
            }
        }

        /// <summary>
        /// fill the columns of the boards with the given hash set
        /// </summary>
        /// <param name="columns">hsould have 3 columns - one for each column of the board</param>
        internal void FillColumns(HashSet<Column> columns)
        {
            foreach (Column c in columns)
            {
                if(c == null)
                {
                    throw new Exception("Error in assigning columns to board");
                }
                else if (c.name == "backlog")
                {
                    backlog = c;
                }
                else if (c.name == "in progress")
                {
                    inProgress = c;
                }
                else if (c.name == "done")
                {
                    done = c;
                }
            }
        }

        public void AddUser(string email)
        {
            if (usernames.Contains(email) || owner == email)
            {
                logger.Warn(email + " attempted to join a board that he's already in");
                throw new Exception(email + " is already in " + name);
            }
            dto.AddUser(email);
            logger.Info(email + " added to board " + name);
            usernames.Add(email);
        }

        public void RemoveUser(string email)
        {
            if (usernames.Contains(email))
            {
                dto.RemoveUser(email);
                usernames.Remove(email);
                UnassignTasks(email);
                logger.Info(email + " removed from board " + name);
            }
            else if (owner == email)
            {
                logger.Warn("Attempt to remove board owner failed");
                throw new Exception("Cannot remove owner from board without providing another owner");
            }
            else
            {
                logger.Warn("Attempt to remove user from board that the user was not in");
                throw new Exception(email + " is not in " + name);
            }
        }



        private void UnassignTasks(string email)
        {
            backlog.UnassignTasks(email);
            inProgress.UnassignTasks(email);
        }

        public void ChangeOwner(string currentOwner, string newOwner)
        {
            if (currentOwner != owner)
            {
                logger.Warn("Attempt to change owner of board failed due to incorrect currentOwner name");
                throw new Exception("Failed to change owner because " + currentOwner + " is not the owner of the board");
            }
            if (!usernames.Contains(newOwner))
            {
                logger.Warn("Attempt to change owner of board failed due to newOwner not in the board");
                throw new Exception("Failed to change owner because " + newOwner + " is not the in the board");

            }
            dto.ChangeOwner(newOwner);
            usernames.Add(owner);
            usernames.Remove(newOwner);
            owner = newOwner;
            logger.Info("Owner changed successfully");
        }

        internal Task GetTask(int columnOrdinal, int taskId)
        {
            try
            {
                Column column = GetColumn(columnOrdinal);
                if (column == null)
                    return null;
                Task t = column.GetTask(taskId);
                if (t != null)
                    return t;
                return null;
            }
            catch (FormatException)
            {
                return null;
            }

        }

        public List<Task> GetAllAssignedTasks(string email)
        {
            if (!IsInBoard(email))
            {
                logger.Warn("Attempt to get all asigned tasks to user not in board");
                return null;
            }
            logger.Info(email + " got all his assigned tasks");
            return inProgress.GetAllAssignedTasks(email);   
        }


        public void AssignTask(string assigner, int columnOrdinal, int taskID, string assignee)
        {   
            Task t = GetTask(columnOrdinal, taskID);
            if (t == null)
            {
                logger.Warn(assigner + " attempted to reassign a task that doesn't exist");
                throw new Exception("Task does not exist");
            }
            if (!IsInBoard(assignee) || !IsInBoard(assigner))
            {
                throw new Exception("Assigner or assignee not in board");
            }
            t.AssignTask(assigner, assignee);
        }

        public void UpdateTaskTitle(int columnOrdinal, int taskId, string newTitle)
        {
            Column column = GetColumn(columnOrdinal);
            if (column == null)
            {
                throw new Exception("Invalid column ordinal.");
            }
            if (column.name == "done")
            {
                throw new Exception("Cannot edit tasks that are done.");
            }
            Task task = GetTask(columnOrdinal, taskId);
            column.UpdateTaskTitle(task, newTitle);
        }

        public void UpdateTaskDescription(int columnOrdinal, int taskId, string newDesc)
        {
            Column column = GetColumn(columnOrdinal);
            if (column == null)
            {
                throw new Exception("Invalid column ordinal.");
            }
            if (column.name == "done")
            {
                throw new Exception("Cannot edit tasks that are done.");
            }
            Task task = GetTask(columnOrdinal, taskId);
            column.UpdateTaskDescription(task, newDesc);
        }

        public void UpdateTaskDueDate(int columnOrdinal, int taskId, DateTime newDueDate)
        {
            Column column = GetColumn(columnOrdinal);
            if (column == null)
            {
                throw new Exception("Invalid column ordinal.");
            }
            if (column.name == "done")
            {
                throw new Exception("Cannot edit tasks that are done.");
            }
            Task task = GetTask(columnOrdinal, taskId);
            column.UpdateTaskDueDate(task, newDueDate);
        }
    }
}
