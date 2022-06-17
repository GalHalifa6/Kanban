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

        public static int title_Max_Length;

        public TaskService(UserController uc, BoardController bc)
        {
            this.uc = uc;
            this.bc = bc;
            title_Max_Length = 50;
        }

        private string InvokeMethod(Delegate method, string ret, params object[] args)
        {
            try
            {
                method.DynamicInvoke(args);
                return ret;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new Response(ex.Message, true), Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            }
        }


        /// <summary>
        /// update an existing tasks' title
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
            return InvokeMethod(new Action<string, string, int, int, string>(bc.UpdateTaskTitle), "{}", email, boardName, columnOrdinal, taskId, newTitle);
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
            return InvokeMethod(new Action<string, string, int, int, string>(bc.UpdateTaskDescription), "{}", email, boardName, columnOrdinal, taskId, newDesc);
        }

        internal Response LoadData()
        {
            throw new NotImplementedException();
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
            /*if (newDueDate < DateTime.Now)
                return JsonConvert.SerializeObject(new Response("Due date cannot be in the past.", true), Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
*/          if (email == null)
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
            return InvokeMethod(new Action<string, string, int, int, DateTime>(bc.UpdateTaskDueDate), "{}", email, boardName, columnOrdinal, taskId, newDueDate);
        }

        private string GenerateBadResponseString(string s)
        {
            Response r = new Response(s, true);
            return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
