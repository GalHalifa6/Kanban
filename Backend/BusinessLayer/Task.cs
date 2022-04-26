using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    internal class Task
    {
        private string title { get; set; }
        private string description { get; set; }
        private DateTime creationTime { get; set; }
        private DateTime dueDate { get; set; }
        private Boolean isDone { get; set; }

        public Task(string name, string description)
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
    }
}
