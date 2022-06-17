using IntroSE.Kanban.Backend.DataAccessLayer;
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
        public List<Task> tasks { get; private set; }

        public ColumnDTO dto { get; private set; }

        private log4net.ILog logger = Utility.Logger.GetLogger();

        public Column(string name)
        {
            this.name = name;
            tasks = new List<Task>();
            maxTasks = int.MaxValue; //If there's no limit on number of tasks, the value is the maximum value of int
            dto = new ColumnDTO(this);
        }

        public Column(ColumnDTO column)
        {
            name = column.name;
            maxTasks = column.maxTasks;
            dto = column;
            foreach (TaskDTO task in column.tasks)
            {
                Task t = new Task(task.Id, task.Title, task.Description, task.DueDate);
                tasks.Add(t);
            }
        }

        internal void AddTask(Task task)
        {
            if (tasks.Contains(task))
            {
                logger.Warn("Cannot add task because it already exists.");
                throw new Exception("This task already exists.");
            }
            for (int i = 0; i < tasks.Count; i++)
            {
                if (task.Id == tasks[i].Id)
                {
                    logger.Warn("Cannot add task because a task with the same Id already exists.");
                    throw new Exception("A task with the same Id already exists.");
                }
            }
            if (tasks.Count == maxTasks)
            {
                logger.Warn("Cannot add task because there are maximum tasks in this column.");
                throw new Exception("The maximum capacity of tasks in this column is full.");
            }
            tasks.Add(task);
            logger.Info("Added task: " + task.Title);
            dto.AddTask(task.dto);
        }

        internal void AddTask(int ID, string title, string description, DateTime dueDate)
        {
            if (tasks.Count == maxTasks)
            {
                logger.Warn("Cannot add task because there are maximum tasks in this column.");
                throw new Exception("The maximum capacity of tasks in this column is full.");
            }
            for (int i = 0; i < tasks.Count; i++)
            {
                if (ID == tasks[i].Id)
                {
                    logger.Warn("Cannot add task because a task with the same Id already exists.");
                    throw new Exception("A task with the same Id already exists.");
                }
            }
            Task newTask = new Task(ID, title, description, dueDate);
            tasks.Add(newTask);
            logger.Info("Added task: " + newTask.Title);
            dto.AddTask(newTask.dto);
        }

        internal void RemoveTask(Task task)
        {
            if (!tasks.Contains(task))
            {
                logger.Warn("Cannot remove task because it doesn't exist.");
                throw new Exception("The task doesn't exist.");
            }
            logger.Info("Task: " + task.Title + " is removed.");
            tasks.Remove(task);
            dto.RemoveTask(task.Id);
        }

        internal void RemoveTask(int id)
        {
            Boolean found = false;
            for (int i = 0; i < tasks.Count & !found; i++)
            {
                if (tasks[i].Id == id)
                {
                    tasks.RemoveAt(i);
                    found = true;
                }
            }
            if (!found)
            {
                logger.Warn("Cannot remove task because it doesn't exist.");
                throw new Exception("The task doesn't exist.");
            }
            logger.Info("Task: " + id + " is removed.");
            dto.RemoveTask(id);
        }
        internal void UpdateTaskTitle(Task task, string newTitle)
        {
            if (!tasks.Contains(task))
            {
                logger.Warn("Cannot edit task because it doesn't exist.");
                throw new Exception("The task doesn't exist in this column");
            }
            task.UpdateTaskTitle(newTitle);
            logger.Info("Task Title changed to: " + newTitle);
        }

        internal void UpdateTaskDescription(Task task, string newDescription)
        {
            if (!tasks.Contains(task))
            {
                logger.Warn("Cannot edit task because it doesn't exist.");
                throw new Exception("That task doesn't exist in this column.");
            }
            task.UpdateTaskDescription(newDescription);
            logger.Info("Task Description changed to: " + newDescription);
        }

        internal void UpdateTaskDueDate(Task task, DateTime newDueDate)
        {
            if (!tasks.Contains(task))
            {
                logger.Warn("Cannot edit task because it doesn't exist.");
                throw new Exception("That task doesn't exist in this column.");
            }
            task.UpdateTaskDueDate(newDueDate);
            logger.Info("Task due date changed to: " + newDueDate);
        }

        public void SetMax(int maxTasks)
        {
            logger.Info("Column's tasks limit was changed to: " + maxTasks);
            this.maxTasks = maxTasks;
            dto.SetMax(maxTasks);
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

        internal List<Task> GetAllAssignedTasks(string email)
        {
            List<Task> output = new List<Task>();
            foreach(Task task in tasks)
            {
                if (task.AssigneeEmail == email)
                {
                    output.Add(task);
                }
            }
            return output;
        }

        internal void UnassignTasks(string email)
        {
            foreach (Task task in tasks)
            {
                if (task.AssigneeEmail.Equals(email))
                {
                    task.UnassignTask();
                }
            }
            logger.Info("Unassigned " + email + " from all tasks");
        }
    }
}