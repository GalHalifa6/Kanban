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
        

        public UserService()
        {
            uc = new UserController();
        }

        public string Register(string email, string password)
        {
            if (IsValidEmail(email) == false)
                return "false";
            if (IsValidPassword(password) == false)
                return "false";


            Response<bool> response = uc.createUser(email, password);
            if (response.ErrorOccured)
            {
                return response.ErrorMessage;
            }
            return "{}";
        }

        public string Login(string email, string password)
        {
            if (IsValidEmail(email) == false)
                return "false";
            if (IsValidPassword(password) == false)
                return "false";

            Response<bool> response = uc.login(email, password);
            if (response.ErrorOccured)
            {
                return response.ErrorMessage;
            }
            return "{}";
        }

        public string Logout(string email)
        {
            if (IsValidEmail(email) == false)
                return "false";

            Response<bool> response = uc.LogOut(email);
            if (response.ErrorOccured)
            {
                return response.ErrorMessage;
            }
            return "{}";

        }


        public string DeleteUser(string email, string password)
        {
            if (IsValidEmail(email) == false)
                return "false";
            if (IsValidPassword(password) == false)
                return "false";

            Response<bool> response = uc.DeleteUser(email, password);
            if (response.ErrorOccured)
            {
                return response.ErrorMessage;
            }
            return "{}";
        }

        public string ChangePassword(string email, string oldPassword, string newPassword)
        {
            if (IsValidPassword(oldPassword) == false)
                return "false";
            if (IsValidPassword(newPassword) == false)
                return "false";
            if (IsValidEmail(email) == false)
                return "false";


            Response<bool> response = uc.changePassword(email, oldPassword, newPassword);
            if (response.ErrorOccured)
            {
                return response.ErrorMessage;
            }
            return "{}";
        }

        private bool IsValidEmail(string email)
        {
            bool state = true;
            int indexAt = email.IndexOf('@');
            int indexDot = email.IndexOf('.');

            if (indexAt == -1 || indexDot == -1)
                return false;

            //The email contains '@' and '.'
            if (indexAt == 0) { state = false; }
            if (indexDot == 0) { state = false; }
            if (indexDot - indexAt == 1) { state = false; }
            if (email.Length - indexDot == 0) { state = false; }

            //check if the email contains ' ' empty string
            for (int i = 0; i < email.Length; i++)
            {
                if (email[i] == ' ')
                {
                    state = false;
                }
            }
            return state;
        }

        private bool IsValidPassword(string password)
        {
            for (int m = 0; m <= password.Length; m++)
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





    }
}
