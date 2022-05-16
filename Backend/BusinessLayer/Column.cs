using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    internal class Column
    {
        public string name { get; private set; }
        public int maxTasks { get; private set; }
        private List<Task> tasks { get; set; }

        public Column(string name)
        {
            this.name = name;
            tasks = new List<Task>();
            maxTasks = -1; //The value is -1 if there isn't a limit on the max tasks possible. Any number higher than 0 is the limit number.
        }

        internal Response<bool> addTask(Task task)
        {
            if (tasks.Contains(task))
            {
                return new Response<bool>("This task already exists.");
            }
            tasks.Add(task);
            return new Response<bool>(true);
        }

        internal Response<bool> addTask(int ID, string title, string description, DateTime dueDate)
        {
            if (maxTasks != -1 & tasks.Count == maxTasks)
            {
                return new Response<bool>("The maximum capacity of tasks in this column is full.");
            }
            Task newTask = new Task(ID, title, description, dueDate);
            tasks.Add(newTask);
            return new Response<bool>(true);
        }

        internal Response<bool> removeTask(Task task)
        {
            if (!tasks.Contains(task))
            {
                return new Response<bool>("The task doesn't exist.");
            }
            tasks.Remove(task);
            return new Response<bool>(true);
        }
        internal Response<bool> editTaskTitle(Task task, string newTitle)
        {
            if (!tasks.Contains(task))
            {
                return new Response<bool>("The task doesn't exist in this column");
            }
            task.editTaskTitle(newTitle);
            return new Response<bool>(true);
        }

        internal Response<bool> editTaskDescription(Task task, string newDescription)
        {
            if (!tasks.Contains(task))
            {
                return new Response<bool>("That task doesn't exist in this column.");
            }
            task.editTaskDescription(newDescription);
            return new Response<bool>(true);
        }

        internal Response<bool> editTaskDueDate(Task task, DateTime newDueDate)
        {
            if(!tasks.Contains(task))
            {
                return new Response<bool>("That task doesn't exist in this column.");
            }
            task.editTaskDueDate(newDueDate);
            return new Response<bool>(true);
        }

        internal Response<bool> setMax(int maxTasks)
        {
            if (maxTasks < 1)
            {
                return new Response<bool>("Number of max tasks needs to be more than 1.");
            }
            this.maxTasks = maxTasks;
            return new Response<bool>(true);
        }

        public Task getTask(int taskID)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].ID == taskID)
                {
                    return tasks[i];
                }
            }
            return null;
        }

        public string getTasksList()
        {
            string output = "";
            if (tasks.Count > 0)
            {
                for (int i = 0; i < tasks.Count; i++) {
                    if (i != tasks.Count - 1)
                    {
                        output = output + tasks[i].toString() + "\n";
                    }
                    else
                    {
                        output = output + tasks[i].toString();
                    }
                }
            }
            return output;
        }
    }
}
