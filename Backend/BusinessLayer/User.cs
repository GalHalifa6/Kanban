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
        private HashSet<Board> CommonBoards;
        // CommonBoards is a set with int key
        private UserDTO dto;

        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
            MyBoards = new HashSet<Board>();
            CommonBoards = new HashSet<Board>();
            this.isLoggedIn = false;
            dto = new UserDTO(email, password);
        }

        /// <summary>
        /// set for the email field 
        /// </summary>
        /// <param name="email"> the new email to set </param>
        public void setEmail(string email)
        {
            this.email = email;
        }

        //switch mode of the field
        public void logIn()
        {
            this.isLoggedIn = true;
        }

        //switch mode of the field
        public void logOut()
        {
            this.isLoggedIn = false;
        }

        /// <summary>
        /// set new password 
        /// </summary>
        /// <param name="password"> new password to be set</param>
        public void setPassword(string password)
        {
            this.password = password;
        }

        /// <summary>
        /// Registers a user to the system
        /// </summary>
        /// <param name="email"> email to be registered </param>
        /// <param name="password"> password of the user </param>
        public void RegisterUser(string email, string password)
        {
            dto.RegisterUser(email, password);
        }

        /// <summary>
        /// Add board to a users board list- the list of boards that the user is the owner of them
        /// </summary>
        /// <param name="email"> Email of a user </param>
        /// <param name="board"> Board the the user will be the owner </param>
        /// <returns> Bool- if the procedure succeed or not </returns>
        internal bool AddBoard(Board board)
        {
            if (MyBoards.Contains(board))
            {
                return false;
            }
            MyBoards.Add(board);
            return true;
        }

        /// <summary>
        /// remove board- by name from the user's MyBoards list
        /// </summary>
        /// <param name="email"> Email of the user </param>
        /// <param name="name"> Name the candidate board to be remove</param>
        /// <returns> Bool statment of the procedure </returns>
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

        /*
        internal void JoinBoard(string email, int boardID)
        {
            CommonBoards.Add(boardID);
        }
        */

        /// <summary>
        /// leave board that the user is taking apart, not the owner of them
        /// </summary>
        /// <param name="email"> Email of the user </param>
        /// <param name="boardID"> ID of the board that the user should leave </param>
        internal void LeaveBoard(int boardID)
        {
            foreach(Board board in CommonBoards)
            {
                if(board.id == boardID)
                {
                    CommonBoards.Remove(board);
                }
            }
        }

        /// <summary>
        /// get boards of a user by the email
        /// </summary>
        /// <param name="email"> Email of the user </param>
        /// <returns> return list of boards- by their id </returns>
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
        
        /// <summary>
        /// renounce new ownership of board, move the board from the owner list boards to the common list boards
        /// </summary>
        /// <param name="boardName"> name of the candidate board</param>
        /// <returns> Board </returns>
        public Board renounceOwnership(string boardName) {
            foreach(Board board in MyBoards)
            {
                if(board.name == boardName)
                {

                    MyBoards.Remove(board);
                    CommonBoards.Add(board);
                    return board;
                }
            }
            return null;
        }

        /// <summary>
        /// user take ownership of a board
        /// </summary>
        /// <param name="board"> board that sent from renounceOwnership </param>
        /// <param name="currentUser"> New owner of the board</param>
        public Response takeOwnership(Board board, string currentUser)  {
            /*Response r = board.ChangeOwner(currentUser, this.email);
            if (!MyBoards.Contains(board))
            {
                if (r.ErrorOccured() ==  false)
                {
                    MyBoards.Add(board);

                }             
            }
            return r; */
            return null;
        }

        /// <summary>
        /// check if the user can take the ownership
        /// </summary>
        /// <param name="boardName"> name of the candidate board </param>
        /// <returns> Bool statment of the procedure </returns>
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
        /// <summary>
        /// user will take apart in a board, he will not the owner 
        /// </summary>
        /// <param name="b"> Board that the user will take apart in</param>
        /// <returns> Bool statement of the procedure </returns>
        internal bool JoinBoard(Board b)
        {
            foreach(Board board in CommonBoards)
            {
                if(b.name == board.name)
                {
                    return false;
                }
            }
            CommonBoards.Add(b);
            return true;
        }
    }
} 