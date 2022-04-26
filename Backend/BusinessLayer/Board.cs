using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    internal class Board
    {
        private string name { get; set; }
        private Column backlog { get; set; }
        private Column inProgress { get; set; }
        private Column done { get; set; }

        public Board(string name)
        {
            throw new NotImplementedException();
        }

        public void addTask(string boardName, string title, string description)
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
        }
    }
}
