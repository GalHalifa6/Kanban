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
        }

        public bool IsLoggedIn(string email)
        {
            email = email.ToLower();
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

        public Response createUser(string email, string password)
        {
            if (!users.ContainsKey(email))
            {
                User user = new User(email, password);
                Response r = user.RegisterUser(email, password);
                if (r.ErrorOccured())
                    return r;
                users.Add(email, user);
                logger.Info("User: " + email + ", registered successfully");
                return r;
            }
            else
            {
                logger.Warn("Failed to create user " + email + ", because a user with that name already exists.");
                return new Response("This email is already taken", true);
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
            email = email.ToLower();
            if (users.ContainsKey(email))
                return true;
            else
                return false;
        }

        public Response DeleteUser(string email, string password)
        {
            if (GetUser(email) != null)
            {
                if (GetUser(email).password == password)
                {
                    if (GetUser(email).isLoggedIn == true)
                    {
                        users.Remove(email);
                        logger.Info("User: " + email + ", deleted successfully");
                        return new Response(true);
                    }
                    logger.Warn("Failed to delete user " + email + ", because the user is not connected");
                    return new Response("the user " + email + " is not connected, The user can not be deleted", true);
                }
                logger.Warn("Failed to delete user " + email + ", because There is no match between email and password");
                return new Response("There is no match between email and password", true);
            }
            logger.Warn("Failed to delete user " + email + ", because a user with that name is not exists");
            return new Response("The user " + email + " does not exist", true);
        }

        public Response login(string email, string password)
        {
            if (GetUser(email) != null)
            {
                if (GetUser(email).password == password)
                {
                    GetUser(email).logIn();
                    logger.Info("The user " + email + " logged in successfully");
                    return new Response(true);
                }
                else
                {
                    logger.Warn("Faild to login the user " + email + ", because there is no match between the email and password");
                    return new Response("Incorrect password", true);
                }
            }
            else
                logger.Warn("Failed to delete user " + email + ", because a user with that name is not exists");
            return new Response("The user " + email + " does not exist", true);
        }

        public Response LogOut(string email)
        {
            if (GetUser(email) != null)
            {
                if (GetUser(email).isLoggedIn == true)
                {
                    GetUser(email).logOut();
                    logger.Info("The user " + email + " logged out successfully");
                    return new Response(true);
                }
                logger.Warn("Faild to logout the user " + email + ", because the user is not connected");
                return new Response("The user logged out unsuccesssfully", true);
            }
            logger.Warn("Failed to logout user " + email + ", because a user with that name is not exists");
            return new Response("The user " + email + " does not exist", true);
        }

        public Response changePassword(string email, string oldPassword, string newPassword)
        {
            if (GetUser(email) != null)
            {
                if (GetUser(email).isLoggedIn == true)
                {
                    if (users[email].password == oldPassword)
                    {
                        GetUser(email).setPassword(newPassword);
                        logger.Info("The password of the user " + email + " changed successfully");
                        return new Response(true);
                    }
                    logger.Warn("Faild to change password of the user " + email + ", because there is no match between the email and password");
                    return new Response("There is no match between the password and the user name", true);
                }
                logger.Warn("The user's password can not be changed, because the user " + email + " is not connected");
                return new Response("This user is not connected, password can't be changed", true);
            }
            logger.Warn("Failed to change the user's password at " + email + ", because a user with that email is not exists");
            return new Response("The user " + email + " does not exist", true);
        }

        internal bool RemoveBoard(string email, string name)
        {
            User user = users[email];
            return user.RemoveBoard(name);
        }

        public bool AddBoard(string email, Board board)
        {
            User user = users[email];
            return user.AddBoard(board);
        }

        internal void JoinBoard(string email, int boardID)
        {
            User user = users[email];
            user.JoinBoard(email, boardID);
        }

        internal void LeaveBoard(string email, int boardID)
        {
            User user = users[email];
            user.LeaveBoard(boardID);
        }

        
        internal Response GetUserBoards(string email)
        {
            User user = users[email];
            return user.GetUserBoards();
        }
        
        
        internal Response TransferOwnership(string currentOwnerEmail, string newOwnerEmail, string boardName)
        {
            if(GetUser(newOwnerEmail) != null)
            {
                User currentOwner = users[currentOwnerEmail];
                User newOwner = users[newOwnerEmail];
                if (newOwner.CheckIfCanAddBoard(boardName)){
                    if (currentOwner.renounceOwnership(boardName) != null)
                    {
                        Board board = currentOwner.renounceOwnership(boardName);
                        newOwner.takeOwnership(board, currentOwnerEmail);
                        return new Response($"{newOwnerEmail} is the new owner of '{boardName}' instead of {currentOwnerEmail} ");
                    }
                    return new Response($"{currentOwnerEmail} has no board called '{boardName}' ");
                }
                return new Response($"{newOwnerEmail} already has a board called {boardName}");
            }
            return new Response($"{newOwnerEmail} is not registered, can not transfer the ownership of '{boardName}");           
            
        }

        internal bool JoinBoard(string email, Board b)
        {
            User user = users[email];
            return user.JoinBoard(b);
        }
    }
}