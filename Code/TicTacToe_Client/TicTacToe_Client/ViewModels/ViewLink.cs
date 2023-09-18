using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using TicTacToe_Client.Views;
using TicTacToe_Client.Models;

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

        public static void NavigateToGame()
        {
            PageHolder.Holder.NavigationService.Navigate(GamePage);
        }

        public static void ResetGamePage()
        {
            _GamePage = new GamePage();
        }

        public static void ToggleRestartButton()
        {
            GamePage.Restart_Game.IsEnabled = true;
            GamePage.Restart_Game.Content = "Recommencer";
        }

        public static void UpdateTurn()
        {
            if (Game.IsMyTurn)
            {
                GamePage.YourTurn_TextBlock.Text = "C'est à votre tour";
                GamePage.YourSide_TextBlock.Text = "Vous jouez les X bleu";
                GamePage.Instruction_TextBlock.Text = "Sélectionner un emplacement libre puis appuyer sur confirmer pour jouer votre coup.";
            }
            else
            {
                GamePage.YourTurn_TextBlock.Text = "C'est au tour de votre adversaire";
                GamePage.YourSide_TextBlock.Text = "Il joue les O rouge";
                GamePage.Instruction_TextBlock.Text = "Patientez pendant que votre adversaire choisit un coup";
            }
            GamePage.Confirm_Move.IsEnabled = Game.IsMyTurn && GamePage.selected != null;
        }
    }
}
