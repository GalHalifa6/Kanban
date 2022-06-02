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

        private HashSet<string> MyBoards;
        // MyBoards is a set with string key
        private HashSet<int> CommonBoards;
        // CommonBoards is a set with int key
        private UserDTO dto;

        public User(string email, string password)
        {
            this.email = email;  
            this.password = password;    
            MyBoards = new HashSet<string>();
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

        internal bool AddBoard(string name)
        {
            if (MyBoards.Contains(name))
            {
                return false;
            }
            MyBoards.Add(name);
            return true;
        }

        internal bool RemoveBoard(string name)
        {
            if (MyBoards.Contains(name))
            {
                MyBoards.Remove(name);
                return true;
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

        /*
        internal List<string> GetUserBoard()
        {
            if (MyBoards.Count == 0)
            {
                return null;
            }
            List<string> boards = new List<string>();


        }
        */

        /*
       internal void TransferOwnership(string boardName)
       {
           MyBoards.Remove(boardName);
           CommonBoards.Add(boardName);
       }
        */
    }
}