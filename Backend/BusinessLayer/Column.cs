using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class Column
    {
        public string name { get; private set; }
        public int maxTasks { get; private set; }
        private List<Task> tasks { get; set; }

        private log4net.ILog logger = Utility.Logger.GetLogger();

        public Column(string name)
        {
            this.name = name;
            tasks = new List<Task>();
            maxTasks = int.MaxValue; //If there's no limit on number of tasks, the value is the maximum value of int
        }

        internal Response<bool> addTask(Task task)
        {
            if (tasks.Contains(task))
            {
                logger.Warn("Cannot add task because it already exists.");
                return new Response<bool>("This task already exists.", true);
            }
            for (int i = 0; i < tasks.Count; i++)
            {
                if (task.ID == tasks[i].ID)
                {
                    logger.Warn("Cannot add task because a task with the same ID already exists.");
                    return new Response<bool>("A task with the same ID already exists.", true);
                }
            }
            if (tasks.Count == maxTasks)
            {
                logger.Warn("Cannot add task because there are maximum tasks in this column.");
                return new Response<bool>("The maximum capacity of tasks in this column is full.", true);
            }
            tasks.Add(task);
            logger.Info("Added task: " + task.toString());
            return new Response<bool>(true);
        }

        internal Response<bool> addTask(int ID, string title, string description, DateTime dueDate)
        {
            if (tasks.Count == maxTasks)
            {
                logger.Warn("Cannot add task because there are maximum tasks in this column.");
                return new Response<bool>("The maximum capacity of tasks in this column is full.", true);
            }
            for (int i = 0; i < tasks.Count; i++)
            {
                if (ID == tasks[i].ID)
                {
                    logger.Warn("Cannot add task because a task with the same ID already exists.");
                    return new Response<bool>("A task with the same ID already exists.", true);
                }
            }
            Task newTask = new Task(ID, title, description, dueDate);
            tasks.Add(newTask);
            logger.Info("Added task: " + newTask.toString());
            return new Response<bool>(true);
        }

        internal Response<bool> removeTask(string taskName)
        {
            Boolean found = false;
            for (int i = 0; i < tasks.Count & !found; i++)
            {
                if (tasks[i].title == taskName)
                {
                    tasks.RemoveAt(i);
                    found = true;
                }
            }
            if (!found)
            {
                logger.Warn("Cannot remove task because it doesn't exist.");
                return new Response<bool>("The task doesn't exist.", true);
            }
            logger.Info("Task: " + taskName + " is removed.");
            return new Response<bool>(true);
        }
        internal Response<bool> editTaskTitle(Task task, string newTitle)
        {
            if (!tasks.Contains(task))
            {
                logger.Warn("Cannot edit task because it doesn't exist.");
                return new Response<bool>("The task doesn't exist in this column", true);
            }
            task.editTaskTitle(newTitle);
            logger.Info("Task title changed to:" + newTitle);
            return new Response<bool>(true);
        }

        internal Response<bool> editTaskDescription(Task task, string newDescription)
        {
            if (!tasks.Contains(task))
            {
                logger.Warn("Cannot edit task because it doesn't exist.");
                return new Response<bool>("That task doesn't exist in this column.", true);
            }
            task.editTaskDescription(newDescription);
            logger.Info("Task description changed to:" + newDescription);
            return new Response<bool>(true);
        }

        internal Response<bool> editTaskDueDate(Task task, DateTime newDueDate)
        {
            if (!tasks.Contains(task))
            {
                logger.Warn("Cannot edit task because it doesn't exist.");
                return new Response<bool>("That task doesn't exist in this column.", true);
            }
            task.editTaskDueDate(newDueDate);
            logger.Info("Task due date changed to:" + newDueDate);
            return new Response<bool>(true);
        }

        public void setMax(int maxTasks)
        {
            this.maxTasks = maxTasks;
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