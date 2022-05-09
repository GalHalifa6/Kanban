using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    internal class Column
    {
        private string name { get; set; }
        private int maxTasks { get; set; }
        private List<Task> tasks { get; set; }

        public Column(string name)
        {
            this.name = name;
            tasks = new List<Task>();
            maxTasks = -1; //The value is -1 if there isn't a limit on the max tasks possible. Any number higher than 0 is the limit number.
        }

        public void addTask(string title, string description, DateTime dueDate)
        {
            if (maxTasks != -1 & tasks.Count == maxTasks)
            {
                throw new Exception("The maximum capacity of tasks in this column is full. It is not possible to add another task");
            }
            Task newTask = new Task(title, description, dueDate);
            tasks.Add(newTask);
        }

        public Boolean removeTask(Task task)
        {
            if (!tasks.Contains(task))
            {
                return false;
            }
            tasks.Remove(task);
            return true;
        }

        public void editTaskTitle(Task task, string newTitle)
        {
            if (!tasks.Contains(task))
            {
                throw new Exception("That task doesn't exist in this column");
            }
            task.editTaskTitle(newTitle);
        }
        public void editTaskDescription(Task task, string newDescription)
        {
            if (!tasks.Contains(task))
            {
                throw new Exception("That task doesn't exist in this column");
            }
            task.editTaskDescription(newDescription);
        }

        public void editTaskDueDate(Task task, DateTime newDueDate)
        {
            if (!tasks.Contains(task))
            {
                throw new Exception("That task doesn't exist in this column");
            }
            task.editTaskDueDate(newDueDate);
        }
    }
}
