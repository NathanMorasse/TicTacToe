using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
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

        public static void SwitchReward(bool win)
        {
            if (win)
            {
                _GamePage.Reward_ViewBox.Visibility = Visibility.Hidden;
                _GamePage.Reward_Image.Visibility = Visibility.Visible;
            }
            else
            {
                _GamePage.Reward_Image.Visibility = Visibility.Hidden;
                _GamePage.Reward_ViewBox.Visibility = Visibility.Visible;
            }
        }

        public static void ApplyResult(string exclamation, string status, string message, SolidColorBrush color)
        {
            _GamePage.Exclamation_TextBlock.Text = exclamation;
            _GamePage.Result_textBlock.Text = status;
            _GamePage.Reward_TextBlock.Text = message;
            _GamePage.Finished_Color.Background = color;
        }

        public static void ResetGamePage()
        {
            _GamePage = new GamePage();
        }
    }
}
