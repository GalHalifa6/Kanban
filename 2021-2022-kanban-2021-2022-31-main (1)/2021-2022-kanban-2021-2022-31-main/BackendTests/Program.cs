using IntroSE.Kanban.Backend;
using System;


    class Program
    {
        static void Main(String[] args)
        {
            UserServiceTest userServiceTest = new UserServiceTest();
            BoardServiceTest boardServiceTest = new BoardServiceTest();
            TaskServiceTest taskServiceTest = new TaskServiceTest();
            //boardServiceTest.RunTests();
            userServiceTest.RunTests();
            //taskServiceTest.runTests();

        }
    }
