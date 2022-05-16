using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    internal class TaskService
    {
        private UserController uc { get; }
        private BoardController bc { get; }

        public TaskService()
        {
            uc = new UserController();
            bc = new BoardController();
        }

        internal Response<bool> editTaskTitle(string email, string boardName, string taskName, string newTitle)
        {
            if (!uc.exists(email)) //The user doesn't exist
            {
                return new Response<bool>("The user trying to access does not exist.");
            }
            if (!uc.isLoggedIn(email)) //The user isn't logged in
            { 
                return new Response<bool>("The user trying to access is not logged in.");
            }
            if (bc.GetBoard(email, boardName) == null) //The board doesn't exist
            {
                return new Response<bool>("The specified board does not exist.");
            }
            if (bc.GetTask(email, boardName, taskName) == null) //Task doesn't exist in the given board
            {
                return new Response<bool>("The specified task does not exist.");
            }
            BusinessLayer.Task task = bc.GetTask(email, boardName, taskName);
            task.editTaskTitle(newTitle);
            return new Response<bool>(true);
        }

        internal Response<bool> editTaskDescription(string email, string boardName, string taskName, string newDesc)
        {
            if (!uc.exists(email)) //The user doesn't exist
            {
                return new Response<bool>("The user trying to access does not exist.");
            }
            if (!uc.isLoggedIn(email)) //The user isn't logged in
            {
                return new Response<bool>("The user trying to access is not logged in.");
            }
            if (bc.GetBoard(email, boardName) == null) //The board doesn't exist
            {
                return new Response<bool>("The specified board does not exist.");
            }
            if (bc.GetTask(email, boardName, taskName) == null) //Task doesn't exist in the given board
            {
                return new Response<bool>("The specified task does not exist.");
            }
            BusinessLayer.Task task = bc.GetTask(email, boardName, taskName);
            task.editTaskDescription(newDesc);
            return new Response<bool>(true);
        }

        internal Response<bool> editTaskDueDate(string email, string boardName, string taskName, DateTime newDueDate)
        {
            if (!uc.exists(email)) //The user doesn't exist
            {
                return new Response<bool>("The user trying to access does not exist.");
            }
            if (!uc.isLoggedIn(email)) //The user isn't logged in
            {
                return new Response<bool>("The user trying to access is not logged in.");
            }
            if (bc.GetBoard(email, boardName) == null) //The board doesn't exist
            {
                return new Response<bool>("The specified board does not exist.");
            }
            if (bc.GetTask(email, boardName, taskName) == null) //Task doesn't exist in the given board
            {
                return new Response<bool>("The specified task does not exist.");
            }
            BusinessLayer.Task task = bc.GetTask(email, boardName, taskName);
            task.editTaskDueDate(newDueDate);
            return new Response<bool>(true);
        }
    }
}
