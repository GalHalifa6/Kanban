using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{

    public class BoardModel
    {
        private string boardName;
        public string BoardName { get => boardName; }

        public BoardModel(string boardName)
        {
            this.boardName = boardName;
        }


        public override string ToString()
        {
            return boardName;
        }

    }
}
