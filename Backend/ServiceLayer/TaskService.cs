using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;
using Newtonsoft.Json;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    internal class TaskService
    {
        private UserController uc { get; }
        private BoardController bc { get; }

        public TaskService(UserController uc, BoardController bc)
        {
            this.uc = uc;
            this.bc = bc;   
        }

        private string InvokeMethod(Delegate method, string ret, params object[] args)
        {
            Response response = (Response)method.DynamicInvoke(args);
            if (response.ErrorOccured())
                return JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return ret;
        }

        /// <summary>
        /// update an existing tasks title
        /// </summary>
        /// <param name="email">user holding the board that holds the task</param>
        /// <param name="boardName">board holding the column that holds task</param>
        /// <param name="columnOrdinal">column holding the task</param>
        /// <param name="taskId">the task's id</param>
        /// <param name="newTitle">the new title</param>
        /// <returns>Json response with the result of the procedure</returns>
        public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string newTitle)
        {
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            if (string.IsNullOrEmpty(newTitle) || string.IsNullOrWhiteSpace(newTitle))
            {
                Response r = new Response("Cannot have an empy title.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            if (newTitle.Length > 50)
            {
                Response r = new Response("Title is too long. Max number of characters is 50", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            if (!uc.exists(email)) //The user doesn't exist
            {
                Response r = new Response("The user trying to access does not exist.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            if (!uc.IsLoggedIn(email)) //The user isn't logged in
            {
                Response r = new Response("The user trying to access is not logged in.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            if (bc.GetBoard(email, boardName) == null) //The board doesn't exist
            {
                Response r = new Response("The specified board does not exist.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            if (bc.GetTaskInColumn(email, boardName, columnOrdinal, taskId) == null) //Task doesn't exist in the given board & column.
            {
                Response r = new Response("The specified task does not exist.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            Board board = bc.GetBoard(email, boardName);
            if (board == null)
            {
                Response r = new Response("Board " + boardName + " does not exist", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            }
            Column column = board.GetColumn(columnOrdinal);
            if (column == null)
            {
                Response r = new Response("Invalid column ordinal.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            }
            if (column.name == "done")
            {
                Response r = new Response("Cannot edit tasks that are done.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            }
            BusinessLayer.Task task = bc.GetTaskInColumn(email, boardName, columnOrdinal, taskId);
            return InvokeMethod(new Func<BusinessLayer.Task, string, Response>(column.UpdateTaskTitle), "{}", task, newTitle);
        }

        /// <summary>
        /// update an existing tasks description
        /// </summary>
        /// <param name="email">user holding the board that holds the task</param>
        /// <param name="boardName">board holding the column that holds task</param>
        /// <param name="columnOrdinal">column holding the task</param>
        /// <param name="taskId">the task's id</param>
        /// <param name="newDesc">the new description</param>
        /// <returns>Json response with the result of the procedure</returns>
        public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string newDesc)
        {
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            if (newDesc == null)
                newDesc = "";
            if (newDesc.Length > 300)
            {
                Response r = new Response("Description is too long. Max number of characters is 300", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            if (!uc.exists(email)) //The user doesn't exist
            {
                Response r = new Response("The user trying to access does not exist.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            if (!uc.IsLoggedIn(email)) //The user isn't logged in
            {
                Response r = new Response("The user trying to access is not logged in.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            if (bc.GetBoard(email, boardName) == null) //The board doesn't exist
            {
                Response r = new Response("The specified board does not exist.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            if (bc.GetTaskInColumn(email, boardName, columnOrdinal, taskId) == null) //Task doesn't exist in the given board & column.
            {
                Response r = new Response("The specified task does not exist.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            Board board = bc.GetBoard(email, boardName);
            if (board == null)
            {
                Response r = new Response("Board " + boardName + " does not exist", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            }
            Column column = board.GetColumn(columnOrdinal);
            if (column == null)
            {
                Response r = new Response("Invalid column ordinal.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            }
            if (column.name == "done")
            {
                Response r = new Response("Cannot edit tasks that are done.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            }
            BusinessLayer.Task task = bc.GetTaskInColumn(email, boardName, columnOrdinal, taskId);
            return InvokeMethod(new Func<BusinessLayer.Task, string, Response>(column.UpdateTaskDescription), "{}", task, newDesc);
        }

        /// <summary>
        /// update an existing task's due date
        /// </summary>
        /// <param name="email">user holding the board that holds the task</param>
        /// <param name="boardName">board holding the column that holds task</param>
        /// <param name="columnOrdinal">column holding the task</param>
        /// <param name="taskId">the task's id</param>
        /// <param name="newDueDate">the new due date</param>
        /// <returns>Json response with the result of the procedure</returns>
        public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime newDueDate)
        {
            if (newDueDate < DateTime.Now)
                return JsonConvert.SerializeObject(new Response("Due date cannot be in the past.", true), Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            if (!uc.exists(email)) //The user doesn't exist
            {
                Response r = new Response("The user trying to access does not exist.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            if (!uc.IsLoggedIn(email)) //The user isn't logged in
            {
                Response r = new Response("The user trying to access is not logged in.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            if (bc.GetBoard(email, boardName) == null) //The board doesn't exist
            {
                Response r = new Response("The specified board does not exist.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            if (bc.GetTaskInColumn(email, boardName, columnOrdinal, taskId) == null) //Task doesn't exist in the given board & column.
            {
                Response r = new Response("The specified task does not exist.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            Board board = bc.GetBoard(email, boardName);
            if (board == null)
            {
                Response r = new Response("Board " + boardName + " does not exist", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            }
            Column column = board.GetColumn(columnOrdinal);
            if (column == null)
            {
                Response r = new Response("Invalid column ordinal.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            }
            if (column.name == "done")
            {
                Response r = new Response("Cannot edit tasks that are done.", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            }
            BusinessLayer.Task task = bc.GetTaskInColumn(email, boardName, columnOrdinal, taskId);
            return InvokeMethod(new Func<BusinessLayer.Task, DateTime, Response>(column.UpdateTaskDueDate), "{}", task, newDueDate);
        }

        private string GenerateBadResponseString(string s)
        {
            Response r = new Response(s, true);
            return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
