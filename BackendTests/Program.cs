using System;
namespace IntroSE.Kanban.Backend;

class Program
{
    static void Main(String[] args)
    {
        //UserServiceTest userServiceTest = new UserServiceTest();
        BoardServiceTest boardServiceTest = new BoardServiceTest();
        boardServiceTest.RunTests();
        
    }
}