using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;

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
                return JsonSerializer.Serialize(response);
            return ret;
        }
        private string GenerateBadResponseString(string errMsg)
        {
            return "{ErrorMessage: " + errMsg + ", ReturnValue: null}";
        }
        private string GenerateGoodResponseString(string value)
        {
            return "{ErrorMessage: null, ReturnValue: " + value + "}";
        }

        public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string newTitle)
        {
            if (!uc.exists(email)) //The user doesn't exist
            {
                return JsonSerializer.Serialize(new Response("The user trying to access does not exist.", true));
            }
            if (!uc.IsLoggedIn(email)) //The user isn't logged in
            {
                return JsonSerializer.Serialize(new Response("The user trying to access is not logged in.",true));
            }
            if (bc.GetBoard(email, boardName) == null) //The board doesn't exist
            {
                return JsonSerializer.Serialize(new Response("The specified board does not exist.", true));
            }
            if (bc.GetTaskInColumn(email, boardName, columnOrdinal, taskId) == null) //Task doesn't exist in the given board & column.
            {
                return JsonSerializer.Serialize(new Response("The specified task does not exist.",true));
            }
            Board board = bc.GetBoard(email, boardName);
            Column column = board.GetColumn(columnOrdinal);
            BusinessLayer.Task task = bc.GetTaskInColumn(email, boardName, columnOrdinal, taskId);
            return InvokeMethod(new Func<BusinessLayer.Task, string, Response>(column.UpdateTaskTitle), "{}", task, newTitle);
        }

        public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string newDesc)
        {
            if (!uc.exists(email)) //The user doesn't exist
            {
                return JsonSerializer.Serialize(new Response("The user trying to access does not exist.", true));
            }
            if (!uc.IsLoggedIn(email)) //The user isn't logged in
            {
                return JsonSerializer.Serialize(new Response("The user trying to access is not logged in.", true));
            }
            if (bc.GetBoard(email, boardName) == null) //The board doesn't exist
            {
                return JsonSerializer.Serialize(new Response("The specified board does not exist.", true));
            }
            if (bc.GetTaskInColumn(email, boardName, columnOrdinal, taskId) == null) //Task doesn't exist in the given board & column.
            {
                return JsonSerializer.Serialize(new Response("The specified task does not exist.", true));
            }
            Board board = bc.GetBoard(email, boardName);
            Column column = board.GetColumn(columnOrdinal);
            BusinessLayer.Task task = bc.GetTaskInColumn(email, boardName, columnOrdinal, taskId);
            return InvokeMethod(new Func<BusinessLayer.Task, string, Response>(column.UpdateTaskDescription), "{}", task, newDesc);
        }

        public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime newDueDate)
        {
            if (!uc.exists(email)) //The user doesn't exist
            {
                return JsonSerializer.Serialize(new Response("The user trying to access does not exist.", true));
            }
            if (!uc.IsLoggedIn(email)) //The user isn't logged in
            {
                return JsonSerializer.Serialize(new Response("The user trying to access is not logged in.", true));
            }
            if (bc.GetBoard(email, boardName) == null) //The board doesn't exist
            {
                return JsonSerializer.Serialize(new Response("The specified board does not exist.", true));
            }
            if (bc.GetTaskInColumn(email, boardName, columnOrdinal, taskId) == null) //Task doesn't exist in the given board & column.
            {
                return JsonSerializer.Serialize(new Response("The specified task does not exist.", true));
            }
            Board board = bc.GetBoard(email, boardName);
            Column column = board.GetColumn(columnOrdinal);
            BusinessLayer.Task task = bc.GetTaskInColumn(email, boardName, columnOrdinal, taskId);
            return InvokeMethod(new Func<BusinessLayer.Task, DateTime, Response>(column.UpdateTaskDueDate), "{}", task, newDueDate);
        }
    }
}
