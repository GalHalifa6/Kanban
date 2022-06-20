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
            Console.WriteLine(sc.Register("itay@gmail.com", "Aa123456"));
            Console.WriteLine(sc.Login("itay@gmail.com", "Aa123456"));
            Console.WriteLine(sc.AddBoard("itay@gmail.com", "B1"));
            Console.WriteLine(sc.Logout("itay@gmail.com"));
            Console.WriteLine(sc.Register("gal@gmail.com", "Aa123456"));
            Console.WriteLine(sc.Login("gAl@gmail.com", "Aa123456"));
            Console.WriteLine(sc.JoinBoard("gal@gmail.com", 0));
            Console.WriteLine(sc.Logout("gal@gmail.com"));
            Console.WriteLine(sc.Register("Tomer@gmail.com", "Aa123456"));
            Console.WriteLine(sc.Login("tomer@gmail.com", "Aa123456"));
            Console.WriteLine(sc.JoinBoard("tomER@gmail.com", 0));
            Console.WriteLine(sc.Logout("tomer@gmail.com"));
            Console.WriteLine(sc.Login("gAl@gmail.com", "Aa123456"));
            Console.WriteLine(sc.LeaveBoard("gal@gmail.com", 0));
            Console.WriteLine(sc.Logout("gal@gmail.com"));
            Console.WriteLine(sc.Login("itay@gmail.com", "Aa123456"));
            Console.WriteLine(sc.AddTask("itay@gmail.com", "B1", "Test", "test", new DateTime()));
            Console.WriteLine(sc.TransferOwnership("itay@gmail.com", "tomer@gmail.com", "B1"));

        }
    }
}