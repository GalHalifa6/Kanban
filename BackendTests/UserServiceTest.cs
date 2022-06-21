using System;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BusinessLayer;
using System.Text.Json;
using IntroSE.Kanban.Backend.DataAccessLayer;

class UserServiceTest
{
    public UserServiceTest()
    {

    }
    public void RunTests()
    {
        DBConnector.GetInstance().ResetDB();
        RegisterTest();
        DBConnector.GetInstance().ResetDB();
        LoginTest();
        DBConnector.GetInstance().ResetDB();
        LogOutTest();
        DBConnector.GetInstance().ResetDB();
        GetUserBoardsTest();

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
        res = gradinService.Login("itaygmail.com", "123456Aa");
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

        Console.WriteLine("\n---------- TESTS FOR PASSWORDS ----------\n");
        Console.WriteLine("registerd with incorrect password. should succeed");
        gradinService.Register("omer@gmail.com", "123456Gg");
        res = gradinService.Login("omer@gmail.com", "123456Gg");
        Console.WriteLine(res);

        Console.WriteLine("registerd with incorrect password. should fail");
        gradinService.Register("check@gmail.com", " 123456Gg");
        res = gradinService.Login("check@gmail.com", " 123456Gg");
        Console.WriteLine(res);

        Console.WriteLine("registerd with incorrect password. should fail");
        gradinService.Register("check@gmail.com", "123456Gg ");
        res = gradinService.Login("check@gmail.com", "123456Gg ");
        Console.WriteLine(res);

        Console.WriteLine("registerd with incorrect password. should fail");
        gradinService.Register("check@gmail.com", "123456Gg ");
        res = gradinService.Login("check@gmail.com", "123456Gg ");
        Console.WriteLine(res);

        Console.WriteLine("registerd with incorrect password. should fail");
        gradinService.Register("check@gmail.com", "123456G");
        res = gradinService.Login("check@gmail.com", "123456G");
        Console.WriteLine(res);

        Console.WriteLine("registerd with incorrect password. should fail");
        gradinService.Register("check@gmail.com", "123456g");
        res = gradinService.Login("check@gmail.com", "123456g");
        Console.WriteLine(res);

        Console.WriteLine("registerd with incorrect password. should fail");
        gradinService.Register("check@gmail.com", "123gG");
        res = gradinService.Login("check@gmail.com", "123gG");
        Console.WriteLine(res);

        Console.WriteLine("registerd with incorrect password. should fail");
        gradinService.Register("check@gmail.com", "12345gG.");
        res = gradinService.Login("check@gmail.com", "12345gG.");
        Console.WriteLine(res);

        Console.WriteLine("registerd with incorrect password. should fail");
        gradinService.Register("check@gmail.com", "");
        res = gradinService.Login("check@gmail.com", "");
        Console.WriteLine(res);

        Console.WriteLine("registerd with incorrect password. should fail");
        gradinService.Register("check@gmail.com", "       ");
        res = gradinService.Login("check@gmail.com", "       ");
        Console.WriteLine(res);

        Console.WriteLine("registerd with incorrect password. should fail");
        gradinService.Register("check@gmail.com", "AaAaAaAa");
        res = gradinService.Login("check@gmail.com", "AaAaAaAa");
        Console.WriteLine(res);

        Console.WriteLine("registerd with incorrect password. should fail");
        gradinService.Register("check@gmail.com", "%1234Aa");
        res = gradinService.Login("check@gmail.com", "%1234Aa");
        Console.WriteLine(res);

        Console.WriteLine("registerd with incorrect password. should fail");
        gradinService.Register("check@gmail.com", "%1234Aa");
        res = gradinService.Login("check@gmail.com", "%1234Aa");
        Console.WriteLine(res);

        Console.WriteLine("registerd with incorrect password. should fail");
        gradinService.Register("check@gmail.com", "1234 Aa");
        res = gradinService.Login("check@gmail.com", "1234 Aa");
        Console.WriteLine(res);


    }


    ///<summary>
    ///This function test Requirement 8
    ///</summary>
    public void LoginTest()
    {
        GradingService gradinService = new GradingService();
        Console.WriteLine("login user correctly. should succeed");
        gradinService.Register("gal@gmail.com", "123456Aa");
        string res = gradinService.Login("gal@gmail.com", "123456Aa");
        Console.WriteLine(res);
    }


    ///<summary>
    ///This function test Requirement 8
    ///</summary>
    public void LogOutTest()
    {
        GradingService gradinService = new GradingService();
        Console.WriteLine("logout user correctly. should succeed");
        gradinService.Register("gal@gmail.com", "123456Aa");
        gradinService.Login("gal@gmail.com", "123456Aa");
        string res = gradinService.Logout("gal@gmail.com");
        Console.WriteLine(res);
    }

    public void GetUserBoardsTest()
    {
        GradingService gradinService = new GradingService();
        Console.WriteLine("return all user's boards. should succeed");
        gradinService.Register("gal@gmail.com", "123456Aa");
        gradinService.Login("gal@gmail.com", "123456Aa");
        gradinService.AddBoard("gal@gmail.com", "board1");
        gradinService.AddBoard("gal@gmail.com", "board2");
        string res = gradinService.GetUserBoards("gal@gmail.com");
        Console.WriteLine(res);

        Console.WriteLine("return all user's boards- empty board. should return null");
        gradinService.Register("itay@gmail.com", "123456Aa");
        gradinService.Login("itay@gmail.com", "123456Aa");
        res = gradinService.GetUserBoards("itay@gmail.com");
        Console.WriteLine(res);

        Console.WriteLine("return all user's boards- not registered. should fail");
        gradinService.Login("itay@gmail.com", "123456Aa");
        res = gradinService.GetUserBoards("itay@gmail.com");
        Console.WriteLine(res);

        Console.WriteLine("return all user's boards- not loging. should fail");
        gradinService.Register("itay@gmail.com", "123456Aa");
        res = gradinService.GetUserBoards("itay@gmail.com");
        Console.WriteLine(res);

    }

    public void TransferOwnership()
    {

        GradingService gradinService = new GradingService();
        Console.WriteLine("transfer ownership of a board. should succeed");
        gradinService.Register("gal@gmail.com", "123456Aa");
        gradinService.Login("gal@gmail.com", "123456Aa");
        gradinService.AddBoard("gal@gmail.com", "board1");
        gradinService.Register("itay@gmail.com", "123456Aa");
        gradinService.TransferOwnership("gal@gmail.com", "itay@gmail.com", "board1");
        gradinService.Login("itay@gmail.com", "123456Aa");
        string res = gradinService.GetUserBoards("itay@gmail.com");
        Console.WriteLine(res);

        Console.WriteLine("transfer ownership of a board- user did not register. should fail");
        gradinService.Register("gal@gmail.com", "123456Aa");
        gradinService.Login("gal@gmail.com", "123456Aa");
        gradinService.AddBoard("gal@gmail.com", "board1");
        gradinService.TransferOwnership("gal@gmail.com", "itay@gmail.com", "board1");
        res = gradinService.GetUserBoards("itay@gmail.com");
        Console.WriteLine(res);

        Console.WriteLine("transfer ownership of a board- user have no boards. should fail");
        gradinService.Register("tomer@gmail.com", "123456Aa");
        gradinService.Login("tomer@gmail.com", "123456Aa");
        gradinService.Register("itay@gmail.com", "123456Aa");
        gradinService.TransferOwnership("tomer@gmail.com", "itay@gmail.com", "board1");
        res = gradinService.GetUserBoards("itay@gmail.com");
        Console.WriteLine(res);

        Console.WriteLine("transfer ownership of a board- user can't have two boards with the same name. should fail");
        gradinService.Register("tomer@gmail.com", "123456Aa");
        gradinService.Login("tomer@gmail.com", "123456Aa");
        gradinService.Register("itay@gmail.com", "123456Aa");
        gradinService.Login("itay@gmail.com", "123456Aa");
        gradinService.AddBoard("itay@gmail.com", "board1");
        gradinService.AddBoard("tomer@gmail.com", "board1");
        gradinService.TransferOwnership("tomer@gmail.com", "itay@gmail.com", "board1");
        res = gradinService.GetUserBoards("itay@gmail.com");
        Console.WriteLine(res);

    }
}




