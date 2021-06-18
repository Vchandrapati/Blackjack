﻿using System;
using static Blackjack.Classes.Variables;
using System.Diagnostics;
using System.Windows;

namespace Blackjack
{
    public partial class HelpPage : Window
    {
        public HelpPage()
        {
            InitializeComponent();

            _sqlconnect = new MySQLconnect();

            Connection = _sqlconnect.Connect();
        }

        private void BtnWeb_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("cmd", "/c start https://bicyclecards.com/how-to-play/blackjack/");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error opening this website {ex}");
            }
        }

        private void BtnEmail_OnClick(object sender, RoutedEventArgs e)
        {
            EmailPage ep = new EmailPage();
            ep.Show();
        }

        private void BtnBack_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}