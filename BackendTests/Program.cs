using IntroSE.Kanban.Backend;
using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BusinessLayer;

class Program
{
    static void Main(String[] args)
    {
        Console.WriteLine("Adding task to a user. Should succeed");
        GradingService gradingService = new GradingService();
        DBConnector.GetInstance().ResetDB();
        gradingService.Register("tomer@gmail.com", "123456Ab");
        gradingService.Register("gal@gmail.com", "123456Ab");
        gradingService.Register("itay@gmail.com", "123456Ab");
        gradingService.Login("tomer@gmail.com", "123456Ab");
        gradingService.AddBoard("tomer@gmail.com", "Board1");
        string res = gradingService.AddTask("tomer@gmail.com", "Board1", "Task1", "Testing task1", new DateTime());
        Console.WriteLine(res);
        Console.WriteLine("======================");
        Console.WriteLine("Adding assignee to Task1. Should fail");
        string res1 = gradingService.AssignTask("tomer@gmail.com", "Board1", 0, 0, "itay@gmail.com");
        Console.WriteLine(res1);
        Console.WriteLine("======================");
        Console.WriteLine("Adding assignee to Task1. Should succeed");
        gradingService.Logout("tomer@gmail.com");
        gradingService.Login("itay@gmail.com", "123456Ab");
        gradingService.JoinBoard("itay@gmail.com", 0);
        gradingService.Logout("itay@gmail.com");
        gradingService.Login("tomer@gmail.com", "123456Ab");
        string res2 = gradingService.AssignTask("tomer@gmail.com", "Board1", 0, 0, "itay@gmail.com");
        Console.WriteLine(res2);
        Console.WriteLine("======================");
        Console.WriteLine("Changing assignee of Task1. Should fail");
        string res3 = gradingService.AssignTask("tomer@gmail.com", "Board1", 0, 0, "gal@gmail.com");
        Console.WriteLine(res3);
    }
}
