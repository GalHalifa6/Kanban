using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class ColumnDTO
    {
        public int boardID { get; }
        public int ordinal{ get; }
        public int maxTasks { get; private set; }

    }
}
