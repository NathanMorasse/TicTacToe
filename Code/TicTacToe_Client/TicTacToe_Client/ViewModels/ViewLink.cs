using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe_Client.Views;

namespace TicTacToe_Client.ViewModels
{
    public static class ViewLink
    {
        private static GamePage _GamePage;
        private static ConnectionPage _ConnectionPage;

        private static PageHolder _PageHolder;

        public static GamePage GamePage
        {
            get { return _GamePage; }
            set { _GamePage = value; }
        }

        public static ConnectionPage ConnectionPage
        {
            get { return _ConnectionPage; }
            set { _ConnectionPage = value; }
        }

        public static PageHolder PageHolder
        {
            get { return _PageHolder; }
            set { _PageHolder = value; }
        }
    }
}
