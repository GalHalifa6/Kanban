using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace IntroSE.Kanban.Backend.BusinessLayer
{

    public class BoardController
    {
        public Dictionary<string, HashSet<Board>> boards;
        log4net.ILog logger = Utility.Logger.GetLogger();
        public int nextBoardID { get; private set; }
        //private int nextTaskID { get; set; }


        public BoardController()
        {
            boards = new Dictionary<string, HashSet<Board>>();
            nextBoardID = 0;
            //LoadData();

        }

        /// <summary>
        /// Get a specific board
        /// </summary>
        /// <param name="email"> user whose board will be returned</param>
        /// <param name="boardID">board to be returned</param>
        /// <returns>the board that was looked for, null if no such board exists</returns>
        public Board GetBoard(string email, string boardID)
        {
            if (!boards.ContainsKey(email))
            {
                boards.Add(email, new HashSet<Board>());
                return null;
            }
            HashSet<Board> userBoards = boards[email];
            foreach (Board b in userBoards)
            {
                if (b.name == boardID)
                {
                    return b;
                }
            }
            return null;
        }


        public Response AddBoard(string email, string name)
        {
            if (!boards.ContainsKey(email))
            {
                boards[email] = new HashSet<Board>();
            }
            if (GetBoard(email, name) != null)
            {
                logger.Warn("Failed to create board " + name + ", because a board with that name already exists.");
                return new Response("Board with this name already exists", true);
            }
            Board b = new Board(name, nextBoardID, email);
            Response r = b.AddBoard();
            if (r.ErrorOccured())
                return r;
            boards[email].Add(b);

            logger.Info("board " + name + " created for user " + email);
            nextBoardID++;
            return r;
        }

        /// <summary>
        /// Called when a successful registeration occurs. Adds a new (key,value) pair to the user boards dictionary
        /// </summary>
        /// <param name="email">newly registered user</param>
        internal void Register(string email)
        {
            boards.Add(email, new HashSet<Board>());
        }

        public Response RemoveBoard(string email, string boardName)
        {
            Board board = GetBoard(email, boardName);
            if (board != null)
            {
                Response r = board.RemoveBoard();
                if (r.ErrorOccured())
                    return r; // so that if the db deletion failed, nothing would change
                boards[email].Remove(board);
                logger.Info("board " + boardName + " removed from user " + email);
                return r;
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
            Response r = board.AddTask(email, title, description, dueDate);
            return r;

        }

        /*public Response<string> RemoveTask(string email, string boardID, string Title)
        {
            Board board = GetBoard(email, boardID);
            if (board == null)
                return new Response<string>("The board \"" + boardID + "\" does not exist", true);
            return board.RemoveTask(Title);
        }*/


        public Response AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            Board board = GetBoard(email, boardName);
            if (board == null)
                return new Response("The board '" + boardName + "' does not exist", true);
            return board.AdvanceTask(email, columnOrdinal, taskId);
        }

        public Response InProgressTasks(string email)
        {
            HashSet<Board> boardList = boards[email];
/*            if (boardList.Count == 0)
                return new Response("This user has no boards");*/
            List<Task> tasks = new List<Task>();
            foreach (Board b in boardList) {
                tasks.AddRange(b.getInProgressTasks());
            }
            return new Response(tasks);
        }

/*        public Task GetTask(string email, string boardID, int taskId)
        {
            return GetBoard(email, boardID).GetTask(taskId);
        }*/

        public Task GetTaskInColumn(string email, string boardName, int columnOrdinal, int taskId)
        {
            Board b = GetBoard(email, boardName);
            if(b == null)
                return null;
            return b.GetTask(columnOrdinal, taskId);
        }

        /*public Response AddUserToBoard(string email, int boardID)
        {
            
        }*/

        /*public Response RemoveUserFromBoard(string email, int boardID)
        {
            
        }*/

        public void LoadData()
        {
            SQLiteDataReader res = new BoardControllerDTO().LoadData();
        }



    }
}

