using System;
using IntroSE.Kanban.Backend.ServiceLayer;


class UserServiceTest
{
   public UserServiceTest()
    {
       
    }


    ///<summary>
    ///This function test Requirement 3
    ///</summary>
    public bool RegisterTestDoubleRegistration(string email, string password){
       UserService userService = new UserService();
        bool result = true;

       string str = userService.register("gahalifa@gmail.com", "12345678Aa");
       if (str != "ok")
        {
           result = false; 
        }
       
      
       str = userService.register("galhalifa@gmail.com", "123456Aa");
       if (str == "ok")
       {
            result = false;
       }
       return result;

       }
        
    

    ///<summary>
    ///This function test Requirement 2
    ///</summary>
    public bool RegisterTestInValidPassword(string email, string password){
    UserService userService = new UserService();
    bool result = true;
    string str = userService.register("galhalifa@gmail.com", "123456A");
    if (str == "ok")
    {
            result = false;
    }
    str = userService.register("galhalifa@gmail.com", "456Aa");
        if (str == "ok")
        {
            result = false;
        }

    str=userService.register("galhalifa@gmail.com","");
        if (str == "ok")
        {
            result = false;
        }


    str = userService.register("galhalifa@gmail.com", "");
    if (str == "ok")
    {
        result = false;
    }

    return result;


}
     
     ///<summary>
     ///This function test Requirement 1
     ///</summary>
     public bool RegisterTestInValidEmail(string email, string password){
     UserService userService = new UserService();
     bool result = true;

     string str = userService.register("", "12345678Aa");
     if (str != "ok")
     {
        result = false;
     }

     str = userService.register("itayg8676@gmailcom", "12345678Aa");
     if (str == "ok")
     {
       result = false;
     }

     return result;

    }
     

   
    public bool LoginTest(string email, string password){
        bool result =true;
        return result;

    }

    public bool LogOutTest(string email){
        bool result = true;
        return result;
    }


    public string runTestTest()
    {
        string result="";
        return result;
    }
}