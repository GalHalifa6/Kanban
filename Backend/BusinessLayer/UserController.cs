using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    internal class UserController
    {
        private Dictionary<string, User> users { get; set; }

        public UserController()
        {
            throw new NotImplementedException();
        }

        public void createUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void deleteUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        public User getUser(string email)
        {
            throw new NotImplementedException();
        }
        public Boolean exists(string email)
        {
            throw new NotImplementedException();
        }

    }
}
