using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class ColumnDTO
    {
        private string name;
        public string Name
        {
            get => name;
        }
        private int boardID;
        public int BoardID
        {
            get => boardID;
        }
        private int ordinal;
        public int Ordinal
        {
            get => ordinal;
        }
        private int maxTasks;
        public int MaxTasks
        {
            get => maxTasks;
            set => maxTasks = value;
        }
        private HashSet<TaskDTO> tasks;
        public HashSet<TaskDTO> Tasks
        {
            get => tasks;
            set => tasks = value;
        }

        public ColumnDTO(int boardID, string name, HashSet<TaskDTO> tasks)
        {
            this.boardID = boardID;
            this.name = name;
            this.maxTasks = int.MaxValue;
            if (name == "backlog")
            {
                ordinal = 0;
            }
            else if (name == "in progress")
            {
                ordinal = 1;
            }
            else
            {
                ordinal = 2;
            }
            this.tasks = tasks;
        }

        public ColumnDTO(Column column)
        {
            name = column.name;
            maxTasks = column.maxTasks;
            if (column.name == "backlog")
            {
                ordinal = 0;
            }
            else if (column.name == "in progress")
            {
                ordinal = 1;
            }
            else
            {
                ordinal = 2;
            }
            tasks = new HashSet<TaskDTO>();
            for (int i = 0; i < column.tasks.Count; i++)
            {
                TaskDTO t = new TaskDTO(column.tasks[i]);
                tasks.Add(t);
            }
        }
        private void GeneralNonQuery(string query, string badMsg)
        {
            if (!DBConnector.GetInstance().ExecuteNonQuery(query))
            {
                throw new Exception(badMsg);
            }
        }
        /// <summary>
        /// Remove a task
        /// </summary>
        /// <param name="id">The task id</param>
        internal void RemoveTask(int id)
        {
            string query = $"DELETE FROM Tasks WHERE id = {id} AND boardId = {boardID}";
            GeneralNonQuery(query, "Something went wrong");
            foreach(TaskDTO task in tasks)
            {
                if(task.Id == id)
                {
                    tasks.Remove(task);
                    break;
                }
            }
        }
        /// <summary>
        /// Add a task
        /// </summary>
        /// <param name="taskDTO">The task</param>
        internal void AddTask(TaskDTO taskDTO)
        {
            string query = $"INSERT INTO Tasks(boardID, columnOrdinal,id, title, description, dueDate, assignee) VALUES({taskDTO.boardID},{taskDTO.columnOrdinal},{taskDTO.Id},'{taskDTO.Title}','{taskDTO.Description}','{taskDTO.DueDate}', 'null')";
            GeneralNonQuery(query, "A task with this id already exists");
        }
        /// <summary>
        /// Set a limitation on the tasks number in this column
        /// </summary>
        /// <param name="newLimit"></param>
        internal void SetMax(int newLimit)
        {
            string query = $"UPDATE Columns SET maxTasks = '{newLimit}' WHERE columnOrdinal = {ordinal} AND boardID = {boardID}";
            maxTasks = newLimit;
            GeneralNonQuery(query, "Something went wrong");
        }

        internal void AddColumnToDB()
        {
            string query = $"INSERT INTO Columns(boardID,columnOrdinal,maxTasks) VALUES({boardID},{ordinal},{maxTasks})";
            GeneralNonQuery(query, "Something went wrong");
        }
    }
}
