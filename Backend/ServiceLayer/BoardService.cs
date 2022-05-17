using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;
using System.Text.Json;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardService
    {
        private BoardController bc { get; }

        public BoardService()
        {
            bc = new BoardController();
        }

        private string InvokeMethod(Delegate method, string ret, params object[] args)
        {
            Response<bool> response = (Response<bool>) method.DynamicInvoke(args);
            if (response.ErrorOccured)
                return GenerateBadResponseString(response.ErrorMessage);
            return GenerateGoodResponseString(ret);
        }
        private string GenerateBadResponseString(string errMsg)
        {
            return "{ErrorMessage: " + errMsg + ", ReturnValue: null}";
        }
        private string GenerateGoodResponseString(string value)
        {
            return "{ErrorMessage: null, ReturnValue: " + value +"}";
        }

        public string AddBoard(string email, string name)
        {
            if (name == "")
            {
                return GenerateBadResponseString("Cannot have empty board name");
            }
            return InvokeMethod(new Func<string, string, Response<bool>>(bc.AddBoard), "{}", email, name);
        }

        public string RemoveBoard(string email, string name) {
            return InvokeMethod(new Func<string, string, Response<bool>>(bc.RemoveBoard), "{}", email, name);
        }

        public string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            return InvokeMethod(new Func<string, string, string, string, DateTime, Response<string>>(bc.AddTask), email,
                email, boardName, title, description, dueDate);
        }

        public string removeTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            throw new NotImplementedException();
        }


        public string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            return InvokeMethod(new Func<string, string, int ,int, Response<string>>(bc.AdvanceTask), "{}", email,
                boardName, columnOrdinal, taskId);
        }

        public string LimitColumn(string email, string boardName, int columnNumber, int newLimit) {
            return InvokeMethod(new Func<string, string, int, int, Response<bool>>(bc.LimitColumnTasks), "{}", email, boardName, columnNumber, newLimit);
        }

        public string GetColumnLimit(string email, string boardName, int columnNumber)
        {
            Response<int> response = bc.GetColumnLimit(email, boardName, columnNumber);
            if (response.ErrorOccured) 
                return GenerateBadResponseString(response.ErrorMessage);
            return GenerateGoodResponseString(response.Result.ToString());
        }

        public string GetColumnName(string email, string boardName, int columnNumber)
        {
            Response<string> response = bc.GetColumnName(email, boardName, columnNumber);
            if (response.ErrorOccured)
                return GenerateBadResponseString(response.ErrorMessage);
            return GenerateGoodResponseString(response.Result.ToString());
        }

        public string GetColumn(string email, string boardName, int columnOrdinal)
        {
            Response<string> response = bc.GetColumn(email, boardName, columnOrdinal);
            if (response.ErrorOccured)
                return GenerateBadResponseString(response.ErrorMessage);
            return GenerateGoodResponseString(response.Result.ToString());
        }

        public string InProgressTasks(string email)
        {
            Response<string> response = bc.InProgressTasks(email);
            if (response.ErrorOccured)
                return GenerateBadResponseString(response.ErrorMessage);
            return GenerateGoodResponseString(response.Result.ToString());
        }



    }
}
