using Frontend.Model;
using Frontend.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for UserBoardsView.xaml
    /// </summary>
    public partial class UserBoardsView : Window
    {
        UserBoardsViewModel ubvm;
        public UserBoardsView(UserModel user)
        {
            this.DataContext = new UserBoardsViewModel(user);
            this.ubvm = (UserBoardsViewModel)DataContext;
            InitializeComponent();
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string boardName = UserBoards.SelectedItem as string;
            BoardModel bm = UserBoardsViewModel.GetBoard(boardName);
            if (bm != null)
            {
                BoardView boardView = new BoardView(bm);
                boardView.Show();
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
