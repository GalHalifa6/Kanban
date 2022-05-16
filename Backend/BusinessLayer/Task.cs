using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    internal class Task
    {
        public string title { get; private set; }
        public string description { get; private set; }
        public DateTime creationTime { get; private set; }
        public DateTime dueDate { get; private set; }
        public Boolean isDone { get; private set; }
        public int ID { get; private set; }

        public Task(int ID, string name, string description, DateTime dueDate)
        {
            this.ID = ID;
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

        public string toString()
        {
            string output = "";
            output = output + string.Format("{0}: {1}", "Id", ID) + "\n";
            output = output + string.Format("{0}: {1}", "CreationTime", creationTime) + "\n";
            output = output + string.Format("{0}: {1}", "Title", title) + "\n";
            output = output + string.Format("{0}: {1}", "Description", description) + "\n";
            output = output + string.Format("{0}: {1}", "DueDate", dueDate);
            return output;
        }
    }
}
