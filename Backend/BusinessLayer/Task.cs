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

        public Task(string name, string description, DateTime dueDate)
        {
            this.title = name;
            this.description = description;
            creationTime = DateTime.Now;
            this.dueDate = dueDate;
            isDone = false;
        }

        public void editTaskTitle(string newTitle)
        {
            title = newTitle;
        }
        public void editTaskDescription(string newDescription)
        {
            description = newDescription;
        }

        public void editTaskDueDate(DateTime newDueDate)
        {
            dueDate = newDueDate;
        }
    }
}
