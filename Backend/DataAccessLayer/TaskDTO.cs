using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class TaskDTO
    {
        public int Id { get; private set; }
        public DateTime CreationTime { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }

        public DateTime DueDate { get; private set; }

        public TaskDTO dto { get; private set; }

        public String AssigneeEmail { get; private set; }

        public TaskDTO(int ID, string name, string description, DateTime dueDate)
        {
            this.Id = ID;
            this.Title = name;
            this.Description = description;
            CreationTime = DateTime.Now;
            this.DueDate = dueDate;
            AssigneeEmail = null;
            //isDone = false;
        }

        public TaskDTO(BusinessLayer.Task task)
        {
            this.Id = task.Id;
            this.Title = task.Title;
            this.Description = task.Description;
            this.DueDate = task.DueDate;
            CreationTime = task.CreationTime;
            AssigneeEmail = null;
        }

        private Response GeneralNonQuery(string query, string goodMsg, string badMsg)
        {
            if (!DBConnector.GetInstance().ExecuteNonQuery(query))
            {
                return new Response(badMsg, true);
            }
            return new Response(goodMsg);
        }

        internal Response UpdateTaskTitle(string newTitle)
        {
            string query = $"UPDATE Tasks SET newTitle = '{newTitle}' WHERE id = {Id}";
            Response r = GeneralNonQuery(query, "Task's title was updated successfully", "Something went wrong");
            if (!r.ErrorOccured())
            {
                Title = newTitle;
            }
            return r;
        }

        internal Response UpdateTaskDescription(string newDesc)
        {
            string query = $"UPDATE Tasks SET description = '{newDesc}' WHERE id = {Id}";
            Response r = GeneralNonQuery(query, "Task's description was updated successfully", "Something went wrong");
            if (!r.ErrorOccured())
            {
                Description = newDesc;
            }
            return r;
        }

        internal Response UpdateTaskDueDate(DateTime newDueDate)
        {
            string query = $"UPDATE Tasks SET dueDate = '{newDueDate}' WHERE id = {Id}";
            Response r = GeneralNonQuery(query, "Task's due date was updated successfully", "Something went wrong");
            if (!r.ErrorOccured())
            {
                DueDate = newDueDate;
            }
            return r;
        }

        internal Response AssignTask(string assigner, string assignee)
        {
            string query = $"UPDATE Tasks SET assignee = '{assignee}' WHERE id = {Id}";
            Response r = GeneralNonQuery(query, "Task's assignee was updated successfully", "Something went wrong");
            if (!r.ErrorOccured())
            {
                AssigneeEmail = assignee;
            }
            return r;
        }

        internal Response UnassignTask()
        {
            string query = $"UPDATE Tasks SET assignee = 'null' WHERE id = {Id}";
            return GeneralNonQuery(query, "Task's assignee was updated successfully", "Something went wrong");
        }
    }
}