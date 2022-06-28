using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Frontend.View
{
    internal class BoardView : Window
    {
        private BoardModel bm;

        public BoardView(BoardModel bm)
        {
            this.bm = bm;
        }
    }
}
