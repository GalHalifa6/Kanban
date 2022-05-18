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
        public BoardController bc { get; }

        public BoardService()
        {
            bc = new BoardController();
        }

        private string InvokeMethod(Delegate method, string ret, params object[] args)
        {
            Response response = (Response) method.DynamicInvoke(args);
            if (response.ErrorOccured())
                return JsonSerializer.Serialize(response);
            return ret;
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
            if (string.IsNullOrWhiteSpace(name))
            {
                return JsonSerializer.Serialize(new Response("Cannot have an empty board name", true));
            }
            return InvokeMethod(new Func<string, string, Response>(bc.AddBoard), "{}", email, name);
        }

        public string RemoveBoard(string email, string name) {
            return InvokeMethod(new Func<string, string, Response>(bc.RemoveBoard), "{}", email, name);
        }

        public string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            
            Response response = bc.AddTask(email, boardName, title, description, dueDate);
            return JsonSerializer.Serialize(response);
        }

        public string removeTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            throw new NotImplementedException();
        }


        public string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            return InvokeMethod(new Func<string, string, int ,int, Response>(bc.AdvanceTask), "{}", email,
                boardName, columnOrdinal, taskId);
        }

        public string LimitColumn(string email, string boardName, int columnNumber, int newLimit) {
            return InvokeMethod(new Func<string, string, int, int, Response>(bc.LimitColumnTasks), "{}", email, boardName, columnNumber, newLimit);
        }

        public string GetColumnLimit(string email, string boardName, int columnNumber)
        {
            Response response = bc.GetColumnLimit(email, boardName, columnNumber);
            /*            if (response.ErrorOccured()) 
                            return GenerateBadResponseString(response.ErrorMessage);
                        return GenerateGoodResponseString(response.ReturnValue.ToString());*/
            return JsonSerializer.Serialize(response);
        }

        public string GetColumnName(string email, string boardName, int columnNumber)
        {
            Response response = bc.GetColumnName(email, boardName, columnNumber);
            /*if (response.ErrorOccured())
                            return GenerateBadResponseString(response.ErrorMessage);
                        return GenerateGoodResponseString(response.ReturnValue.ToString());*/
            return JsonSerializer.Serialize(response);
        }

        public string GetColumn(string email, string boardName, int columnOrdinal)
        {
            Response response = bc.GetColumn(email, boardName, columnOrdinal);
            /*            if (response.ErrorOccured())
                            return GenerateBadResponseString(response.ErrorMessage);
                        return GenerateGoodResponseString(response.ReturnValue.ToString());*/
            return JsonSerializer.Serialize(response);
        }

        public string InProgressTasks(string email)
        {
            Response response = bc.InProgressTasks(email);
            /* if (response.ErrorOccured())
                            return GenerateBadResponseString(response.ErrorMessage);
                        return GenerateGoodResponseString(response.ReturnValue.ToString());*/
            return JsonSerializer.Serialize(response);
        }



    }
}
