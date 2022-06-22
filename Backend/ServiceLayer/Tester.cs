using IntroSE.Kanban.Backend.DataAccessLayer;
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
            DBConnector db = DBConnector.GetInstance();
            ServiceController sc = new ServiceController();
            db.ResetDB();
            Console.WriteLine(sc.Register("itay@gmail.com", "Aa123456"));
            Console.WriteLine(sc.Register("itay@gmail.com", "Aa123456"));
            Console.WriteLine(sc.Logout("itay@gmail.com"));
            Console.WriteLine(sc.Login("itay@gmail.com", "Aa123456"));
            Console.WriteLine(sc.AddBoard("itay@gmail.com", "B0"));
            Console.WriteLine(sc.AddBoard("itay@gmail.com", "B0"));
            Console.WriteLine(sc.AddTask("itay@gmail.com", "B0", "task0", "test0", new DateTime()));
            Console.WriteLine(sc.AddTask("itay@gmail.com", "B0", "task1", "test1", new DateTime()));
            Console.WriteLine(sc.AddTask("itay@gmail.com", "B1", "task1", "test1", new DateTime()));
            Console.WriteLine(sc.AddTask("itay@gmail.com", "B0", "task1", "test1", new DateTime()));
            Console.WriteLine(sc.AdvanceTask("itay@gmail.com", "B1", 0, 2));
            Console.WriteLine(sc.AdvanceTask("itay@gmail.com", "B2", 0, 1));
            Console.WriteLine(sc.AdvanceTask("itay@gmail.com", "B1", 0, 1));
            Console.WriteLine(sc.AddBoard("itay@gmail.com", "B1"));
            Console.WriteLine(sc.Register("gal@gmail.com", "Aa123456"));
            Console.WriteLine(sc.JoinBoard("gal@gmail.com", 0));
            Console.WriteLine(sc.AddBoard("gal@gmail.com", "B0"));
            Console.WriteLine(sc.AddBoard("gal@gmail.com", "B1"));
            Console.WriteLine(sc.JoinBoard("gal@gmail.com", 1));
            Console.WriteLine(sc.JoinBoard("gal@gmail.com", 5));
            Console.WriteLine(sc.AddBoard("itay@gmail.com", ""));
            Console.WriteLine(sc.AddBoard("itay@gmail.com", "       "));
            Console.WriteLine(sc.AddBoard("itay@gmail.com", null));

            /*            Console.WriteLine(sc.LoadData());
                        Console.WriteLine(sc.Login("itay@gmail.com", "Aa123456"));*/
        }
    }
}
