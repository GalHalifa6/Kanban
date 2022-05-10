using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    internal class Board
    {
        public string name { get; private set; }
        public Column backlog { get; private set; }
        public Column inProgress { get; private set; }
        public Column done { get; private set; }

        private log4net.ILog logger = Utility.Logger.GetLogger();

        public Board(string name)
        {
            this.name = name;
            backlog = new Column("Backlog");
            inProgress = new Column("In Progress");
            done = new Column("Done");
        }

        /*public void addTask(string boardName, string title, string description)
        {
            throw new NotImplementedException();
        }

        public Boolean removeTask(string boardName, string title)
        {
            throw new NotImplementedException();
        }

        public void editTaskTitle(string boardName, string oldTitle, string newTitle)
        {
            throw new NotImplementedException();
        }
        public void editTaskDescription(string boardName, string title, string newDescription)
        {
            throw new NotImplementedException();
        }

        public void editTaskDueDate(string boardName, string title, DateTime newDueDate)
        {
            throw new NotImplementedException();
        }

        public void advanceTaskPhase(string title)
        {
            throw new NotImplementedException();
        }

        public List<Task> getInProgressTasks()
        {
            throw new NotImplementedException();
        }*/

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

        internal Response<bool> LimitColumnTasks(int columnNumber, int newLimit)
        {
            if (newLimit < -1)
            {
                logger.Warn("Failed to limit column tasks due to invalid limit");
                return new Response<bool>("Invalid limitation of tasks");
            }
            if (newLimit == -1)
            {
                newLimit = int.MaxValue;
            }
            Column col = GetColumn(columnNumber);
            if (col == null)
            {
                logger.Warn("Failed to limit column tasks due to invalid column ordinal");
                return new Response<bool>("Invalid column");
            }
            col.maxTasks = newLimit;
            logger.Info("Max tasks limited to " + newLimit);
            return new Response<bool>(true);
        }

        internal Response<int> GetColumnLimit(string boardName, int columnNumber)
        {
            Column col = GetColumn(columnNumber);
            if (col == null)
                return new Response<int>("Invalid column");
            return new Response<int>(col.maxTasks);
        }
    }
}
