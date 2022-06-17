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

        public void UpdateTaskTitle(string newTitle)
        {
            Title = newTitle;
            dto.UpdateTaskTitle(newTitle);
        }
        public void UpdateTaskDescription(string newDescription)
        {
            Description = newDescription;
            dto.UpdateTaskDescription(newDescription);
        }

        public void UpdateTaskDueDate(DateTime newDueDate)
        {
            DueDate = newDueDate;
            dto.UpdateTaskDueDate(newDueDate);
        }

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

        internal bool IsAssigned(string email)
        {
            if (email != null && email == AssigneeEmail)
            {
                return true;
            }
            return false;
        }

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