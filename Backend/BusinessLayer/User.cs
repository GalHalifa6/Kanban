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
        private string Email { get; set; }
        public string Password { get; private set; }

        private List<Board> Boards { get; set; }

        public User(string email, string password)
        {
            User user = new User(email, password);
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
            this.Password = password;
        }
    }
}