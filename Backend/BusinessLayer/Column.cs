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
        public string name;
        public string Name
        {
            get => name;
        }
        public int maxTasks;
        public int MaxTasks
        {
            get => maxTasks;
            set => maxTasks = value;
        }
        public List<Task> tasks;
        public List<Task> Tasks
        {
            get => tasks;
            set => tasks = value;
        }

        private int boardID;
        public int BoardID { get => boardID; }

        public ColumnDTO dto;
        public ColumnDTO DTO
        {
            get => dto;
            set => dto = value;
        }

        private log4net.ILog logger = Utility.Logger.GetLogger();

        public Column(string name, int boardID)
        {
            this.name = name;
            this.tasks = new List<Task>();
            this.maxTasks = int.MaxValue; //If there's no limit on number of tasks, the value is the maximum value of int
            this.boardID = boardID;
            this.dto = new ColumnDTO(this);

        }

        public Column(ColumnDTO column)
        {
            this.name = column.Name;
            this.maxTasks = column.MaxTasks;
            this.dto = column;
            tasks = new List<Task>();
            this.dto = column;
            this.boardID = column.BoardID;
            foreach (TaskDTO task in column.Tasks)
            {
                Task t = new Task(task.BoardID, task.ColumnOrdinal, task.Id, task.Title, task.Description, task.DueDate);
                this.tasks.Add(t);
            }
        }

        public int GetBoardID()
        {
            return dto.BoardID;
        }

        /// <summary>
        /// Add a task to this column
        /// </summary>
        /// <param name="task">The task</param>
        /// <exception cref="Exception"></exception>
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
            dto.AddTask(task.DTO);
        }
        /// <summary>
        /// Add a task to this column
        /// </summary>
        /// <param name="ID">The task id</param>
        /// <param name="title">The task's title</param>
        /// <param name="description">The task's description</param>
        /// <param name="dueDate">The task's due date</param>
        /// <exception cref="Exception"></exception>
        internal void AddTask(int boardID, int columnOrdinal, int ID, string title, string description, DateTime dueDate)
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
            Task newTask = new Task(boardID, columnOrdinal, ID, title, description, dueDate);
            tasks.Add(newTask);
            logger.Info("Added task: " + newTask.Title);
            dto.AddTask(newTask.DTO);
        }

        internal void AddColumnToDB()
        {
            dto.AddColumnToDB();
        }

        /// <summary>
        /// Remove a task from this column
        /// </summary>
        /// <param name="task">The task we need to remove</param>
        /// <exception cref="Exception"></exception>
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

        /*internal void RemoveTask(int id)
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
        }*/
        /// <summary>
        /// Update a task's title
        /// </summary>
        /// <param name="task">The task</param>
        /// <param name="newTitle">The new title</param>
        /// <exception cref="Exception"></exception>
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
        /// <summary>
        /// Update a task's description
        /// </summary>
        /// <param name="task">The task</param>
        /// <param name="newDescription">The new description</param>
        /// <exception cref="Exception"></exception>
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
        /// <summary>
        /// Update a task's due date
        /// </summary>
        /// <param name="task">The task</param>
        /// <param name="newDueDate">The new due date</param>
        /// <exception cref="Exception"></exception>
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
        /// <summary>
        /// Set a limit of the column's tasks
        /// </summary>
        /// <param name="maxTasks">The new limit</param>
        public void SetMax(int maxTasks)
        {
            logger.Info("Column's tasks limit was changed to: " + maxTasks);
            this.maxTasks = maxTasks;
            dto.SetMax(maxTasks);
        }
        /// <summary>
        /// Get a task by the id
        /// </summary>
        /// <param name="taskID">The task id</param>
        /// <returns></returns>
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
        /// <summary>
        /// Get a list of tasks in this column
        /// </summary>
        /// <returns></returns>
        public List<Task> GetTasksList()
        {
            return tasks;
        }
        /// <summary>
        /// Get a list of the assigned tasks of a user
        /// </summary>
        /// <param name="email">The user's email</param>
        /// <returns></returns>
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
        /// <summary>
        /// Unassign all tasks from a user
        /// </summary>
        /// <param name="email">The user's email</param>
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