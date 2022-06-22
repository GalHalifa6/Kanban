using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;
using Newtonsoft.Json;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class UserService
    {
        private string currentEmail;
        public string CurrentEmail
        {
            get => currentEmail;
            set => currentEmail = value;
        }

        private UserController uc;
        public UserController Uc { get => uc; }

        public UserService()
        {
            uc = new UserController();
        }

        /// <summary>
        /// Registers a user to the system
        /// </summary>
        /// <param name="email"> email to be registered </param>
        /// <param name="password"> password of the user </param>
        /// <returns> json of the procedure </returns>
        public string Register(string email, string password)
        {
            if (IsValidEmail(email) == false)
            {
                Response r = new Response("Invalid email", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            if (IsValidPassword(password) == false)
            {
                Response r = new Response("Invalid password", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            try {
                uc.createUser(email, password);
                return "{}";
            }
            catch(Exception e)
            {
                return JsonConvert.SerializeObject(new Response(e.Message, true), Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
        }

        /// <summary>
        /// Allows a user to log in to the system
        /// </summary>
        /// <param name="email">Email of the user that is trying to log in</param>
        /// <param name="password">Password of the user trying to log in</param>
        /// <returns> json of the procedure </returns>
        public string Login(string email, string password)
        {
            /*
            if (currentEmail != null)
            {
                Response r = new Response("Cannot log in while another user is logged in", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            }
            */
            if (IsValidEmail(email) == false)
            {
                Response r = new Response("Invalid email", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            if (IsValidPassword(password) == false)
            {
                Response r = new Response("Invalid password", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            try
            {
                uc.login(email, password);
                currentEmail = email;
                return "{}";
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new Response(e.Message, true), Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }           
        }

        /// <summary>
        /// Allows a logged in user to log out
        /// </summary>
        /// <param name="email"> Email of the user trying to log out</param>
        /// <returns> json of the procedure </returns>
        public string Logout(string email)
        {
            if (IsValidEmail(email) == false)
            {
                Response r = new Response("Invalid email", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            try
            {
                uc.LogOut(email);
                currentEmail = null;
                return "{}";
            }
            catch(Exception e)
            {
                return JsonConvert.SerializeObject(e.Message, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
        }


        /// <summary>
        /// Deletes an existing user from the system
        /// </summary>
        /// <param name="email"> Email of the user trying to deleted </param>
        /// <param name="password"> Password of the user trying to deleted </param>
        /// <returns> json of the procedure </returns>
        public string DeleteUser(string email, string password)
        {
            if (IsValidEmail(email) == false)
            {
                Response r = new Response("Invalid email", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            if (IsValidPassword(password) == false)
            {
                Response r = new Response("Invalid password", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            Response response = uc.DeleteUser(email, password);
            return JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        /// <summary>
        /// remove board- by name from the user's MyBoards list
        /// </summary>
        /// <param name="email"> Email of the user </param>
        /// <param name="name"> Name the candidate board to be remove</param>
        /// <returns> Bool statment of the procedure </returns>
        internal bool RemoveBoard(string email, string name)
        {
            return uc.RemoveBoard(email, name);
        }


        /// <summary>
        /// Allows a user to change password
        /// </summary>
        /// <param name="email"> Email of the user </param>
        /// <param name="oldPassword"> Old password of the user</param>
        /// <param name="newPassword"> New password of the user </param>
        /// <returns> json string </returns>

        public string ChangePassword(string email, string oldPassword, string newPassword)
        {
            if (IsValidPassword(oldPassword) == false)
            {
                Response r = new Response("Old password is invalid", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            if (IsValidPassword(newPassword) == false)
            {
                Response r = new Response("New password is invalid", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            if (IsValidEmail(email) == false)
            {
                Response r = new Response("Invalid email", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            try
            {
                uc.changePassword(email, oldPassword, newPassword);
                return "{}";
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(e.Message, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
        }

        internal string LoadData()
        {
            try
            {
                uc.LoadData();
                return "{}";
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(e.Message, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            }
        }

        internal string DeleteData()
        {
            try
            {
                uc.DeleteData();
                return "{}";
            }
            catch(Exception e)
            {
                return JsonConvert.SerializeObject(e.Message, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            }
        }

        /// <summary>
        /// return user controller 
        /// </summary>
        /// <returns> user controller </returns>
        internal UserController GetUc()
        {
            return uc;
        }

        /// <summary>
        /// get boards of a user by the email
        /// </summary>
        /// <param name="email"> Email of the user </param>
        /// <returns> return list of boards- by their id </returns>
        internal string GetUserBoards(string email)
        {
            Response response = uc.GetUserBoards(email);
            return JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        /// <summary>
        /// leave board that the user is taking apart, not the owner of them
        /// </summary>
        /// <param name="email"> Email of the user </param>
        /// <param name="boardID"> ID of the board that the user should leave </param>
        internal void LeaveBoard(string email, int boardID)
        {
            uc.LeaveBoard(email, boardID);
        }

        /*
        internal void JoinBoard(string email, int boardID)
        {
            uc.JoinBoard(email, boardID);
        }
        */

        /// <summary>
        /// validataion function for passwords, uses helper functions for smaller validations
        /// </summary>
        /// <param name="pass"> Password to validate</param>
        /// <returns>boolean indicating the validity of the password</returns>
        public bool IsValidPassword(string pass)
        {
            if (string.IsNullOrWhiteSpace(pass) || !(pass.Length >= 6 && pass.Length <= 20) || !(pass.Any(char.IsUpper)) || !(pass.Any(char.IsLower)) || !(pass.Any(char.IsDigit)))
            {
                return false;
            }
            if (!validPassword(pass))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Transfer the ownership of a board to another user
        /// </summary>
        /// <param name="currentOwnerEmail"> Email of the owner user</param>
        /// <param name="newOwnerEmail"> Email of the new owner- user </param>
        /// <param name="boardName"> name of the board we want to trasfer the ownership</param>
        /// <returns> string- the result of the transfer</returns>
        internal string TransferOwnership(string currentOwnerEmail, string newOwnerEmail, string boardName)
        {
            try
            {
                uc.TransferOwnership(currentOwnerEmail, newOwnerEmail, boardName);
                return "{}";
            }
            catch(Exception e)
            {
                return JsonConvert.SerializeObject(e.Message, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
        }
        
        /// <summary>
        /// check if the password is valid
        /// </summary>
        /// <param name="pass"> password of the user</param>
        /// <returns> return Bool- if the password is correct or not</returns>
        private bool validPassword(string pass)
        {
            bool atLeastOneUpper = false;
            bool atLeastOneLower = false;
            bool atLeastOneNumber = false;
            foreach (char c in pass.ToCharArray())
            {
                if (char.IsUpper(c))
                {
                    atLeastOneUpper = true;
                }
                if (char.IsLower(c))
                {
                    atLeastOneLower = true;
                }
                if (char.IsDigit(c))
                {
                    atLeastOneNumber = true;
                }
            }
            if (!atLeastOneUpper | !atLeastOneNumber | !atLeastOneLower)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// validates email address
        /// </summary>
        /// <param name="emailaddress">email to be verified</param>
        /// <returns>boolean indicating the validity of the email</returns>
        internal bool IsValidEmail(string emailaddress)
        {
            EmailAddressAttribute email = new EmailAddressAttribute();
            try
            {
                Regex rx = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                      @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                      @".)+))([a-zA-Z]{2,6}|[0-9]{1,3})(\]?)$");

                if (rx.IsMatch(emailaddress))
                {
                    Regex emailAttribute = new Regex(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$");
                    if (emailAttribute.IsMatch(emailaddress))
                    {
                        return test1(emailaddress) & test2(emailaddress) & test3(emailaddress);
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false; ;
            }
        }
        // test1, test2, test3 -helpers functions to validation of email and password //
        public bool test2(string email)
        {
            Regex regex = new Regex(@"^[\w!#$%&'+\-/=?\^_`{|}~]+(\.[\w!#$%&'+\-/=?\^_`{|}~]+)*@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            Match match = regex.Match(email);
            return match.Success;
        }
        public bool test3(string email)
        {
            char[] charArray = { 'א', 'ב', 'ג', 'ד', 'ה', 'ו', 'ז', 'ח', 'ט', 'י', 'כ', 'ל', 'מ', 'נ', 'ס', 'ע', 'פ', 'צ', 'ק', 'ר', 'ש', 'ת' };
            foreach (char c in email.ToCharArray())
            {
                if (charArray.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }

        public bool test1(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        /// <summary>
        /// check if the user is logged in or loggedout
        /// </summary>
        /// <param name="email"> Email of the user</param>
        /// <returns> Bool by the statment of the proprety </returns>
        public bool IsLoggedIn(string email)
        {
            email = email.ToLower();
            return uc.IsLoggedIn(email);
        }

        /// <summary>
        /// Add board to a users board list- the list of boards that the user is the owner of them
        /// </summary>
        /// <param name="email"> Email of a user </param>
        /// <param name="board"> Board the the user will be the owner </param>
        /// <returns> Bool- if the procedure succeed or not </returns>
        public bool AddBoard(string email, Board board)
        {
            return uc.AddBoard(email, board);
        }



    }
}