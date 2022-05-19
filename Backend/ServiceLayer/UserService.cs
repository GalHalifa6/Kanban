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
        public UserController uc { get; }
      
        public UserService()
        {
            uc = new UserController();
        }

        /// <summary>
        /// Registers a user to the system
        /// </summary>
        /// <param name="email"> email to be registered </param>
        /// <param name="password"> password of the user </param>
        /// <returns></returns>
        public string Register(string email, string password)
        {
            email = email.ToLower();
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
            Response response = uc.createUser(email, password);
            if (response.ErrorOccured())
                JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return "{}";
        }

        /// <summary>
        /// Allows a user to log in to the system
        /// </summary>
        /// <param name="email">Email of the user that is trying to log in</param>
        /// <param name="password">Password of the user trying to log in</param>
        /// <returns></returns>
        public string Login(string email, string password)
        {
            email = email.ToLower();
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
            Response response = uc.login(email, password);
            if (response.ErrorOccured())
                return JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return JsonConvert.SerializeObject(new Response(email), Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        }

        /// <summary>
        /// Allows a logged in user to log out
        /// </summary>
        /// <param name="email"> Email of the user trying to log out</param>
        /// <returns></returns>
        public string Logout(string email)
        {
            email = email.ToLower();
            if (IsValidEmail(email) == false)
            {
                Response r = new Response("Invalid email", true);
                return JsonConvert.SerializeObject(r, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            Response response = uc.LogOut(email);
            if (response.ErrorOccured())
            {
                JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            return "{}";
        }


        /// <summary>
        /// Deletes an existing user from the system
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string DeleteUser(string email, string password)
        {
            email = email.ToLower();
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
        /// Allows a user to change password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>

        public string ChangePassword(string email, string oldPassword, string newPassword)
        {
            email = email.ToLower();    
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
            Response response = uc.changePassword(email, oldPassword, newPassword);
            return JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        }

        /// <summary>
        /// validataion function for passwords, uses helper functions for smaller validations
        /// </summary>
        /// <param name="pass">password to validate</param>
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

        public bool IsLoggedIn(string email)
        {
            return uc.IsLoggedIn(email);
    }
    }
   
}