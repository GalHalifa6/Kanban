using IntroSE.Kanban.Backend.BusinessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    /// <summary>
    /// Facade class for the service layer. For descripitions of the methods see GradingService or the system's service classes.
    /// </summary>
    public class ServiceController
    {

        private UserService US { get; set; }
        private BoardService BS { get; set; }
        private TaskService TS { get; set; }

        public ServiceController()
        {

            US = new UserService();
            BS = new BoardService();
            TS = new TaskService(US.uc, BS.bc);
        }

        internal string Register(string email, string password)
        {
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            string res = US.Register(email, password);
            if (res == "{}")
                BS.Register(email);
            return res;
        }

        internal string Login(string email, string password)
        {
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            return US.Login(email, password);
        }

        internal string Logout(string email)
        {
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            return US.Logout(email);
        }

        internal string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            if (!US.IsLoggedIn(email))
                return GenerateBadResponseString("You must be logged in to perform this action");
            return BS.LimitColumn(email, boardName, columnOrdinal, limit);  
        }

        internal string GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            if (!US.IsLoggedIn(email))
                return GenerateBadResponseString("You must be logged in to perform this action");
            return BS.GetColumnLimit(email, boardName, columnOrdinal);
        }

        internal string GetColumnName(string email, string boardName, int columnOrdinal)
        {
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            if (!US.IsLoggedIn(email))
                return GenerateBadResponseString("You must be logged in to perform this action");
            return BS.GetColumnName(email, boardName, columnOrdinal);
        }

        internal string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            if (!US.IsLoggedIn(email))
                return GenerateBadResponseString("You must be logged in to perform this action");
            return BS.AddTask(email, boardName, title, description, dueDate);
        }

        internal string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            if (!US.IsLoggedIn(email))
                return GenerateBadResponseString("You must be logged in to perform this action");
            return TS.UpdateTaskDueDate(email, boardName, columnOrdinal, taskId, dueDate);
        }

        internal string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            if (!US.IsLoggedIn(email))
                return GenerateBadResponseString("You must be logged in to perform this action");
            return TS.UpdateTaskTitle(email, boardName, columnOrdinal, taskId, title);
        }

        internal string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            if (!US.IsLoggedIn(email))
                return GenerateBadResponseString("You must be logged in to perform this action");
            return TS.UpdateTaskDescription(email, boardName, columnOrdinal, taskId, description);
        }

        internal string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            if (!US.IsLoggedIn(email))
                return GenerateBadResponseString("You must be logged in to perform this action");
            return BS.AdvanceTask(email, boardName, columnOrdinal, taskId);
        }

        internal string GetColumn(string email, string boardName, int columnOrdinal)
        {
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            if (!US.IsLoggedIn(email))
                return GenerateBadResponseString("You must be logged in to perform this action");
            return BS.GetColumn(email, boardName, columnOrdinal);
        }

        internal string AddBoard(string email, string name)
        {
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            if (!US.IsLoggedIn(email))
                return GenerateBadResponseString("You must be logged in to perform this action");
            return BS.AddBoard(email, name);
        }

        internal string RemoveBoard(string email, string name)
        {
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            if (!US.IsLoggedIn(email))
                return GenerateBadResponseString("You must be logged in to perform this action");
            return BS.RemoveBoard(email, name);
        }

        internal string InProgressTasks(string email)
        {
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            if (!US.IsLoggedIn(email))
                return GenerateBadResponseString("You must be logged in to perform this action");
            return BS.InProgressTasks(email);
        }

        private string GenerateBadResponseString(string s)
        {
            Response r = new Response(s, true);
            return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
