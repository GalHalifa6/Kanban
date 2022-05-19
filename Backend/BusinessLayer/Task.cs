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

        //private Boolean isDone;
        

        public Task(int ID, string name, string description, DateTime dueDate)
        {
            this.Id = ID;
            this.Title = name;
            this.Description = description;
            CreationTime = DateTime.Now;
            this.DueDate = dueDate;
            //isDone = false;
        }

        public void UpdateTaskTitle(string newTitle)
        {
            Title = newTitle;
        }
        public void UpdateTaskDescription(string newDescription)
        {
            Description = newDescription;
        }

        public void UpdateTaskDueDate(DateTime newDueDate)
        {
            DueDate = newDueDate;
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