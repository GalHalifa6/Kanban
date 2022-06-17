using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class User
    {
        public bool isLoggedIn { get; set; }
        private string email { get; set; }
        public string password { get; private set; }

        private HashSet<Board> MyBoards;
        // MyBoards is a set with string key
        private HashSet<int> CommonBoards;
        // CommonBoards is a set with int key
        private UserDTO dto;

        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
            MyBoards = new HashSet<Board>();
            CommonBoards = new HashSet<int>();
            this.isLoggedIn = false;
            dto = new UserDTO(email, password);
        }

        public void setEmail(string email)
        {
            this.email = email;
        }

        public void logIn()
        {
            this.isLoggedIn = true;
        }

        public void logOut()
        {
            this.isLoggedIn = false;
        }

        public void setPassword(string password)
        {
            this.password = password;
        }

        public Response RegisterUser(string email, string password)
        {
            return dto.RegisterUser(email, password);
        }

        internal bool AddBoard(Board board)
        {
            if (MyBoards.Contains(board))
            {
                return false;
            }
            MyBoards.Add(board);
            return true;
        }

        internal bool RemoveBoard(string name)
        {
            foreach(Board board in MyBoards)
            {
                if(board.name == name)
                {
                    MyBoards.Remove(board);
                    return true;
                }
            }
            return false;
        }

        internal void JoinBoard(string email, int boardID)
        {
            CommonBoards.Add(boardID);
        }

        internal void LeaveBoard(int boardID)
        {
            CommonBoards.Remove(boardID);
        }
        
        internal Response GetUserBoards()
        {
            List<int> boards = new List<int>();
            foreach(Board board in MyBoards)
            {
                boards.Add(board.id);
            }
            Response r = new Response(boards);
            return r;
        }
        
        public Board renounceOwnership(string boardName) {
            foreach(Board board in MyBoards)
            {
                if(board.name == boardName)
                {

                    MyBoards.Remove(board);
                    CommonBoards.Add(board.id);
                    return board;
                }
            }
            return null;
        }
        public Response takeOwnership(Board board, string currentUser)  {
            Response r = board.ChangeOwner(currentUser, this.email);
            if (!MyBoards.Contains(board))
            {
                if (r.ErrorOccured() ==  false)
                {
                    MyBoards.Add(board);

                }             
            }
            return r;      
        }

        public bool CheckIfCanAddBoard(string boardName)
        {
            foreach(Board board in MyBoards)
            {
                if(board.name == boardName)
                {
                    return false;
                }
            }
            return true;
        }

        internal bool JoinBoard(Board b)
        {
            throw new NotImplementedException();
        }
    }
} 