using IntroSE.Kanban.BackendTests;
using System;
namespace IntroSE.Kanban.Backend;

class Program
{
    static void Main(String[] args)
    {
        UserServiceTest userServiceTest = new UserServiceTest();
        BoardServiceTest boardServiceTest = new BoardServiceTest();
        TaskServiceTest taskServiceTest = new TaskServiceTest();
        boardServiceTest.RunTests();
        userServiceTest.RunTests();
        taskServiceTest.runTests();
        
    }
}