using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    
    class Tester
    {
        static void Main(String[] args)
        {
/*            DBConnector db = DBConnector.GetInstance();
            //ServiceController sc = new ServiceController();
            db.ResetDB();*/
/*            Console.WriteLine(sc.Register("itay@gmail.com", "Aa123456"));
            Console.WriteLine(sc.Register("gal@gmail.com", "Aa123456"));
            Console.WriteLine(sc.AddBoard("itay@gmail.com", "B0"));
            Console.WriteLine(sc.JoinBoard("gal@gmail.com", 0));
            Console.WriteLine(sc.AddTask("itay@gmail.com", "B0", "task0", "test0", new DateTime()));
            Console.WriteLine(sc.AddTask("itay@gmail.com", "B0", "task1", "test1", new DateTime()));
            Console.WriteLine(sc.AdvanceTask("itay@gmail.com", "B0", 0, 0));
            Console.WriteLine(sc.AdvanceTask("itay@gmail.com", "B0", 1, 0));
            Console.WriteLine(sc.AssignTask("itay@gmail.com", "B0", 2, 0, "itay@gmail.com"));
*//*            Console.WriteLine(sc.AddTask("gal@gmail.com", "B0", "task0", "test0", new DateTime()));
            Console.WriteLine(sc.AdvanceTask("itay@gmail.com", "B0", 0, 1));
            Console.WriteLine(sc.AddTask("itay@gmail.com", "B0", "task0", "test0", new DateTime()));
            Console.WriteLine(sc.AddTask("itay@gmail.com", "B0", "task0", "test0", new DateTime()));*//*
            Console.WriteLine(sc.InProgressTasks("itay@gmail.com"));*/

        }
    }
}
