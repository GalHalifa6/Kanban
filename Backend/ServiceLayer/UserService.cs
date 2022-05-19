using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class UserService
    {
        public UserController uc { get; }
      
        public UserService()
        {
            uc = new UserController();
        }

        public string Register(string email, string password)
        {
            if (IsValidEmail(email) == false)
                return JsonSerializer.Serialize(new Response("Invalid email", true));
            if (IsValidPassword(password) == false)
                return JsonSerializer.Serialize(new Response("Invalid password", true));
            Response response = uc.createUser(email, password);
            return JsonSerializer.Serialize(response);
        }

        public string Login(string email, string password)
        {
            if (IsValidEmail(email) == false)
                return JsonSerializer.Serialize(new Response("Invalid email", true));
            if (IsValidPassword(password) == false)
                return JsonSerializer.Serialize(new Response("Invalid password", true));
            Response response = uc.login(email, password);
            return JsonSerializer.Serialize(response);

        }

        public string Logout(string email)
        {
            if (IsValidEmail(email) == false)
            {
                return JsonSerializer.Serialize(new Response("Invalid email", true));
            }
            Response response = uc.LogOut(email);
            if (response.ErrorOccured())
            {
                return JsonSerializer.Serialize(response);
            }
            return "{}";
        }


        public string DeleteUser(string email, string password)
        {
            if (IsValidEmail(email) == false)
                return JsonSerializer.Serialize(new Response("Invalid email", true));
            if (IsValidPassword(password) == false)
                return JsonSerializer.Serialize(new Response("Invalid password", true));
            Response response = uc.DeleteUser(email, password);
            return JsonSerializer.Serialize(response);
        }


        public string ChangePassword(string email, string oldPassword, string newPassword)
        {
            if (IsValidPassword(oldPassword) == false)
                return GenerateBadResponseString("Old password is invalid password");
            if (IsValidPassword(newPassword) == false)
                return GenerateBadResponseString("New password is invalid password");
            if (IsValidEmail(email) == false)
                return GenerateBadResponseString("Invalid email");
            Response response = uc.changePassword(email, oldPassword, newPassword);
            return JsonSerializer.Serialize(response);

        }

        private bool IsValidEmail(string email)
        {
            bool state = true;
            int indexAt = email.IndexOf('@');
            int indexDot = email.IndexOf('.');

            if (indexAt == -1 || indexDot == -1)
                return false;
            if(indexDot == email.Length - 1)
                return false;

            //The email contains '@' and '.'
            if (indexAt == 0) { state = false; }
            if (indexDot == 0) { state = false; }
            if (indexDot - indexAt == 1) { state = false; }
            if (email.Length - indexDot == 0) { state = false; }

            int counterAt = 0;
            //check if the email contains ' ' empty string
            for (int i = 0; i < email.Length; i++)
            {
                if (email[i] == ' ')
                {
                    state = false;
                }
                if(email[i] == '@')
                {
                    counterAt++;
                }
            }
            if (counterAt != 1)
            {
                return false;
            }
            else
            {
                return state;
            }           
        }

        private bool IsValidPassword(string password)
        {
            for (int m = 0; m < password.Length; m++)
            {
                if (password[m].Equals(" "))
                {
                    return false;
                }
            }
            if (password.Length < 6 || password.Length > 20)
            {
                return false;
            }
            int upperCase = 0, lowerCase = 0, numbers = 0;
            byte[] bytes = Encoding.ASCII.GetBytes(password);
            for (int i = 0; i < password.Length; i++)
            {
                if (97 <= bytes[i] && bytes[i] <= 122) { lowerCase++; }
                else if (65 <= bytes[i] && bytes[i] <= 90) { upperCase++; }
                else if (48 <= bytes[i] && bytes[i] <= 57) { numbers++; }
                else { return false; }
            }
            if (upperCase >= 1 && lowerCase >= 1 && numbers >= 1)
            { return true; }
            else
            {
                return false;
            }
        }

        public string GenerateBadResponseString(string errMsg)
        {
            return "{ErrorMessage: " + errMsg + ", ReturnValue: null}";
        }
        private string GenerateGoodResponseString(string value)
        {
            return "{ErrorMessage: null, ReturnValue: " + value + "}";
        }

        public bool IsLoggedIn(string email)
        {
            return uc.IsLoggedIn(email);
    }
    }
   
}