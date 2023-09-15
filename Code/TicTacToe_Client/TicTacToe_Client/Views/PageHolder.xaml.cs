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
using System.Windows.Shapes;

namespace TicTacToe_Client.Views
{
    /// <summary>
    /// Logique d'interaction pour PageHolder.xaml
    /// </summary>
    public partial class PageHolder : Window
    {
        public PageHolder()
        {
            InitializeComponent();

            //Holder.NavigationService.Navigate(new WaitingPage());
            Holder.NavigationService.Navigate(new GamePage());
        }
    }
}