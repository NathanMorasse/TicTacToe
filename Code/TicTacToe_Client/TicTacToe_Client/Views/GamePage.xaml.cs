﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TicTacToe_Client.Models;

namespace TicTacToe_Client.Views
{
    /// <summary>
    /// Logique d'interaction pour GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        Button selected = null;

        public GamePage()
        {
            InitializeComponent();
        }

        private void Case_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = ((Button)sender);

            if (clicked.Content != "X" || clicked.Content != "O")
            {
                if (selected != null)
                {
                    selected.Background = Brushes.Transparent;
                }

                clicked.Background = new SolidColorBrush(Color.FromRgb(210,255,137));
                selected = clicked;
                Confirm_Move.IsEnabled = true;
            }
        }

        private void Confirm_Move_Click(object sender, RoutedEventArgs e)
        {
            selected.Background = Brushes.Transparent;
        }
    }
}
