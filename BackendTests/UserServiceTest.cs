using System;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BusinessLayer;
using System.Text.Json;


class UserServiceTest
{
    public UserServiceTest()
    {

    }


    ///<summary>
    ///This function test Requirement 1,2,3,7
    ///</summary>

    ///This function test Requirement 1,7
    public void RegisterTest(string email, string password)
    {
        UserService userService = new UserService();
        string jsonResponse1 = userService.register("example@gmail.com", "123456Aa");
        Response<bool> res = JsonSerializer.Deserialize<Response<bool>>(jsonResponse1);
        Console.WriteLine(res);

        ///This function test Requirement 3
        string jsonResponse2 = userService.register("example@gmail.com", "12345678Aa");
        Response<bool> res2 = JsonSerializer.Deserialize<Response<bool>>(jsonResponse2);
        Console.WriteLine("The following test should failed:");
        Console.WriteLine(res2);

        ///This function test Requirement 2
        string jsonResponse3 = userService.register("example@gmailcom", "12345678Aa");
        Response<bool> res3 = JsonSerializer.Deserialize<Response<bool>>(jsonResponse3);
        Console.WriteLine("The following test should failed:");
        Console.WriteLine(res3);

        string jsonResponse4 = userService.register("examplegmail.com", "12345678Aa");
        Response<bool> res4 = JsonSerializer.Deserialize<Response<bool>>(jsonResponse4);
        Console.WriteLine("The following test should failed:");
        Console.WriteLine(res4);

        string jsonResponse5 = userService.register("example@gmail.com", "12345678A");
        Response<bool> res5 = JsonSerializer.Deserialize<Response<bool>>(jsonResponse5);
        Console.WriteLine("The following test should failed:");
        Console.WriteLine(res5);

        string jsonResponse6 = userService.register("example@gmail.com", "12a345678");
        Response<bool> res6 = JsonSerializer.Deserialize<Response<bool>>(jsonResponse6);
        Console.WriteLine("The following test should failed:");
        Console.WriteLine(res6);

        string jsonResponse7 = userService.register("example@gmail.com", "");
        Response<bool> res7 = JsonSerializer.Deserialize<Response<bool>>(jsonResponse7);
        Console.WriteLine("The following test should failed:");
        Console.WriteLine(res7);

        string jsonResponse8 = userService.register("", "12345678Aa");
        Response<bool> res8 = JsonSerializer.Deserialize<Response<bool>>(jsonResponse8);
        Console.WriteLine("The following test should failed:");
        Console.WriteLine(res8);
    }


    ///<summary>
    ///This function test Requirement 8
    ///</summary>
    public void LoginTest(string email, string password)
    {
        UserService userService = new UserService();
        userService.register("example@gmail.com", "123456Aa");
        string jsonResponse1 = userService.login("example@gmail.com", "123456Aa");
        Response<bool> res = JsonSerializer.Deserialize<Response<bool>>(jsonResponse1);
        Console.WriteLine(res);

        string jsonResponse2 = userService.login("example2@gmail.com", "123456Aa");
        Response<bool> res2 = JsonSerializer.Deserialize<Response<bool>>(jsonResponse2);
        Console.WriteLine("The following test should failed:");
        Console.WriteLine(res2);

        string jsonResponse3 = userService.login("example2@gmail.com", "1234Aa");
        Response<bool> res3 = JsonSerializer.Deserialize<Response<bool>>(jsonResponse3);
        Console.WriteLine("The following test should failed:");
        Console.WriteLine(res3);
    }


    ///<summary>
    ///This function test Requirement 8
    ///</summary>
    public void LogOutTest(string email)
    {
        UserService userService = new UserService();
        userService.register("example@gmail.com", "123456Aa");
        userService.login("example@gmail.com", "123456Aa");
        string jsonResponse1 = userService.logout();
        Response<bool> res = JsonSerializer.Deserialize<Response<bool>>(jsonResponse1);
        Console.WriteLine(res);


    }



}