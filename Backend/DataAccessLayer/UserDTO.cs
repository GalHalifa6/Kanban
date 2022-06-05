using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class UserDTO
    {
        public string email { get; set; }
        public string password { get; private set; }

        public UserDTO(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        private Response GeneralNonQuery(string query, string goodMsg, string badMsg)
        {
            if (!DBConnector.GetInstance().ExecuteNonQuery(query))
            {
                return new Response(badMsg, true);
            }
            return new Response(goodMsg);
        }


        public Response RegisterUser(string email, string password)
        {
            string query = $"INSERT INTO Users(email, password) VALUES('{email}', '{password}')";
            return GeneralNonQuery(query, "User was added successfully", "A user with this email already exists");
        }
    }
}