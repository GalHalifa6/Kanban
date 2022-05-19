using System;
using System.Collections.Generic;

namespace IntroSE.Kanban.Backend.BusinessLayer
{

    public class BoardController
    {
        public Dictionary<string, List<Board>> boards;
        log4net.ILog logger = Utility.Logger.GetLogger();
        //private int nextTaskID { get; set; }


        public BoardController()
        {
            boards = new Dictionary<string, List<Board>>();
            //nextTaskID = 0;

        }

        /// <summary>
        /// Get a specific board
        /// </summary>
        /// <param name="email"> user whose board will be returned</param>
        /// <param name="boardName">board to be returned</param>
        /// <returns>the board that was looked for, null if no such board exists</returns>
        public Board GetBoard(string email, string boardName)
        {
            if (!boards.ContainsKey(email))
            {
                boards.Add(email, new List<Board>());
                return null;
            }
            List<Board> userBoards = boards[email];
            for (int i = 0; i < userBoards.Count; i++)
            {
                if (userBoards[i].name == boardName)
                {
                    return userBoards[i];
                }
            }
            return null;
        }


        public Response AddBoard(string email, string name)
        {
            if (!boards.ContainsKey(email))
            {
                boards[email] = new List<Board>();
            }
            if (GetBoard(email, name) != null)
            {
                logger.Warn("Failed to create board " + name + ", because a board with that name already exists.");
                return new Response("Board with this name already exists", true);
            }
            boards[email].Add(new Board(name));
            logger.Info("board " + name + " created for user " + email);
            return new Response(true);
        }

        /// <summary>
        /// Called when a successful registeration occurs. Adds a new (key,value) pair to the user boards dictionary
        /// </summary>
        /// <param name="email">newly registered user</param>
        internal void Register(string email)
        {
            boards.Add(email, new List<Board>());
        }

        public Response RemoveBoard(string email, string boardName)
        {
            Board board = GetBoard(email, boardName);
            if (board != null)
            {
                boards[email].Remove(board);
                logger.Info("board " + boardName + " removed from user " + email);
                return new Response(true);
            }
            logger.Warn("Failed to remove board " + boardName + ", because a board with that name doesn't exists.");
            return new Response("The board '" + boardName + "' does not exist", true);
        }

        public Response LimitColumnTasks(string email, string boardName, int columnNumber, int newLimit)
        {
            Board board = GetBoard(email, boardName);
            if (board == null)
            {
                logger.Warn("Failed to limit tasks in board " + boardName + ", because a board with that name doesn't exists.");
                return new Response("The board '" + boardName + "' does not exist", true);
            }
            return board.LimitColumnTasks(columnNumber, newLimit);
        }

        public Response GetColumnLimit(string email, string boardName, int columnNumber)
        {
            Board board = GetBoard(email, boardName);
            if (board == null)
            {
                return new Response("The board '" + boardName + "' does not exist", true);
            }
            return board.GetColumnLimit(boardName, columnNumber);
        }

        public Response GetColumnName(string email, string boardName, int columnOrdinal)
        {
            Board board = GetBoard(email, boardName);
            if (board == null)
                return new Response("The board '" + boardName + "' does not exist", true);
            Column column = board.GetColumn(columnOrdinal);
            if (column == null)
                return new Response("Invalid column", true);
            return new Response(column.name);
        }

        internal Response GetColumn(string email, string boardName, int columnOrdinal)
        {
            Board board = GetBoard(email, boardName);
            if (board == null)
                return new Response("The board '" + boardName + "' does not exist", true);
            Column column = board.GetColumn(columnOrdinal);
            if (column == null)
                return new Response("Invalid column", true);
            return new Response(column.GetTasksList());
        }

        
        public Response AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {

            Board board = GetBoard(email, boardName);
            if (board == null)
                return new Response("The board '" + boardName + "' does not exist", true);
            Response r = board.AddTask(title, description, dueDate);
            return r;

        }

        /*public Response<string> RemoveTask(string email, string boardName, string Title)
        {
            Board board = GetBoard(email, boardName);
            if (board == null)
                return new Response<string>("The board \"" + boardName + "\" does not exist", true);
            return board.RemoveTask(Title);
        }*/


        public Response AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            Board board = GetBoard(email, boardName);
            if (board == null)
                return new Response("The board '" + boardName + "' does not exist", true);
            return board.AdvanceTask(columnOrdinal, taskId);
        }

        public Response InProgressTasks(string email)
        {
            List<Board> boardList = boards[email];
/*            if (boardList.Count == 0)
                return new Response("This user has no boards");*/
            List<Task> tasks = new List<Task>();
            foreach (Board b in boardList) {
                tasks.AddRange(b.getInProgressTasks());
            }
            return new Response(tasks);
        }

        public Task GetTask(string email, string boardName, int taskId)
        {
            return GetBoard(email, boardName).GetTask(taskId);
        }

        public Task GetTaskInColumn(string email, string boardName, int columnOrdinal, int taskId)
        {
            return GetBoard(email, boardName).GetTask(columnOrdinal, taskId);
        }

    }
}

