using System;
using System.Globalization;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BusinessLayer;
using System.Text.Json;

namespace IntroSE.Kanban.Backend
{



    class BoardServiceTest


    class BoardServiceTest
    {

        public BoardServiceTest()
        {

        }
        public void RunTests()
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


        /*
        ///<summary>
        ///This function test Requirement 9
        ///</summary>
        public void RemoveTaskTest()
        {


        }
        */



        ///<summary>
        ///This function test Requirement 14,15
        ///</summary>
        public void editTaskTest()
        {

        }


        /*
        ///<summary>
        ///This function test Requirement 14,15
        ///</summary>
        public void editTaskDescriptionTest()
        {
            
        }
        */

        /*
        ///<summary>
        ///This function test Requirement 14,15
        ///</summary>
        public void editTaskDueDateTest()
        {
          
        }
        */


        /*

        ///<summary>
        ///This function test Requirement 9
        ///</summary>
        public void addBoardTest()
        {
            
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
        public void advanceTaskPhaseTest(string Title)
}
}
        }
        */

    }
}
