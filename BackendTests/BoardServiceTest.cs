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
        public void RunTests()
        {
            //addTaskTest();
            //removeBoardTest();
            //editTaskTitleTest();
            //addBoardTest();
            //advanceTaskPhaseTest("task1");
            editTaskDescriptionTest();


        }


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
            Console.WriteLine("Adding task to a user, title- up to 50. Should fail");//fix this. should return error
            gradingService.Register("tomer@gmail.com", "123456Aa");
            gradingService.Login("tomer@gmail.com", "123456Aa");
            gradingService.AddBoard("tomer@gmail.com", "Board3");
            res = gradingService.AddTask("tomer@gmail.com", "Board3", "zzzzzzzzzzxxxxxxxxxxssssssssssaaaaaaaaaaddddddddddd", "testing task", new DateTime());
            Console.WriteLine(res);

            Console.WriteLine("-----------------------");//fix this. should return error
            Console.WriteLine("Adding task to a user, description- up to 300. Should fail");
            gradingService.Register("Omer@gmail.ac.il", "123456Aa");
            gradingService.Login("Omer@gmail.ac.il", "123456Aa");
            gradingService.AddBoard("Omer@gmail.ac.il", "Board8");
            res = gradingService.AddTask("Omer@gmail.ac.il", "Board8", "zzzzzzzzzzxxxxxxxxxssssssssssaaaaaaaaaadddddddddddzzzzzzzzzzxxxxxxxxxxssssssssssaaaaaaaaaadddddddddddzzzzzzzzzzxxxxxxxxxxssssssssssaaaaaaaaaadddddddddddzzzzzzzzzzxxxxxxxxxxssssssssssaaaaaaaaaadddddddddddzzzzzzzzzzxxxxxxxxxxssssssssssaaaaaaaaaadddddddddddzzzzzzzzzzxxxxxxxxxxssssssssssaaaaaaaaaadddddddddddzzzzzzzzzzxxxxxxxxxxssssssssxssaaaaaaaaaaddddddddddd", "testing task", new DateTime());
            Console.WriteLine(res);

            Console.WriteLine("-----------------------");//fix this. should return error
            Console.WriteLine("adding task with empty title. should fail");
            gradingService.AddBoard("Omer@gmail.ac.il", "Board6");
            res = gradingService.AddTask("Omer@gmail.ac.il", "Board6", "", "testing task", new DateTime());
            Console.WriteLine(res);

            Console.WriteLine("-----------------------");
            Console.WriteLine("Adding task to an non exist user. Should fail");
            gradingService.Login("itay@gmail.com", "123456Aa");
            gradingService.AddBoard("itay@gmail.com", "Board1");
            res = gradingService.AddTask("itay@gmail.com", "Board1", "Task1", "Testing task1", new DateTime());
            Console.WriteLine(res);

            Console.WriteLine("-----------------------");
            Console.WriteLine("Adding task to a user that not connected. Should fail");
            gradingService.Register("itay@gmail.com", "123456Aa");
            gradingService.AddBoard("itay@gmail.com", "Board3");
            res = gradingService.AddTask("itay@gmail.com", "Board1", "Task1", "Testing task1", new DateTime());
            Console.WriteLine(res);
        }



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
        public void editTaskTitleTest()
        {
            Console.WriteLine("Editing task to a user. should succeed");
            GradingService gradingService = new GradingService();
            gradingService.Register("gal@gmail.com", "123456Aa");
            gradingService.Login("gal@gmail.com", "123456Aa");
            gradingService.AddBoard("gal@gmail.com", "Board1");
            string res = gradingService.AddTask("gal@gmail.com", "Board1", "task1", "testing task1", new DateTime());
            Console.WriteLine(res);
            res = gradingService.UpdateTaskTitle("gal@gmail.com", "Board1", 0, 1, "task2");
            Console.WriteLine(res);

            Console.WriteLine("-----------------------");
            Console.WriteLine("Editing task to an non exist user. should fail");
            gradingService.Login("tomer@gmail.com", "123456Aa");
            gradingService.AddBoard("tomer@gmail.com", "Board1");
            res = gradingService.AddTask("tomer@gmail.com", "Board1", "task1", "testing rask1", new DateTime());
            Console.WriteLine(res);           

            Console.WriteLine("-----------------------");
            Console.WriteLine("editing a task that not exist. should succeed");
            res =  gradingService.UpdateTaskTitle("gal@gmail.com", "Board1", 0, 2, "task2");
            Console.WriteLine(res);

            Console.WriteLine("-----------------------");
            Console.WriteLine("editing a task with an empty name, should fail");
            gradingService.Register("itay@gmail.com", "123456Aa");
            gradingService.Login("itay@gmail.com", "123456Aa");
            gradingService.AddBoard("itay@gmail.com", "Board2");
            gradingService.AddTask("itay@gmail.com", "Board2", "", "testing task2", new DateTime());
            res =  gradingService.UpdateTaskTitle("itay@gmail.com", "Board1", 0, 1, "");
            Console.WriteLine(res);







        }



        ///<summary>
        ///This function test Requirement 14,15
        ///</summary>
        public void editTaskDescriptionTest()
        {
            Console.WriteLine("Editing task to a user. should succeed");
            GradingService gradingService = new GradingService();
            gradingService.Register("gal@gmail.com", "123456Aa");
            gradingService.Login("gal@gmail.com", "123456Aa");
            gradingService.AddBoard("gal@gmail.com", "Board1");
            string res = gradingService.AddTask("gal@gmail.com", "Board1", "task1", "testing task1", new DateTime());
            Console.WriteLine(res);
            res = gradingService.UpdateTaskDescription("gal@gmail.com", "Board1", 0, 1, "testing task2");
            Console.WriteLine(res);

            Console.WriteLine("-----------------------");
            Console.WriteLine("Editing task to an non exist user. should fail");
            gradingService.Login("tomer@gmail.com", "123456Aa");
            gradingService.AddBoard("tomer@gmail.com", "Board1");
            res = gradingService.AddTask("tomer@gmail.com", "Board1", "task1", "testing rask1", new DateTime());
            res = gradingService.UpdateTaskDescription("tomer@gmail.com", "Board1", 0, 2, "testing task2");
            Console.WriteLine(res);

            Console.WriteLine("-----------------------");
            Console.WriteLine("editing a task that not exist. should fail");
            res = gradingService.UpdateTaskDescription("gal@gmail.com", "Board1", 0, 3, "task3");
            Console.WriteLine(res);

            Console.WriteLine("-----------------------");
            Console.WriteLine("editing a task with an empty name, should fail");
            gradingService.Register("itay@gmail.com", "123456Aa");
            gradingService.Login("itay@gmail.com", "123456Aa");
            gradingService.AddBoard("itay@gmail.com", "Board2");
            gradingService.AddTask("itay@gmail.com", "Board2", "task7", "testing task7", new DateTime());
            res = gradingService.UpdateTaskDescription("itay@gmail.com", "Board2", 0, 2, " ");
            Console.WriteLine(res);

        }


        /*
        ///<summary>
        ///This function test Requirement 14,15
        ///</summary>
        public void editTaskDueDateTest()
        {

        }
        */



        ///<summary>
        ///This function test Requirement 9
        ///</summary>
        public void addBoardTest()
        {
            GradingService gradingService = new GradingService();
            Console.WriteLine("add two boards with the same name. should fail");
            gradingService.Register("gal@gmail.com", "123456Aa");
            gradingService.Login("gal@gmail.com", "123456Aa");
            string res = gradingService.AddBoard("gal@gmail.com", "Board1");
            res = gradingService.AddTask("gal@gmail.com", "Board1", "task", "testing task", new DateTime());
            Console.WriteLine(res);
            res = gradingService.AddBoard("gal@gmail.com", "Board1");
            Console.WriteLine(res);

            Console.WriteLine("-----------------------");
            Console.WriteLine("adding boards with the same name to a different users. should succeed");
            gradingService.Register("gal@gmail.com", "123456Aa");
            gradingService.Login("gal@gmail.com", "123456Aa");
            gradingService.Register("itay@gmail.com", "123456Aa");
            gradingService.Login("itay@gmail.com", "123456Aa");
            res = gradingService.AddBoard("gal@gmail.com", "Board2");
            Console.WriteLine(res);
            res = gradingService.AddBoard("itay@gmail.com", "Board2");
            Console.WriteLine(res);

            Console.WriteLine("-----------------------");
            Console.WriteLine("adding an empty board name. should fail");
            gradingService.Register("omer@gmail.com", "123456Aa");
            gradingService.Login("omer@gmail.com", "123456Aa");
            res = gradingService.AddBoard("omer@gmail.com", " ");
            Console.WriteLine(res);

            Console.WriteLine("-----------------------");
            Console.WriteLine("adding an empty board name. should fail");
            res = gradingService.AddBoard("omer@gmail.com", "");
            Console.WriteLine(res);
        }





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



        ///<summary>
        ///This function test Requirement 13
        ///</summary>
        public void advanceTaskPhaseTest(string Title)
        {
            GradingService gradingService = new GradingService();
            Console.WriteLine("advace task twice. should succeed");
            gradingService.Register("gal@gmail.com", "123456Aa");
            gradingService.Login("gal@gmail.com", "123456Aa");
            gradingService.AddBoard("gal@gmail.com", "Board1");
            gradingService.AddTask("gal@gmail.com", "Board1", Title, "testing task1", new DateTime());
            string res = gradingService.AdvanceTask("gal@gmail.com", "Board1", 0, 1);
            Console.Write(res);
            res = gradingService.AdvanceTask("gal@gmail.com", "Board1", 1, 1);
            Console.Write(res+"\n");

            Console.WriteLine("-----------------------");
            Console.WriteLine("advace task three times. should fail");
            gradingService.Register("itay@gmail.com", "123456Aa");
            gradingService.Login("itay@gmail.com", "123456Aa");
            gradingService.AddBoard("itay@gmail.com", "Board1");
            gradingService.AddTask("itay@gmail.com", "Board1", Title, "testing task1", new DateTime());
            res = gradingService.AdvanceTask("itay@gmail.com", "Board1", 0, 2);
            Console.Write(res);
            res = gradingService.AdvanceTask("itay@gmail.com", "Board1", 1, 2);
            Console.Write(res);
            res = gradingService.AdvanceTask("itay@gmail.com", "Board1", 2, 2);
            Console.Write(res);

            Console.WriteLine("-----------------------");
            Console.WriteLine("advace unexist task. should fail");
            gradingService.AddBoard("gal@gmail.com", "Board2");
            gradingService.AddTask("gal@gmail.com", "Board2", Title, "testing task9", new DateTime());
            res = gradingService.AdvanceTask("gal@gmail.com", "Board2", 0, 4);
            Console.Write(res);







        }
    }
}       
       
