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
            TS = new TaskService(US.Uc, BS.Bc);
        }

        private string InitialValidation(ref string email)
        {
            if (email == null)
                return GenerateBadResponseString("Email cannot be null");
            email = email.ToLower();
            if (!US.IsLoggedIn(email))
                return GenerateBadResponseString("You must be logged in to perform this action");
            return null;

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
            string res = InitialValidation(ref email);
            if (res != null)
                return res;
            return BS.LimitColumn(email, boardName, columnOrdinal, limit);
        }

        internal string GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            string res = InitialValidation(ref email);
            if (res != null)
                return res;
            return BS.GetColumnLimit(email, boardName, columnOrdinal);
        }

        internal string GetColumnName(string email, string boardName, int columnOrdinal)
        {
            string res = InitialValidation(ref email);
            if (res != null)
                return res;
            return BS.GetColumnName(email, boardName, columnOrdinal);
        }

        internal string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            string res = InitialValidation(ref email);
            if (res != null)
                return res;
            return BS.AddTask(email, boardName, title, description, dueDate);
        }

        internal string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            string res = InitialValidation(ref email);
            if (res != null)
                return res;
            return TS.UpdateTaskDueDate(email, boardName, columnOrdinal, taskId, dueDate);
        }

        internal string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            string res = InitialValidation(ref email);
            if (res != null)
                return res;
            return TS.UpdateTaskTitle(email, boardName, columnOrdinal, taskId, title);
        }

        internal string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            string res = InitialValidation(ref email);
            if (res != null)
                return res;
            return TS.UpdateTaskDescription(email, boardName, columnOrdinal, taskId, description);
        }

        internal string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            string res = InitialValidation(ref email);
            if (res != null)
                return res;
            return BS.AdvanceTask(email, boardName, columnOrdinal, taskId);
        }

        internal string GetColumn(string email, string boardName, int columnOrdinal)
        {
            string res = InitialValidation(ref email);
            if (res != null)
                return res;
            return BS.GetColumn(email, boardName, columnOrdinal);
        }

        internal string AddBoard(string email, string name)
        {
            string res = InitialValidation(ref email);
            if (res != null)
                return res;
            return BS.AddBoard(email, name, US);
        }

        internal string RemoveBoard(string email, string name)
        {
            string res = InitialValidation(ref email);
            if (res != null)
                return res;
            US.RemoveBoard(email, name);
            return BS.RemoveBoard(email, name);
        }

        internal string InProgressTasks(string email)
        {
            string res = InitialValidation(ref email);
            if (res != null)
                return res;
            return US.InProgressTasks(email);
        }

        private string GenerateBadResponseString(string s)
        {
            Response r = new Response(s, true);
            return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }


        internal string GetUserBoards(string email)
        {
            string res = InitialValidation(ref email);
            if (res != null)
                return res;
            string r = US.GetUserBoards(email);
            return r;
        }

        internal string JoinBoard(string email, int boardID)
        {
            string res = InitialValidation(ref email);
            if (res != null)
                return res;

            string r = BS.JoinBoard(email, boardID, US);
            if (r == "{}")
            {
                return r;
            }
            return GenerateBadResponseString(r);
        }


        internal string LeaveBoard(string email, int boardID)
        {
            string res = InitialValidation(ref email);
            if (res != null)
                return res;
            string r = BS.LeaveBoard(email, boardID);
            if (r == "{}")
            {
                US.LeaveBoard(email, boardID);
            }
            return r;
        }

        internal string AssignTask(string email, string boardName, int columnOrdinal, int taskID, string emailAssignee)
        {
            string res = InitialValidation(ref email);
            if (res != null)
                return res;
            if (emailAssignee == null)
            {
                return GenerateBadResponseString("Assignee email cannot be null");
            }
            emailAssignee = emailAssignee.ToLower();
            return BS.AssignTask(email, boardName, columnOrdinal, taskID, emailAssignee);
        }

        internal string LoadData()
        {
            BS.LoadData();
            return US.LoadData();
        }

        internal string DeleteData()
        {
            BS.DeleteData();
            return US.DeleteData();
        }


        internal string TransferOwnership(string currentOwnerEmail, string newOwnerEmail, string boardName)
        {
            string res = InitialValidation(ref currentOwnerEmail);
            if (res != null)
                return res;
            if (newOwnerEmail == null)
                return GenerateBadResponseString("new owner email cannot be null");
            newOwnerEmail = newOwnerEmail.ToLower();
            string r = US.TransferOwnership(currentOwnerEmail, newOwnerEmail, boardName);
            if(r == "{}")
            {
                return r;
            }
            return GenerateBadResponseString(r);
        }

    }
}
