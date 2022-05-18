using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class UserController
    {
        log4net.ILog logger = Utility.Logger.GetLogger();
        private Dictionary<string, User> users { get; set; }

        public UserController()
        {
            users = new Dictionary<string, User>();
            //UserController userController = new UserController();
        }

        public bool IsLoggedIn(string email)
        {
            if (!users.ContainsKey(email))
            {
                return false;
            }
            User user = users[email];
            if (user.isLoggedIn)
            {
                return true;
            }
            return false;

        }
        public Response<bool> createUser(string email, string password)
        {
            if (!users.ContainsKey(email))
            {
                User user = new User(email, password);
                users.Add(email, user);
                logger.Info("User: " + email + ", registered successfully");
                return new Response<bool>(true);
            }
            else
            {
                logger.Warn("Failed to create user " + email + ", because a user with that name already exists.");
                return new Response<bool>("This email is already taken", true);
            }
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

        public Response<bool> DeleteUser(string email, string password)
        {
            if (GetUser(email) != null)
            {
                if (GetUser(email).Password == password)
                {
                    if (GetUser(email).isLoggedIn == true)
                    {
                        users.Remove(email);
                        logger.Info("User: " + email + ", deleted successfully");
                        return new Response<bool>(true);
                    }
                    logger.Warn("Failed to delete user " + email + ", because the user is not connected");
                    return new Response<bool>("the user "+ email + " is not connected, The user can not be deleted", true);
                }
                logger.Warn("Failed to delete user " + email + ", because There is no match between email and password");
                return new Response<bool>("There is no match between email and password", true);
            }
            logger.Warn("Failed to delete user " + email + ", because a user with that name is not exists");
            return new Response<bool>("The user "+ email + " does not exist", true);
        }

        public Response<bool> login(string email, string password)
        {
            if (GetUser(email) != null)
            {
                if (GetUser(email).Password == password)
                {
                    GetUser(email).logIn();
                    logger.Info("The user " + email + " logged in successfully");
                    return new Response<bool>(true);                  
                }
                else
                {
                    logger.Warn("Faild to login the user " + email + ", because there is no match between the email and password");
                    return new Response<bool>("Incorrect password", true);                  
                }
            }
            else
                logger.Warn("Failed to delete user " + email + ", because a user with that name is not exists");
                return new Response<bool>("The user "+ email + " does not exist", true);
        }

        public Response<bool> LogOut(string email)
        {
            if (GetUser(email) != null)
            {
                if (GetUser(email).isLoggedIn == true)
                {
                    GetUser(email).logOut();
                    logger.Info("The user " + email + " logged out successfully");
                    return new Response<bool>(true);                  
                }
                logger.Warn("Faild to logout the user " + email + ", because the user is not connected");
                return new Response<bool>("The user logged out unsuccesssfully", true);
            }
            logger.Warn("Failed to logout user " + email + ", because a user with that name is not exists");
            return new Response<bool>("The user " + email + " does not exist", true);
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
                        logger.Info("The password of the user " + email + " changed successfully");
                        return new Response<bool>(true);
                    }
                    logger.Warn("Faild to change password of the user " + email + ", because there is no match between the email and password");
                    return new Response<bool>("There is no match between the password and the user name", true);
                }
                logger.Warn("The user's password can not be changed, because the user " + email + " is not connected");
                return new Response<bool>("This user is not connected, password can't be changed", true);
            }
            logger.Warn("Failed to change the user's password at " + email + ", because a user with that email is not exists");
            return new Response<bool>("The user " + email + " does not exist", true);
        }
    }
}