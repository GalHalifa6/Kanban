using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    internal class BoardService
    {
        BusinessLayer.BoardController bc {get};

        public BoardService()
        {
            
        }

        public string addBoard(string name) {
            throw new NotImplementedException();
        }

        public string removeBoard(string name) {
            throw new NotImplementedException();
        }

        public string addTask(string title, string description) {
            throw new NotImplementedException();
        }

        public string removeTask(string title) {
            throw new NotImplementedException();
        }

        public string limitColumn(string boardName, int columnNumber, int newLimit) {
            throw new NotImplementedException();
        }

        public string editTaskTitle(string oldTitle, string newTitle) {
            throw new NotImplementedException();
        }

        public string editTaskDescription(string title, string newDesc) {
            throw new NotImplementedException();
        }

        public string editTaskDueDate(string title, DateTime newDate) {
            throw new NotImplementedException();
        }


    }
}
