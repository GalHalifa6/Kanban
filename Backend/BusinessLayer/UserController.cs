using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class UserController

    {
        private Dictionary<string, User> users { get; set; }

        public UserController()
        {
            UserController userController = new UserController();
        }

        public Response<bool> createUser(string email, string password)
        {
            if (!users.ContainsKey(email))
            {
                User user = new User(email, password);
                users.Add(email, user);
                Response<bool> response = new Response<bool>(true);
                return response;
            }
            else
            {
                Response<bool> response = new Response<bool>("This email is already taken", true);
                return response;
            }
        }

        internal bool isLoggedIn(string email)
        {
            if (exists(email))
                return users[email].isLoggedIn;
            return false;
        }

        public Response<bool> DeleteUser(string email, string password)
        {
            if (GetUser(email) != null)
            {
                if (GetUser(email).Password == password)
                {
                    if (GetUser(email).isLoggedIn == true)
                    {
                        users.Remove(email);
                        return new Response<bool>(true);

                    }
                }
            }
            return new Response<bool>("The user can not be deleted", true);

        }


        public User GetUser(string email)
        {
            if (exists(email))
            {
                return users[email];
            }
            else
            {
                return null;
            }

        }
        public bool exists(string email)
        {
            if (users.ContainsKey(email))
                return true;
            else
                return false;
        }

        public Response<bool> login(string email, string password)
        {
            if (GetUser(email) != null)
            {
                if (GetUser(email).Password == password)
                {
                    GetUser(email).logIn();
                    return new Response<bool>(true);
                }
                else
                {
                    return new Response<bool>("The user logged in unsuccessfully", true);
                }
            }
            else
                return new Response<bool>("The user logged in unsuccessfully", true);
        }

        public Response<bool> LogOut(string email)
        {
            if (GetUser(email) != null)
            {
                if (GetUser(email).isLoggedIn == true)
                {
                    GetUser(email).logOut();
                    return new Response<bool>(true);
                }
                return new Response<bool>("The user logged out unsuccesssfully", true);
            }

            return new Response<bool>("The user logged out unsuccesssfully", true);
        }

        public Response<bool> changePassword(string email, string oldPassword, string newPassword)
        {
            if (GetUser(email) != null)
            {
                if (GetUser(email).isLoggedIn == true)
                {
                    if (users[email].Password == oldPassword)
                    {
                        GetUser(email).setPassword(newPassword);
                        return new Response<bool>(true);
                    }
                }
            }
            return new Response<bool>("Password changed unsuccessfully", true);
        }

    }
}
