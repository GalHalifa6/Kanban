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

        internal Response AddTask(Task task)
        {
            if (tasks.Contains(task))
            {
                logger.Warn("Cannot add task because it already exists.");
                return new Response("This task already exists.", true);
            }
            for (int i = 0; i < tasks.Count; i++)
            {
                if (task.Id == tasks[i].Id)
                {
                    logger.Warn("Cannot add task because a task with the same Id already exists.");
                    return new Response("A task with the same Id already exists.", true);
                }
            }
            if (tasks.Count == maxTasks)
            {
                logger.Warn("Cannot add task because there are maximum tasks in this column.");
                return new Response("The maximum capacity of tasks in this column is full.", true);
            }
            tasks.Add(task);
            logger.Info("Added task: " + task.Title);
            return new Response(true);
        }

        internal Response AddTask(int ID, string title, string description, DateTime dueDate)
        {
            if (tasks.Count == maxTasks)
            {
                logger.Warn("Cannot add task because there are maximum tasks in this column.");
                return new Response("The maximum capacity of tasks in this column is full.", true);
            }
            for (int i = 0; i < tasks.Count; i++)
            {
                if (ID == tasks[i].Id)
                {
                    logger.Warn("Cannot add task because a task with the same Id already exists.");
                    return new Response("A task with the same Id already exists.", true);
                }
            }
            Task newTask = new Task(ID, title, description, dueDate);
            tasks.Add(newTask);
            logger.Info("Added task: " + newTask.Title);
            return new Response(true);
        }

        internal Response RemoveTask(Task task)
        {
            if (!tasks.Contains(task))
            {
                logger.Warn("Cannot remove task because it doesn't exist.");
                return new Response("The task doesn't exist.", true);
            }
            logger.Info("Task: " + task.Title + " is removed.");
            tasks.Remove(task);
            return new Response(true);
        }

        internal Response RemoveTask(string taskName)
        {
            Boolean found = false;
            for (int i = 0; i < tasks.Count & !found; i++)
            {
                if (tasks[i].Title == taskName)
                {
                    tasks.RemoveAt(i);
                    found = true;
                }
            }
            if (!found)
            {
                logger.Warn("Cannot remove task because it doesn't exist.");
                return new Response("The task doesn't exist.", true);
            }
            logger.Info("Task: " + taskName + " is removed.");
            return new Response(true);
        }
        internal Response UpdateTaskTitle(Task task, string newTitle)
        {
            if (!tasks.Contains(task))
            {
                logger.Warn("Cannot edit task because it doesn't exist.");
                return new Response("The task doesn't exist in this column", true);
            }
            task.UpdateTaskTitle(newTitle);
            logger.Info("Task Title changed to:" + newTitle);
            return new Response(true);
        }

        internal Response UpdateTaskDescription(Task task, string newDescription)
        {
            if (!tasks.Contains(task))
            {
                logger.Warn("Cannot edit task because it doesn't exist.");
                return new Response("That task doesn't exist in this column.", true);
            }
            task.UpdateTaskDescription(newDescription);
            logger.Info("Task Description changed to:" + newDescription);
            return new Response(true);
        }

        internal Response UpdateTaskDueDate(Task task, DateTime newDueDate)
        {
            if (!tasks.Contains(task))
            {
                logger.Warn("Cannot edit task because it doesn't exist.");
                return new Response("That task doesn't exist in this column.", true);
            }
            task.UpdateTaskDueDate(newDueDate);
            logger.Info("Task due date changed to:" + newDueDate);
            return new Response(true);
        }

        public void SetMax(int maxTasks)
        {
            this.maxTasks = maxTasks;
        }

        public Task GetTask(int taskID)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Id == taskID)
                {
                    return tasks[i];
                }
            }
            return null;
        }

        public List<Task> GetTasksList()
        {
            /*string output = "";
            if (tasks.Count > 0)
            {
                for (int i = 0; i < tasks.Count; i++) {
                    if (i != tasks.Count - 1)
                    {
                        output = output + tasks[i].toString() + ",\n";
                    }
                    else
                    {
                        output = output + tasks[i].toString();
                    }
                }
            }
            return output;*/
            return tasks;
        }
    }
}