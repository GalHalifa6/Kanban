using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Newtonsoft.Json;

namespace IntroSE.Kanban.Backend.BusinessLayer
{

    public class BoardController
    {
        public Dictionary<string, HashSet<Board>> boards;
        //HashSet<Board> boards;
        log4net.ILog logger = Utility.Logger.GetLogger();
        public int nextBoardID { get; private set; }

        public BoardController()
        {
            //DBConnector.GetInstance(); // to initialize db
            boards = new Dictionary<string, HashSet<Board>>();
            nextBoardID = 0;
        }

        /// <summary>
        /// Get a specific board
        /// </summary>
        /// <param name="email"> user whose board will be returned</param>
        /// <param name="boardID">board to be returned</param>
        /// <returns>the board that was looked for, null if no such board exists</returns>
        public Board GetBoard(string email, string boardName)
        {
            HashSet<Board> userBoards = boards[email];
            foreach (Board b in userBoards)
            {
                if (b.name == boardName)
                {
                    return b;
                }
            }
            return null;
        }


        public Board GetBoard(int boardID)
        {
            foreach (HashSet<Board> set in boards.Values)
            {
                foreach (Board b in set)
                {
                    if (b.id == boardID)
                    {
                        return b;
                    }
                }
            }
            return null;
        }


        public Response AddBoard(string email, string name)
        {
/*            if (GetBoard(email, name) != null)
            {
                logger.Warn("Failed to create board " + name + ", because a board with that name already exists.");
                return new Response("Board with this name already exists", true);
            }*/
            Board b = new Board(name, nextBoardID, email);
            Response r = b.AddBoard();
            if (r.ErrorOccured())
                return r;
            boards[email].Add(b);

            logger.Info("board " + name + " created for user " + email);
            nextBoardID++;
            return new Response(b);
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
                Response r = board.RemoveBoard(email);
                if (r.ErrorOccured())
                    return r; // so that if the db deletion failed, nothing would change
                boards[email].Remove(board);
                logger.Info("board " + boardName + " removed from user " + email);
                return r;
            }
            logger.Warn("Failed to remove board " + boardName + ", because a board with that name doesn't exists.");
            return new Response("The board '" + boardName + "' does not exist", true);
        }



        public void LimitColumnTasks(string email, string boardName, int columnNumber, int newLimit)
        {
            Board board = GetBoard(email, boardName);
            if (board == null)
            {
                logger.Warn("Failed to limit tasks in board " + boardName + ", because a board with that name doesn't exists.");
                throw new Exception("The board '" + boardName + "' does not exist");
            }
            board.LimitColumnTasks(columnNumber, newLimit);
        }

        public int GetColumnLimit(string email, string boardName, int columnNumber)
        {
            Board board = GetBoard(email, boardName);
            if (board == null)
            {
                throw new Exception("The board '" + boardName + "' does not exist");
            }
            return board.GetColumnLimit(boardName, columnNumber);
        }

        public string GetColumnName(string email, string boardName, int columnOrdinal)
        {
            Board board = GetBoard(email, boardName);
            if (board == null)
                throw new Exception("The board '" + boardName + "' does not exist");
            Column column = board.GetColumn(columnOrdinal);
            if (column == null)
                throw new Exception("Invalid column");
            return column.name;
        }

        internal List<Task> GetColumn(string email, string boardName, int columnOrdinal)
        {
            Board board = GetBoard(email, boardName);
            if (board == null)
                throw new Exception("The board '" + boardName + "' does not exist");
            Column column = board.GetColumn(columnOrdinal);
            if (column == null)
                throw new Exception("Invalid column");
            return column.GetTasksList();
        }

        
        public void AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {

            Board board = GetBoard(email, boardName);
            if (board == null)
                throw new Exception("The board '" + boardName + "' does not exist");
            board.AddTask(email, title, description, dueDate);
        }

        /*public Response<string> RemoveTask(string email, string boardID, string Title)
        {
            Board board = GetBoard(email, boardID);
            if (board == null)
                return new Response<string>("The board \"" + boardID + "\" does not exist", true);
            return board.RemoveTask(Title);
        }*/


        public void AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            Board board = GetBoard(email, boardName);
            if (board == null)
                throw new Exception("The board '" + boardName + "' does not exist");
            board.AdvanceTask(email, columnOrdinal, taskId);
        }

        public List<Task> InProgressTasks(string email)
        {
            HashSet<Board> boardList = boards[email];
            List<Task> tasks = new List<Task>();
            foreach (Board b in boardList) {
                tasks.AddRange(b.getInProgressTasks());
            }
            return tasks;
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

        internal void AssignTask(string assigner, string boardName, int columnOrdinal, int taskID, string assignee)
        {
            Board b = GetBoard(assigner, boardName);
            if (b == null)
            {
                logger.Warn(assigner + " attempted to access a board that doesn't exist");
                throw new Exception("The board '" + boardName + "' does not exist");
            }
            b.AssignTask(assigner, columnOrdinal, taskID, assignee);
        }

        public Response LoadData()
        {
            HashSet<BoardDTO> res = new BoardControllerDTO().LoadData();
            return null;
        }

        internal Response JoinBoard(string email, int boardID)
        {
            Board b = GetBoard(boardID);
            if (b == null)
            {
                logger.Warn(email + " attempted to join a board that doesn't exist");
                return new Response("A board with id: " + boardID + " does not exist", true);
            }
            Response r = b.AddUser(email);
            if (!r.ErrorOccured())
                boards[email].Add(b);
            return r;
        }

        internal Response LeaveBoard(string email, int boardID)
        {
            Board b = GetBoard(boardID);
            if (b == null)
            {
                logger.Warn(email + " attempted to leave a board that doesn't exist");
                return new Response("The board '" + boardID + "' does not exist", true);
            }
            Response r = b.RemoveUser(email);
            if (!r.ErrorOccured())
                boards[email].Remove(b);
            return r;
        }

        internal Response TransferOwnership(string currentOwnerEmail, string newOwnerEmail, string boardName)
        {
            Board b = GetBoard(currentOwnerEmail, boardName);
            if (b == null)
            {
                logger.Warn(currentOwnerEmail + " attempted to leave a board that doesn't exist");
                return new Response("The board '" + boardName + "' does not exist", true);
            }
            return b.ChangeOwner(currentOwnerEmail, newOwnerEmail);
        }

        public void UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string newTitle)
        {
            if (GetTaskInColumn(email, boardName, columnOrdinal, taskId) == null)
            {
                throw new Exception("The specified task does not exist.");
            }
            Board board = GetBoard(email, boardName);
            if (board == null)
            {
                throw new Exception("The specified board does not exist.");
            }
            board.UpdateTaskTitle(columnOrdinal, taskId, newTitle);
        }

        public void UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string newDesc)
        {
            if (GetTaskInColumn(email, boardName, columnOrdinal, taskId) == null)
            {
                throw new Exception("The specified task does not exist.");
            }
            Board board = GetBoard(email, boardName);
            if (board == null)
            {
                throw new Exception("The specified board does not exist.");
            }
            board.UpdateTaskDescription(columnOrdinal, taskId, newDesc);
        }

        public void UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime newDueDate)
        {
            if (GetTaskInColumn(email, boardName, columnOrdinal, taskId) == null)
            {
                throw new Exception("The specified task does not exist.");
            }
            Board board = GetBoard(email, boardName);
            if (board == null)
            {
                throw new Exception("The specified board does not exist.");
            }
            board.UpdateTaskDueDate(columnOrdinal, taskId, newDueDate);
        }
    }
}

