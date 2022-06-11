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

        public ColumnDTO(string name, int maxTasks, HashSet<TaskDTO> tasks)
        {
            this.name = name;
            this.maxTasks = maxTasks;
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
        private Response GeneralNonQuery(string query, string goodMsg, string badMsg)
        {
            if (!DBConnector.GetInstance().ExecuteNonQuery(query))
            {
                return new Response(badMsg, true);
            }
            return new Response(goodMsg);
        }
        internal Response RemoveTask(int id)
        {
            string query = $"DELETE FROM Tasks WHERE id = {id}";
            Response r = GeneralNonQuery(query, "Task was removed successfully", "Something went wrong");
            if (!r.ErrorOccured())
            {
                foreach(TaskDTO task in tasks)
                {
                    if(task.Id == id)
                    {
                        tasks.Remove(task);
                        break;
                    }
                }
            }
            return r;
        }

        internal Response AddTask(TaskDTO task)
        {

            //int ID, string title, string description, DateTime dueDate
            string query = $"INSERT INTO Tasks(id, title, description, dueDate, assignee) VALUES({task.Id},'{task.Title}',{task.Description},'{task.DueDate}', 'null')";
            Response r = GeneralNonQuery(query, "Task was added successfully", "A task with this id already exists");
            if (!r.ErrorOccured())
                tasks.Add(task);
            return r;
        }

        internal Response SetMax(int newLimit)
        {
            string query = $"UPDATE Columns SET maxTasks = '{newLimit}' WHERE columnOrdinal = {ordinal}";
            maxTasks = newLimit;
            return GeneralNonQuery(query, "Limit of tasks was updated successfully", "Something went wrong");
        }
    }
}
