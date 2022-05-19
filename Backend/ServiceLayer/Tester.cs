using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    internal class Tester
    {
        static void Main(String[] args)
        {
            GradingService gs = new GradingService();
            DateTime dt = new DateTime();
            dt = dt.AddDays(20);
            Console.WriteLine(gs.Login("itay@gmail.com", "123456Ab"));
            Console.WriteLine(gs.Register("itay@gmail.com", "123456Ab"));
            Console.WriteLine(gs.Login("iTAy@gmail.com", "123456Ab"));
            Console.WriteLine(gs.AddBoard("itay@gmail.com", "B1"));
            Console.WriteLine(gs.AddTask("itay@gmail.com", "B1", "t1", "test", dt));
            Console.WriteLine(gs.AddTask("itay@gmail.com", "B1", "t2", "test", dt));
            Console.WriteLine(gs.AdvanceTask("itay@gmail.com", "B1", 0, 0));
            Console.WriteLine(gs.AdvanceTask("itay@gmail.com", "B1", 1, 0));
            //Console.WriteLine(gs.AdvanceTask("itay@gmail.com", "B1", 1, 0));
            Console.WriteLine(gs.UpdateTaskDueDate("itaY@gmail.com", "B1", 2, 0, dt));
            GradingService gradingService = new GradingService();
            string email = "rrr@gmial.com";
            string board = "one";
            var date = "5/1/2028 8:30:00 AM";
            var dateN = "5/1/2020 8:30:00 AM";
            DateTime DueDate = DateTime.Parse(date, System.Globalization.CultureInfo.InvariantCulture);
            DateTime DueDateN = DateTime.Parse(dateN, System.Globalization.CultureInfo.InvariantCulture);
            string invalid = "jgiosejiooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo";
            Console.WriteLine("++" + gradingService.Register(email, "Aka123k123"));                                        //create user Aka123k123
            Console.WriteLine("++" + gradingService.Login(email, "Aka123k123"));                                           //log in user Aka123k123
            Console.WriteLine("++" + gradingService.Logout(email));                                           //log in user Aka123k123
            Console.WriteLine("++" + gradingService.Login(email, "Aka1256563k123"));                                           //log in user Aka123k123
            Console.WriteLine("++" + gradingService.Login(email, "Aka123k123"));                                           //log in user Aka123k123

            Console.WriteLine("++" + gradingService.AddBoard(email, "one"));                                               //add board one
            Console.WriteLine("++" + gradingService.AddBoard(email, ""));                                               //add board one
            Console.WriteLine("++" + gradingService.AddBoard(email, "     "));                                               //add board one

            Console.WriteLine("++" + gradingService.AddBoard(email, null));                                               //add board one
            Console.WriteLine("-----------------ADD TASKS TESTS------------");
            Console.WriteLine("++" + gradingService.AddTask(email, "one", "bRAND", "HELLOW WORLD", DueDate));         // task number 0
            Console.WriteLine("++" + gradingService.AddTask(email, "one", "bRAND", "HELLOW WORLD", DueDateN));         //false - early
            Console.WriteLine("++" + gradingService.AddTask(email, "one", "bRAND", "HELLOW WORLD", DateTime.Now));         //false - early
            Console.WriteLine("++" + gradingService.AddTask(email, "one", "new", "HELLOW WORLD", DueDate));           //add taskID 1 new
            Console.WriteLine("++" + gradingService.AddTask(email, "one", null, "HELLOW WORLD", DueDate));         //null - invalid
            Console.WriteLine("+------+" + gradingService.AddTask(email, "one", "", "HELLOW WORLD", DueDate));         //only spaces - invalid
            Console.WriteLine(gradingService.AddTask(email, "one", "liran", "   ", DueDate));         // task number 1
            Console.WriteLine("++" + gradingService.AddTask(email, "one", "liran", null, DueDate));         //task number 2


            Console.WriteLine("-----------------ADVANCE TASKS TESTS------------");

            Console.WriteLine("++" + gradingService.AdvanceTask(email, "one", 0, 0));                                      //advance taskID 0 to column 1
            Console.WriteLine("++" + gradingService.AdvanceTask(email, "one", 0, 1));                                      //advance taskID 1 to column 1
            Console.WriteLine("++" + gradingService.AdvanceTask(email, "one", 1, 0));                                      //advance taskID 0 to column 2
            Console.WriteLine("++" + gradingService.AdvanceTask(email, "one", 1, 1));                                      //advance taskID 1 to column 2
            Console.WriteLine("++" + gradingService.AddTask(email, "one", "new", "HELLOW WORLD", DueDate));           //add taskID 3 new
            Console.WriteLine("++" + gradingService.AdvanceTask(email, "one", 2, 0));                                      //advance taskID 0 to column 3 - Error
            Console.WriteLine("++" + gradingService.AdvanceTask(email, "one", 0, 0));                                      // no such task in column 0
            Console.WriteLine("++" + gradingService.InProgressTasks(email));                                               //error

            Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 2));                                      //advance taskID 2 to column 1
            Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 3));                                      //advance taskID 2 to column 1
            Console.WriteLine(gradingService.InProgressTasks(email));                                               //return taskID 2
            Console.WriteLine(gradingService.LimitColumn(email, board, 1, 5));                                      //limit column 1 to 5
            Console.WriteLine(gradingService.LimitColumn(email, board, 1, 4));                                      //limit column 1 to 4
            Console.WriteLine(gradingService.LimitColumn(email, board, 1, 10));                                     //limit column 1 to 10
            Console.WriteLine(gradingService.GetColumnLimit(email, board, 1));                                      //receive limit - 10
            Console.WriteLine("-----GET COLUMN NAME----------");
            Console.WriteLine(gradingService.GetColumnName(email, board, 5));                                       // INVALID NUMBER - Error
            Console.WriteLine(gradingService.AddTask(email, "three", "new", "HELLOW WORLD", DueDate));         // no such board three
            Console.WriteLine(gradingService.UpdateTaskDueDate(email, "one", 1, 0, new DateTime(2022, 10 , 5)));                  // not good , changes to task that not in true coloumn number
            Console.WriteLine(gradingService.UpdateTaskDueDate(email, "one", 9, 2, DueDate));
            Console.WriteLine("_" + gradingService.AddBoard(email, "two"));                                               //add Board 'two'
                                                                                                                          // not good , changes to invalid coloumn number
            Console.WriteLine("----------REMOVE BOARD--------");
            Console.WriteLine(gradingService.RemoveBoard(email, "one"));                                            //delete Board one
            Console.WriteLine(gradingService.AddTask(email, "one", "new", "HELLOW WORLD", DueDate));           //Error - there is no Board with this name

            Console.WriteLine("_" + gradingService.AddBoard(email, "one"));                                               //add Board 'two'
            Console.WriteLine(gradingService.AddTask(email, "two", "new", "HELLOW WORLD", DueDate));           //add taskID 0 new
            Console.WriteLine(gradingService.UpdateTaskDueDate(email, "two", 0, 0, DueDate));                  //update dueDate
            Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 0, 0, "new title"));                     //update taskID 0 title to 'new title'
            Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 0, 1, "new title"));                     //Error - no such task
            Console.WriteLine(gradingService.AdvanceTask(email, "two", 0, 0));                                      //advance taskID 0 to column 1
            Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 1, 0, "new title"));                     //update taskID 0 title to 'new title'
            Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 1, 0, invalid));                         //Error - invalid title
            Console.WriteLine(gradingService.UpdateTaskDescription(email, "two", 1, 0, "new descp"));               //update taskID 0 description
            Console.WriteLine(gradingService.UpdateTaskDescription(email, "two", 1, 0, invalid));                   //Error - invalid description
            Console.WriteLine(gradingService.LimitColumn(email, "two", 1, 1));                                      //limit column 1 to 1
            Console.WriteLine(gradingService.AddTask(email, "two", "new task", "HELLOW WORLD", DateTime.Now));      //add taskID 2 to Board 'two' with title 'new task'
            Console.WriteLine(gradingService.AdvanceTask(email, "two", 0, 1));

        }
    }
}
