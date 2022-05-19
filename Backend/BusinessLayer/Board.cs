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


        private log4net.ILog logger = Utility.Logger.GetLogger();

        public Board(string name)
        {
            this.name = name;
            backlog = new Column("backlog");
            inProgress = new Column("in progress");
            done = new Column("done");
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
            col.SetMax(newLimit);
            logger.Info("Max tasks limited to " + newLimit);
            return new Response(true);
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

        internal Response AddTask(int taskID ,string title, string description, DateTime dueDate)
        {
            Response r = backlog.AddTask(taskID, title, description, dueDate);
            if (r.ErrorOccured())
            {
                return r;
            }
            logger.Info("Task was successfully added");
            return new Response("The task was added successfully");
        }

        

        /*internal Response<string> RemoveTask(string Title)
        {
            if (!(backlog.removeTask(Title) || inProgress.removeTask(Title) || done.removeTask(Title)))
            {
                logger.Warn("An attempt to remove a non-existing task was made");
                return new Response<string>("The board " + name + " doesn't have a task named " + Title, true);
            }
            logger.Info("Task was removed successfully");
            return new Response<string>("Task was removed successfully");
        }*/

        internal Response AdvanceTask(int columnOrdinal, int taskId)
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
    }
}
