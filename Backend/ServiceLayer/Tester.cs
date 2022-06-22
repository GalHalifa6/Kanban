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

            /*            db.ResetDB();
                        Console.WriteLine(sc.Register("itay@gmail.com", "Aa123456"));
                        Console.WriteLine(sc.Login("itay@gmail.com", "Aa123456"));
                        Console.WriteLine(sc.Login("itay@gmail.com", "Aa123456"));
                        Console.WriteLine(sc.Logout("itay@gmail.com"));
                        Console.WriteLine(sc.Login("itay@gmail.com", "Aa123456"));
                        Console.WriteLine(sc.AddBoard("itay@gmail.com", "B0"));
                        Console.WriteLine(sc.Register("gal@gmail.com", "Aa123456"));
                        //Console.WriteLine(sc.AddBoard("itay@gmail.com", "B0"));
                        Console.WriteLine(sc.AddBoard("gal@gmail.com", "B1"));
                        Console.WriteLine(sc.JoinBoard("itay@gmail.com", 1));
                        Console.WriteLine(sc.LimitColumn("itay@gmail.com", "B1", 0, 9));
                        Console.WriteLine(sc.GetColumnLimit("itay@gmail.com", "B1", 0));
                        Console.WriteLine(sc.AddBoard("itay@gmail.com", "B1"));
                        Console.WriteLine(sc.LeaveBoard("itay@gmail.com", 1));
                        Console.WriteLine(sc.AddBoard("itay@gmail.com", "B1"));*/

            Console.WriteLine(sc.LoadData());
            Console.WriteLine(sc.Login("itay@gmail.com", "Aa123456"));
        }
    }
}
