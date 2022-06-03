using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class TasksColumnsBoardsDTO
    {

        public TasksColumnsBoardsDTO() { }


        private Response GeneralNonQuery(string query, string goodMsg, string badMsg)
        {
            if (!DBConnector.instance.ExecuteNonQuery(query))
            {
                return new Response(badMsg, true);
            }
            return new Response(goodMsg);
        }
        internal Response AdvanceTask(int boardID, int ordinalORG, int ordinalDEST, int taskID)
        {
            string query = $"UPDATE TasksColumnsBoardsDTO SET columnOrdinal = {ordinalDEST} WHERE boardID = {boardID} " +
                $"AND columnOrdinal = {ordinalORG} AND taskID = {taskID}";
            return GeneralNonQuery(query, "Task advnaced", "Something went wrong");
        }
    }
}
