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

        }
    }
}
