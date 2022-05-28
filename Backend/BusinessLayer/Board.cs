using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class Board
    {
        public string name { get; private set; }
        public Column backlog { get; private set; }
        public Column inProgress { get; private set; }
        public Column done { get; private set; }

        private int nextTaskID;

        public readonly int id;

        public string owner { get; private set;}

        private HashSet<string> usernames;

        private BoardDTO dto;


        private log4net.ILog logger = Utility.Logger.GetLogger();

        public Board(string boardName, int id, string creatorName)
        {
            name = boardName;
            backlog = new Column("backlog");
            inProgress = new Column("in progress");
            done = new Column("done");
            this.id = id;
            nextTaskID = 0;
            usernames = new HashSet<string>();
            owner = creatorName;
            dto = new BoardDTO(id, name, creatorName, nextTaskID);
        }

        public List<Task> getInProgressTasks()
        {
            return inProgress.GetTasksList();
        }

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

        internal Response LimitColumnTasks(int columnNumber, int newLimit)
        {
            if (newLimit < -1)
            {
                logger.Warn("Failed to limit column tasks due to invalid limit");
                return new Response("Invalid limitation of tasks", true);
            }
            if (newLimit % 1 != 0)
            {
                logger.Warn("Failed to limit column tasks due to invalid limit");
                return new Response("Invalid limitation of tasks", true);
            }
            if (newLimit == -1)
            {
                newLimit = int.MaxValue;
            }

            Column col = GetColumn(columnNumber);
            if (col == null)
            {
                logger.Warn("Failed to limit column tasks due to invalid column ordinal");
                return new Response("Invalid column", true);
            }
/*            Response r = col.SetMax(newLimit);
            if (r.ErrorOccured())
                return r;*/
            logger.Info("Max tasks limited to " + newLimit);
            return new Response(true);
        }

        internal Response AddBoard()
        {
            return dto.AddBoard(owner, id, name);
        }

        internal Response RemoveBoard()
        {
            return dto.RemoveBoard();
        }

        internal Response GetColumnLimit(string boardName, int columnNumber)
        {
            Column col = GetColumn(columnNumber);
            if (col == null)
                return new Response("Invalid column", true);
            if (col.maxTasks == int.MaxValue)
                return new Response(-1);
            return new Response(col.maxTasks);
        }

        internal Response AddTask(string email, string title, string description, DateTime dueDate)
        {
            if (IsInBoard(email))
            {
                Response r = backlog.AddTask(nextTaskID, title, description, dueDate);
                if (r.ErrorOccured())
                {
                    return r;
                }
                nextTaskID++;
                logger.Info("Task was successfully added");
                return new Response("The task was added successfully");
            }
            else
            {
                logger.Warn("Cannot add a task since the email provided is not registered in the board");
                return new Response("Cannot add a task since the email provided is not registered in the board", true);
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

        internal Response AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            
            Column c = GetColumn(columnOrdinal);
            if (c == null)
            {
                logger.Warn("Cannot advance task since an invalid column ordinal was entered");
                return new Response("Task could not advance because of a wrong column ordinal", true);
            }
            Task t = c.GetTask(taskId);
            if (t == null)
            {
                logger.Warn("Cannot advance task because it doesn't exist");
                return new Response("Task could not advance because of a wrong task id", true);
            }
            if (!t.IsAssigned(email))
            {
                logger.Warn("Cannot advance task because the user advancing it is not assigned to it");
                return new Response("Cannot advance task because the user advancing it is not assigned to it", true);
            }
            if (c == backlog)
            {
                backlog.RemoveTask(t);
                Response r = inProgress.AddTask(t);
                if (r.ErrorOccured())
                    return r;
                logger.Info("Task " + t.Title + " advanced");
                return new Response("Task " + t.Title + " advanced and is now in progress");
            }
            if (c == inProgress)
            {
                inProgress.RemoveTask(t);
                Response r = done.AddTask(t);
                if (r.ErrorOccured())
                    return r;
                logger.Info("Task " + t.Title + " advanced");
                return new Response("Task " + t.Title + " advanced and is now done");
            }
            else // (c == done)
            {
                logger.Warn("Failed to advance task because the task is already done");
                return new Response("Failed to advance task because the task is already done", true);
            }
        }

        public Response AddUser(string email)
        {
            if (usernames.Contains(email) || owner == email)
            {
                logger.Warn("Attempt to add user to a board that the user is already in");
                return new Response(email + " is already in " + name, true);
            }
            Response r = dto.AddUser(email);
            if (r.ErrorOccured())
                return r;
            logger.Info(email + " added to board " + name);
            usernames.Add(email);
            return new Response(email + " added successfully to " + name);

        }

        public Response RemoveUser(string email)
        {
            if (usernames.Contains(email))
            {
                Response r = dto.RemoveUser(email);
                if (r.ErrorOccured())
                    return r;
                usernames.Remove(email);
                UnassignTasks(email);
                logger.Info(email + " removed from board " + name);
                return r;
            }
            else if (owner == email)
            {
                logger.Warn("Attempt to remove board owner failed");
                return new Response("Cannot remove owner from board without providing another owner", true);
            }
            logger.Warn("Attempt to remove user from board that the user was not in");
            return new Response(email + " is not in " + name, true);
        }

        private void UnassignTasks(string email)
        {
            backlog.UnassignTasks(email);
            inProgress.UnassignTasks(email);
        }

        public Response ChangeOwner(string currentOwner, string newOwner)
        {
            if (currentOwner != owner)
            {
                logger.Warn("Attempt to change owner of board failed due to incorrect currentOwner name");
                return new Response("Failed to change owner because " + currentOwner + " is not the owner of the board", true);
            }
            if (!usernames.Contains(newOwner))
            {
                logger.Warn("Attempt to change owner of board failed due to newOwner not in the board");
                return new Response("Failed to change owner because " + newOwner + " is not the in the board", true);

            }
            Response r = dto.ChangeOwner(newOwner);
            if (r.ErrorOccured())
                return r;
            usernames.Add(owner);
            usernames.Remove(newOwner);
            owner = newOwner;
            logger.Info("Owner changed successfully");
            return r;
        }

        internal Task GetTask(int taskId)
        {
            try
            {
                Task t = backlog.GetTask(taskId);
                if (t != null)
                    return t;
                t = inProgress.GetTask(taskId);
                if (t != null)
                    return t;
                t = done.GetTask(taskId);
                if (t != null)
                    return t;
                return null;
            }
            catch (FormatException)
            {
                return null;
            }
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

        public Response Reassign(string assigner, string assignee, int taskID)
        {   
            Task t = GetTask(taskID);
            if (t == null)
            {
                logger.Warn(assigner + " attempted to reassign a task that doesn't exist");
                return new Response("Task does not exist");
            }
            return t.Reassign(assigner, assignee);
        }
    }
}
