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
        public string name { get; set; }
        public int boardID { get; }
        public int ordinal{ get; }
        public int maxTasks { get; private set; }
        public HashSet<TaskDTO> tasks { get; private set; }

        public ColumnDTO(int boardID, string name, HashSet<TaskDTO> tasks)
        {
            this.boardID = boardID;
            this.name = name;
            this.maxTasks = int.MaxValue;
            if (name == "backlog")
            {
                ordinal = 0;
            }
            else if (name == "inProgress")
            {
                ordinal = 1;
            }
            else
            {
                ordinal = 2;
            }
        }

        public ColumnDTO(Column column)
        {
            name = column.name;
            maxTasks = column.maxTasks;
            if (column.name == "backlog")
            {
                ordinal = 0;
            }
            else if (column.name == "inProgress")
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
            string query = $"INSERT INTO Tasks(id, title, description, dueDate, assignee) VALUES({taskDTO.Id},'{taskDTO.Title}','{taskDTO.Description}','{taskDTO.DueDate}', 'null')";
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
    }
}
