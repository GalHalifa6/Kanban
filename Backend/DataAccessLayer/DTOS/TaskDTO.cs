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
        private int boardID;
        public int BoardID
        {
            get => boardID;
        }

        private int columnOrdinal;
        public int ColumnOrdinal
        {
            get => columnOrdinal;
            set => columnOrdinal = value;
        }

        private int id;
        public int Id
        {
            get => id;
        }

        private DateTime creationTime;
        public DateTime CreationTime
        {
            get => creationTime;
        }

        private string title;
        public string Title
        {
            get => title;
        }

        private string description;
        public string Description
        {
            get => description;
            set => description = value;
        }
        private DateTime dueDate;
        public DateTime DueDate
        {
            get => dueDate;
            set => dueDate = value;
        }

        private string assigneeEmail;
        public string AssigneeEmail
        {
            get => assigneeEmail;
            set => assigneeEmail = value;
        }

        public TaskDTO(int boardID, int columnOrdinal, int ID, string name, string description, DateTime dueDate)
        {
            this.boardID = boardID;
            this.columnOrdinal = columnOrdinal;
            this.id = ID;
            this.title = name;
            this.description = description;
            this.creationTime = DateTime.Now;
            this.dueDate = dueDate;
            this.assigneeEmail = null;
        }

        public TaskDTO(BusinessLayer.Task task)
        {
            this.boardID = task.BoardID;
            this.columnOrdinal = task.ColumnOrdinal;
            this.id = task.Id;
            this.title = task.Title;
            this.description = task.Description;
            this.dueDate = task.DueDate;
            this.creationTime = task.CreationTime;
            this.assigneeEmail = null;
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
            title = newTitle;
        }
        /// <summary>
        /// Update a task's description
        /// </summary>
        /// <param name="newDesc">The new description</param>
        internal void UpdateTaskDescription(string newDesc)
        {
            string query = $"UPDATE Tasks SET description = '{newDesc}' WHERE id = {Id} AND boardId = {boardID}";
            GeneralNonQuery(query, "Something went wrong");
            description = newDesc;
        }
        /// <summary>
        /// Update a task's due date
        /// </summary>
        /// <param name="newDueDate">The new due date</param>
        internal void UpdateTaskDueDate(DateTime newDueDate)
        {
            string query = $"UPDATE Tasks SET dueDate = '{newDueDate}' WHERE id = {Id} AND boardId = {boardID}";
            GeneralNonQuery(query, "Something went wrong");
            dueDate = newDueDate;
        }

        internal void AdvanceTask()
        {
            columnOrdinal = columnOrdinal + 1;
            string query = $"UPDATE Tasks SET columnOrdinal = {columnOrdinal} WHERE boardID = {boardID} " +
                $"AND id = {id}";
            GeneralNonQuery(query, "Something went wrong");
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
            assigneeEmail = assignee;
        }
        /// <summary>
        /// Unassign task from its' user
        /// </summary>
        internal void UnassignTask()
        {
            assigneeEmail = null;
            string query = $"UPDATE Tasks SET assignee = 'null' WHERE id = {Id} AND boardId = {boardID}";
            GeneralNonQuery(query, "Something went wrong");
        }
    }
}