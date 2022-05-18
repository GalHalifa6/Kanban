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
            Console.WriteLine(gs.Register("itay@gmail.com", "123456Ab"));
            Console.WriteLine(gs.Login("itay@gmail.com", "123456Ab"));
            Console.WriteLine(gs.AddBoard("itay@gmail.com", "    "));
/*            Console.WriteLine(gs.AddTask("itay@gmail.com", "Board1", "t1", "test task", new DateTime()));
            Console.WriteLine(gs.UpdateTaskTitle("itay@gmail.com", "Board1", 0, 1, "updated test title"));
            Console.WriteLine(gs.AddTask("itay@gmail.com", "Board1", "t1", "test task", new DateTime()));
            Console.WriteLine(gs.GetColumn("itay@gmail.com", "Board1", 0));*/
        }
    }
}
