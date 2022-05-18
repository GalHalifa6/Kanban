using System;
using System.Globalization;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BusinessLayer;
using System.Text.Json;

namespace IntroSE.Kanban.Backend
{



    class BoardServiceTest
    {

        public BoardServiceTest()
        {

        }

        internal void runTests()
        {
            //addTaskTest();
            //removeBoardTest();



        }

        /*
        ///<summary>
        ///This function test Requirement 12
        ///</summary>
        public void addTaskTest()
        {
            Console.WriteLine("Adding task to a user. Should succeed");
            GradingService gradingService = new GradingService();
            gradingService.Register("gal@gmail.com", "123456Ab");
            gradingService.Login("gal@gmail.com", "123456Ab");
            gradingService.AddBoard("gal@gmail.com", "Board1");
            string res = gradingService.AddTask("gal@gmail.com", "Board1", "Task1", "Testing task1", new DateTime());
            Console.WriteLine(res);
            //Response res = JsonSerializer.Deserialize<Response>(jsonResponse);

            Console.WriteLine("-----------------------");
            Console.WriteLine("Adding task to an non exist user. Should fail");
            gradingService.Login("itay@gmail.com", "123456Aa");
            gradingService.AddBoard("itay@gmail.com", "Board1");
            res = gradingService.AddTask("itay@gmail.com", "Board2", "Task1", "Testing task1", new DateTime());
            Console.WriteLine(res);

            Console.WriteLine("-----------------------");
            Console.WriteLine("Adding task to a user that not connected. Should fail");
            gradingService.Register("itay@gmail.com", "123456Aa");
            gradingService.AddBoard("itay@gmail.com", "Board3");
            res = gradingService.AddTask("itay@gmail.com", "Board1", "Task1", "Testing task1", new DateTime());
            Console.WriteLine(res);
        }
        */



        ///<summary>
        ///This function test Requirement 9
        ///</summary>
        public void removeTaskTest()
        {
            GradingService gradingService = new GradingService();
            gradingService.Register("gal@gmail.com", "12345Aa");
            gradingService.Login("gal@gmail.com", "12345Aa");




        }



        /*
        ///<summary>
        ///This function test Requirement 14,15
        ///</summary>
        public void editTaskTest()
        {
            BoardService board = new BoardService();
            board.addTask("Task1", "Testing task1");
            string jsonResponse = board.editTaskTitle("Task1", "Task2");
            Response res = JsonSerializer.Deserialize<Response>(jsonResponse);
            Console.WriteLine(res);

            string jsonResponse1 = board.editTaskTitle("Task3", "Task4");
            Response res1 = JsonSerializer.Deserialize<Response>(jsonResponse);
            Console.WriteLine("The following test should failed:");
            Console.WriteLine(res1);
        }
        */

        /*
        ///<summary>
        ///This function test Requirement 14,15
        ///</summary>
        public void editTaskDescriptionTest()
        {
            BoardService board = new BoardService();
            board.addTask("Task1", "Testing task1");
            string jsonResponse = board.editTaskDescription("Task1", "Testing new");
            Response res = JsonSerializer.Deserialize<Response>(jsonResponse);
            Console.WriteLine(res);

            string jsonResponse1 = board.editTaskDescription("Task3", "Testing New");
            Response res1 = JsonSerializer.Deserialize<Response>(jsonResponse);
            Console.WriteLine("The following test should failed:");
            Console.WriteLine(res1);
        }
        */

        /*
        ///<summary>
        ///This function test Requirement 14,15
        ///</summary>
        public void editTaskDueDateTest()
        {
            DateTime date = new DateTime();
            BoardService board = new BoardService();
            board.addTask("Task1", "Testing task1");
            string jsonResponse = board.editTaskDueDate("Task1", date);
            Response res = JsonSerializer.Deserialize<Response>(jsonResponse);
            Console.WriteLine(res);

            string jsonResponse1 = board.editTaskDueDate("Task3", date);
            Response res1 = JsonSerializer.Deserialize<Response>(jsonResponse);
            Console.WriteLine("The following test should failed:");
            Console.WriteLine(res1);
        }
        */


        /*

        ///<summary>
        ///This function test Requirement 9
        ///</summary>
        public void addBoardTest()
        {
            BoardService board = new BoardService();
            string jsonResponse = board.addBoard("Board1");
            Response res = JsonSerializer.Deserialize<Response>(jsonResponse);
            Console.WriteLine(res);

            string jsonResponse1 = board.addBoard("Board1");
            Response res1 = JsonSerializer.Deserialize<Response>(jsonResponse);
            Console.WriteLine("The following test should failed:");

            Console.WriteLine(res1);
        }
        */



        /*
        ///<summary>
        ///This function test Requirement 9
        ///</summary>
        public void removeBoardTest()
        {
            GradingService gradingService = new GradingService();
            Console.WriteLine("-----------------------");
            Console.WriteLine("Remove board from a user. should succeed");
            gradingService.Register("gal@gmail.com", "123456Aa");
            gradingService.Login("gal@gmail.com", "123456Aa");
            string res = gradingService.AddBoard("gal@gmail.com", "Board4");
            Console.WriteLine(res);
            gradingService.RemoveBoard("gal@gmail.com", "Board4");
            Console.WriteLine(res);

            Console.WriteLine("-----------------------");
            Console.WriteLine("Remove board from a user that not logged in. nshould fail");
            gradingService.Register("itay@gmail.com", "123456Aa");
            gradingService.AddBoard("itay@gmail.com", "Board5");
            res = gradingService.RemoveBoard("itay@gmail.com", "Board5");
            Console.WriteLine(res);

            Console.WriteLine("-----------------------");
            Console.WriteLine("Remove board that not exist from a user. should fail");
            gradingService.Register("gal@gmail.com", "123456Aa");
            gradingService.Login("gal@gmail.com", "123456Aa");
            res = gradingService.RemoveBoard("gal@gmail.com", "Board2");
            Console.WriteLine(res);
        }
        */

        /*
        ///<summary>
        ///This function test Requirement 13
        ///</summary>
        public void advanceTaskPhaseTest(string title)
        {
            BoardService board = new BoardService();
            board.addTask("Task1", "Testing task1");
            string jsonResponse1 = board.advanceTask("Task1");
            Response res = JsonSerializer.Deserialize<Response>(jsonResponse1);
            Console.WriteLine(res);


            string jsonResponse2 = board.advanceTask("Task2");
            Console.WriteLine("The following test should failed:");
            Response res2 = JsonSerializer.Deserialize<Response>(jsonResponse2);
            Console.WriteLine(res2);

            board.advanceTask("Task1");
            string jsonResponse3 = board.advanceTask("Task1");
            Response res3 = JsonSerializer.Deserialize<Response>(jsonResponse3);
            Console.WriteLine("The following test should failed:");
            Console.WriteLine(res3);


        }
        */

    }
}
