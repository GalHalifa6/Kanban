using Frontend.Model;
using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Frontend.ViewModel
{
    public class BackendController
    {
        private ServiceController service;
        private BackendController()
        {
            service = new ServiceController();
        }
        private static BackendController instance = null;
        public static BackendController Instance  
        {
                get 
                {
                    if (instance == null)
                    {
                        instance = new BackendController();
                    }
                    return instance;
                }
        }
        /*
        public BackendController()
        {
             service = new ServiceController();
        }
        */
        internal UserModel Login(string email, string password) // if there's a problem, try putting back the "new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }" in the login function in the backend
        {
            ResponseT<string> res = JsonConvert.DeserializeObject<ResponseT<string>>(service.Login(email, password));
            if (res.ErrorOccured())
            {
                throw new Exception(res.ErrorMessage);
            }
            return new UserModel(email);
        }

        internal UserModel Register(string email, string password) // if there's a problem, try putting back the "new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }" in the login function in the backend
        {
            ResponseT<string> res = JsonConvert.DeserializeObject<ResponseT<string>>(service.Register(email, password));
            if (res.ErrorOccured())
            {
                throw new Exception(res.ErrorMessage);
            }
            service.AddBoard(email, "B1");
            service.AddTask(email, "B1", "T1", "T1 DESC", new DateTime());
            return new UserModel(email);
        }

        internal List<string> GetUserBoards(string email)
        {
            ResponseT<List<string>> res = JsonConvert.DeserializeObject<ResponseT<List<string>>>(service.GetUserBoards(email));
            if (res.ErrorOccured())
            {
                throw new Exception(res.ErrorMessage);
            }
            return res.ReturnValue;
        }





    }
}