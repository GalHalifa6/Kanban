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
                return response.ErrorMessage;
            return ret;
        }

        private string GenerateGoodResponseString(string value)
        {
            return "{ErrorMessage: null, ReturnValue: " + value +"}";
        }

        public string AddBoard(string email, string name)
        {
            return InvokeMethod(bc.AddBoard, "{}", email, name);
        }

        public string RemoveBoard(string email, string name) {
            return InvokeMethod(bc.RemoveBoard, "{}", email, name);
        }

        /*public string addTask(string title, string description) {
            throw new NotImplementedException();
        }

        public string removeTask(string title) {
            throw new NotImplementedException();
        }

        public string advanceTask(string title) {
            throw new NotImplementedException();    
        }*/

        public string LimitColumn(string email, string boardName, int columnNumber, int newLimit) {
            return InvokeMethod(bc.LimitColumnTasks, "{}", email, boardName, columnNumber, newLimit);
        }

        public string GetColumnLimit(string email, string boardName, int columnNumber)
        {
            Response<int> response = bc.GetColumnLimit(email, boardName, columnNumber);
            if (response.ErrorOccured) 
                return response.ErrorMessage;
            return GenerateGoodResponseString(response.Result.ToString());
        }

        public string GetColumn(string email, string boardName, int columnNumber)
        {
            Response<string> response = bc.GetColumn(email, boardName, columnNumber);
            if (response.ErrorOccured)
                return response.ErrorMessage;
            return GenerateGoodResponseString(response.Result.ToString());
        }

        /*
                public string getInProgressTasks()
                {
                    throw new NotImplementedException();
                }*/


    }
}
