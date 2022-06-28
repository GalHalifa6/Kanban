using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    internal class UserBoardsViewModel
    {
        private UserModel user;

        public UserBoardsViewModel(UserModel user)
        {
            this.user = user;
        }
        internal static BoardModel GetBoard(string? boardName)
        {
            throw new NotImplementedException();
        }
    }
}
