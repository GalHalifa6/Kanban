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

        public String AssigneeEmail { get; set; }
        public int boardID { get; private set; }
        public int columnOrdinal { get; private set; }

        public TaskDTO(int boardID, int columnOrdinal, int ID, string name, string description, DateTime dueDate)
        {
            this.boardID = boardID;
            this.columnOrdinal = columnOrdinal;
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
            this.boardID = task.boardID;
            this.columnOrdinal = task.columnOrdinal;
            this.Id = task.Id;
            this.Title = task.Title;
            this.Description = task.Description;
            this.DueDate = task.DueDate;
            CreationTime = task.CreationTime;
            AssigneeEmail = null;
        }

        private void GeneralNonQuery(string query, string badMsg)
        {
            if (!DBConnector.GetInstance().ExecuteNonQuery(query))
            {
                throw new Exception(badMsg);
            }
        }
        /// <summary>
        /// Update a task's title
        /// </summary>
        /// <param name="newTitle">The new title</param>
        internal void UpdateTaskTitle(string newTitle)
        {
            string query = $"UPDATE Tasks SET newTitle = '{newTitle}' WHERE id = {Id} AND boardId = {boardID}";
            GeneralNonQuery(query, "Something went wrong");
            Title = newTitle;
        }
        /// <summary>
        /// Update a task's description
        /// </summary>
        /// <param name="newDesc">The new description</param>
        internal void UpdateTaskDescription(string newDesc)
        {
            string query = $"UPDATE Tasks SET description = '{newDesc}' WHERE id = {Id} AND boardId = {boardID}";
            GeneralNonQuery(query, "Something went wrong");
            Description = newDesc;
        }
        /// <summary>
        /// Update a task's due date
        /// </summary>
        /// <param name="newDueDate">The new due date</param>
        internal void UpdateTaskDueDate(DateTime newDueDate)
        {
            string query = $"UPDATE Tasks SET dueDate = '{newDueDate}' WHERE id = {Id} AND boardId = {boardID}";
            GeneralNonQuery(query, "Something went wrong");
            DueDate = newDueDate;
        }
        /// <summary>
        /// Assign a task to a user
        /// </summary>
        /// <param name="assigner">The assigner</param>
        /// <param name="assignee">The assigned user</param>
        internal void AssignTask(string assigner, string assignee)
        {
            string query = $"UPDATE Tasks SET assignee = '{assignee}' WHERE id = {Id} AND boardId = {boardID}";
            GeneralNonQuery(query, "Something went wrong");
            AssigneeEmail = assignee;
        }
        /// <summary>
        /// Unassign task from its' user
        /// </summary>
        internal void UnassignTask()
        {
            string query = $"UPDATE Tasks SET assignee = 'null' WHERE id = {Id} AND boardId = {boardID}";
            GeneralNonQuery(query, "Something went wrong");
        }
    }
}