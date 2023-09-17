using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TicTacToe_Server.Views;

namespace TicTacToe_Server.ViewModels
{
    public static class ViewLink
    {
        private static GamePage _GamePage;
        private static WaitingPage _WaitingPage;

        private static PageHolder _PageHolder;

        public static WaitingPage WaitingPage 
        {
            get { return _WaitingPage; }
            set { _WaitingPage = value; }
        }

        public static GamePage GamePage
        {
            get { return _GamePage; }
            set { _GamePage = value;  }
        }

        public static PageHolder PageHolder
        {
            get { return _PageHolder; }
            set { _PageHolder = value; }
        }

        public static void NavigateToGame()
        {
            PageHolder.Holder.NavigationService.Navigate(GamePage);
        }
    }
}
