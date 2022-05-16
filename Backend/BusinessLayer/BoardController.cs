using System;
using System.Collections.Generic;

namespace IntroSE.Kanban.Backend.BusinessLayer
{

    public class BoardController
    {
        Dictionary<string, List<Board>> boards;
        log4net.ILog logger = Utility.Logger.GetLogger();

        public BoardController()
        {
            boards = new Dictionary<string, List<Board>>();
        }

        private Board GetBoard(string email, string boardName)
        {
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

        public Response<bool> AddBoard(string email, string name)
        {
            if (!boards.ContainsKey(email))
            {
                boards[email] = new List<Board>();
            }
            if (GetBoard(email, name) != null)
            {
                logger.Warn("Failed to create board " + name + ", because a board with that name already exists.");
                return new Response<bool>("Board with this name already exists", true);
            }
            boards[email].Add(new Board(name));
            logger.Info("board " + name + " created for user " + email);
            return new Response<bool>(true);
        }

        public Response<bool> RemoveBoard(string email, string boardName)
        {
            Board board = GetBoard(email, boardName);
            if (board != null)
            {
                boards[email].Remove(board);
                logger.Info("board " + boardName + " removed from user " + email);
                return new Response<bool>(true);
            }
            logger.Warn("Failed to remove board " + boardName + ", because a board with that name doesn't exists.");
            return new Response<bool>("The board \"" + boardName + "\" does not exist", true);
        }

        public Response<bool> LimitColumnTasks(string email, string boardName, int columnNumber, int newLimit)
        {
            Board board = GetBoard(email, boardName);
            if (board == null)
            {
                logger.Warn("Failed to limit tasks in board " + boardName + ", because a board with that name doesn't exists.");
                return new Response<bool>("The board \"" + boardName + "\" does not exist", true);
            }
            return board.LimitColumnTasks(columnNumber, newLimit);
        }

        public Response<int> GetColumnLimit(string email, string boardName, int columnNumber)
        {
            Board board = GetBoard(email, boardName);
            if (board == null)
            {
                return new Response<int>("The board \"" + boardName + "\" does not exist",true);
            }
            return board.GetColumnLimit(boardName, columnNumber);
        }

        public Response<string> GetColumn(string email, string boardName, int columnOrdinal)
        {
            Board board = GetBoard(email, boardName);
            if (board == null)
                return new Response<string>("The board \"" + boardName + "\" does not exist", true);
            Column column = board.GetColumn(columnOrdinal);
            if (column == null)
                return new Response<string>("Invalid column",true);
            return new Response<string>(column.ToString());
        }
    }
}