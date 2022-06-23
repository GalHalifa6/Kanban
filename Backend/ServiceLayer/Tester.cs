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
                        Console.WriteLine(sc.Register("itay@gmail.com", "Aa123456"));
                        Console.WriteLine(sc.Logout("itay@gmail.com"));
                        Console.WriteLine(sc.Login("itay@gmail.com", "Aa123456"));
                        Console.WriteLine(sc.Login("itay@gmail.com", "Aa12345"));
                        Console.WriteLine(sc.Login("itay@gmail.com", "Aa1234565"));
                        Console.WriteLine(sc.Login("itay@gmail.com", "45t46554"));
                        Console.WriteLine(sc.Login("itay@gmail.com", null));
                        Console.WriteLine(sc.Login(null, "Aa123456"));
                        Console.WriteLine(sc.Login(null, null));*/
            /*            Console.WriteLine(sc.Register("gal@gmail.com", "Aa123456"));
                        Console.WriteLine(sc.Login("gal@gmail.com", "Aa123456"));*/
/*
            Console.WriteLine(sc.Register("itay@gmail.com", "Aa123456"));
            Console.WriteLine(sc.LoadData());
            Console.WriteLine(sc.Login("gal@gmail.com", "Aa123456"));
            Console.WriteLine(sc.Login("itay@gmail.com", "Aa123456"));
            Console.WriteLine(sc.DeleteData());

            Console.WriteLine(sc.Login("itay@gmail.com", "Aa123456"));
            Console.WriteLine(sc.Logout("itay@gmail.com"));*/
            // db.ResetDB();
        }
    }
}
