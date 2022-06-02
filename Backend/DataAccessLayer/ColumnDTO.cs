using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class ColumnDTO
    {
        public int boardID { get; }
        public int ordinal{ get; }
        public int maxTasks { get; private set; }
        public HashSet<TaskDTO> tasks { get; private set; }

        internal void RemoveTask(TaskDTO taskDTO)
        {
            throw new NotImplementedException();
        }

        internal void AddTask(TaskDTO taskDTO)
        {
            throw new NotImplementedException();
        }
    }
}
