using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class Task
    {
        public int Id { get; private set; }
        public DateTime CreationTime { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
       
        public DateTime DueDate { get; private set; }

        public TaskDTO dto { get; private set; } 

        public String AssigneeEmail { get; private set; }

        private log4net.ILog logger = Utility.Logger.GetLogger();

        //private Boolean isDone;


        public Task(int ID, string title, string description, DateTime dueDate)
        {
            this.Id = ID;
            this.Title = title;
            this.Description = description;
            CreationTime = DateTime.Now;
            this.DueDate = dueDate;
            AssigneeEmail = null;
            //isDone = false;
            dto = new TaskDTO(ID, title, description, dueDate);
        }

        /// <summary>
        /// Update a task's title
        /// </summary>
        /// <param name="newTitle">The new title</param>
        public void UpdateTaskTitle(string newTitle)
        {
            Title = newTitle;
            dto.UpdateTaskTitle(newTitle);
        }
        /// <summary>
        /// Update a task's description
        /// </summary>
        /// <param name="newDescription">The new description</param>
        public void UpdateTaskDescription(string newDescription)
        {
            Description = newDescription;
            dto.UpdateTaskDescription(newDescription);
        }
        /// <summary>
        /// Update a task's due date
        /// </summary>
        /// <param name="newDueDate">The new due date</param>
        public void UpdateTaskDueDate(DateTime newDueDate)
        {
            DueDate = newDueDate;
            dto.UpdateTaskDueDate(newDueDate);
        }
        /// <summary>
        /// Assign a task to a user
        /// </summary>
        /// <param name="assigner">The user assigning</param>
        /// <param name="assignee">The user assigned</param>
        /// <exception cref="Exception">The user can't assign this task</exception>
        internal void AssignTask(string assigner, string assignee)
        {
            if (assignee != null && assignee != assigner)
            {
                logger.Warn("Failed to assign task because the assigner doesn't have permissions");
                throw new Exception("User can't assign to this task");
            }
            AssigneeEmail = assignee;
            logger.Info("Assigned " + assignee + " to the task: " + Id);
            dto.AssignTask(assigner, assignee);
        }
        /// <summary>
        /// Returns if the user is assigned to this task
        /// </summary>
        /// <param name="email">The user's email</param>
        /// <returns></returns>
        internal bool IsAssigned(string email)
        {
            if (email != null && email == AssigneeEmail)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Unassign this task from its' assignee
        /// </summary>
        internal void UnassignTask()
        {
            AssigneeEmail = null;
            dto.UnassignTask();
        }

        /*public string toString()
        {
            *//*string output = "{";
            output = output + string.Format("{0}: {1}", "Id", Id) + ",\n";
            output = output + string.Format("{0}: {1}", "CreationTime", CreationTime) + ",\n";
            output = output + string.Format("{0}: {1}", "Title", Title) + ",\n";
            output = output + string.Format("{0}: {1}", "Description", Description) + ",\n";
            output = output + string.Format("{0}: {1}", "DueDate", DueDate);
            return output + "}";*//*
            return JsonSerializer.Serialize(this);
        }*/
    }
}