using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
        public class UserService
    {
        UserController uc;
        string email; // email of the user currently logged in. null if no user is logged in

        public UserService()
        {

        }

        public string register(string email, string password) {
            throw new NotImplementedException();
        }

        public string login(string email, string password) { 
            throw new NotImplementedException();
        }

        public string logout() {
            throw new NotImplementedException();
        }

        public string changePassword(string oldPassword, string newPassword) {
            throw new NotImplementedException();
        }

    }
}
