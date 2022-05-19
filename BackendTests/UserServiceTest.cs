using System;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BusinessLayer;
using System.Text.Json;


class UserServiceTest
{
    public UserServiceTest()
    {

    }
    public void RunTests()
    {
        RegisterTest();


    }


    ///<summary>
    ///This function test Requirement 1,2,3,7
    ///</summary>

    ///This function test Requirement 1,7
    public void RegisterTest()
    {
        GradingService gradinService = new GradingService();
        Console.WriteLine("\n---------- TESTS FOR EMAIL ----------\n");
        Console.WriteLine("registerd user correctly. should succeed");
        gradinService.Register("gal@gmail.com", "123456Aa");
        string res = gradinService.Login("gal@gmail.com", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("-----------------------");
        Console.WriteLine("try to log in without registerd. should fail");
        res = gradinService.Login("itay@gmail.com", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("-----------------------");
        Console.WriteLine("registerd with incorrect email. should fail");
        gradinService.Register("itaygmail.com", "123456Aa");
        res =  gradinService.Login("itaygmail.com", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("-----------------------");
        Console.WriteLine("registerd with incorrect email. should fail");
        gradinService.Register("itay@@gmail.com", "123456Aa");
        res = gradinService.Login("itay@@gmail.com", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("-----------------------");
        Console.WriteLine("registerd with incorrect email. should fail");
        gradinService.Register("itay @gmail.com", "123456Aa");
        res = gradinService.Login("itay @gmail.com", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("-----------------------");
        Console.WriteLine("registerd with incorrect email. should fail");
        gradinService.Register(" ", "123456Aa");
        res = gradinService.Login(" ", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("-----------------------");
        Console.WriteLine("registerd with incorrect email. should fail");
        gradinService.Register("galhalifa", "123456Aa");
        res = gradinService.Login("galhalifa ", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("-----------------------");
        Console.WriteLine("registerd with incorrect email. should fail");
        gradinService.Register(".gal@gmail.com", "123456Aa");
        res = gradinService.Login(".gal@gmail.com ", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("-----------------------");
        Console.WriteLine("registerd with incorrect email. should fail");
        gradinService.Register("%tomer@gmail.com", "123456Aa");
        res = gradinService.Login("%tomer@gmail.com ", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("-----------------------");
        Console.WriteLine("registerd with incorrect email. should fail");
        gradinService.Register("itay@post.bgu.ac.il", "123456Aa");
        res = gradinService.Login("itay@post.bgu.ac.il", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("-----------------------");
        Console.WriteLine("registerd with incorrect email. should fail");
        gradinService.Register("itay@gmail.", "123456Aa");
        res = gradinService.Login("itay@gmail.", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("-----------------------");
        Console.WriteLine("registerd with incorrect email. should fail");
        gradinService.Register("itay@gmail.com ", "123456Aa");
        res = gradinService.Login("itay@gmail.com ", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("-----------------------");
        Console.WriteLine("registerd with incorrect email. should fail");
        gradinService.Register(" itay@gmail.", "123456Aa");
        res = gradinService.Login(" itay@gmail.com", "123456Aa");
        Console.WriteLine(res);


        Console.WriteLine("-----------------------");
        Console.WriteLine("registerd with incorrect email. should fail");
        gradinService.Register("itay@gmail.comitay@gmail.com", "123456Aa");
        res = gradinService.Login("itay@gmail.comitay@gmail.com", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("-----------------------");
        Console.WriteLine("registerd with incorrect email. should fail");
        gradinService.Register("itay@gmail.com2", "123456Aa");
        res = gradinService.Login("itay@gmail.com2", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("-----------------------");
        Console.WriteLine("registerd with incorrect email. should fail");
        gradinService.Register("email@123.123.123.123", "123456Aa");
        res = gradinService.Login("email@123.123.123.123", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("-----------------------");
        Console.WriteLine("registerd with incorrect email. should fail");
        gradinService.Register("email@123.123.123.123", "123456Aa");
        res = gradinService.Login("email@123.123.123.123", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("-----------------------");
        Console.WriteLine("registerd with incorrect email. should fail");
        gradinService.Register("____@gmail.com", "123456Aa");
        res = gradinService.Login("____@gmail.com", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("-----------------------");
        Console.WriteLine("registerd with incorrect email. should fail");
        gradinService.Register("miki-dan@example.com", "123456Aa");
        res = gradinService.Login("miki-dan@example.com", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("-----------------------");
        Console.WriteLine("registerd with incorrect email. should fail");
        gradinService.Register("email@subdomain.example.com", "123456Aa");
        res = gradinService.Login("email@subdomain.example.com", "123456Aa");
        Console.WriteLine(res);

        Console.WriteLine("\n---------- TESTS FOR PASSWORDS ----------\n");
        Console.WriteLine("registerd with incorrect password. should succeed");
        gradinService.Register("omer@gmail.com", "123456Gg");
        res = gradinService.Login("omer@gmail.com", "123456Gg");
        Console.WriteLine(res);



    }


    ///<summary>
    ///This function test Requirement 8
    ///</summary>
    public void LoginTest()
    {

    }


    ///<summary>
    ///This function test Requirement 8
    ///</summary>
    public void LogOutTest()
    {

    }



}