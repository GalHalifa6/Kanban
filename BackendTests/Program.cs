using IntroSE.Kanban.Backend;
using IntroSE.Kanban.Backend.DataAccessLayer;
using System;


class Program
{
    static void Main(String[] args)
    {
        UserServiceTest userServiceTest = new UserServiceTest();
        BoardServiceTest boardServiceTest = new BoardServiceTest();
        TaskServiceTest taskServiceTest = new TaskServiceTest();
        DBConnector.GetInstance().ResetDB();
        boardServiceTest.RunTests();
        userServiceTest.RunTests();
        taskServiceTest.runTests();

    }
}
