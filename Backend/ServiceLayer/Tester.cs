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
            DBConnector db = DBConnector.GetInstance();
            GradingService gradingService = new GradingService();

            db.ResetDB();
            Console.WriteLine(gradingService.Register("gal@gmail.com", "Aa123456"));
            Console.WriteLine(gradingService.Login("gal@gmail.com", "Aa123456"));
            Console.WriteLine(gradingService.AddBoard("gal@gmail.com", "B1"));




        }
    }
}
