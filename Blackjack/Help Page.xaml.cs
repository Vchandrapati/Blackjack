using System;
using static Blackjack.Classes.Variables;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Blackjack
{
    public partial class HelpPage : Window
    {
        public HelpPage()
        {
            InitializeComponent();
        }

        private void BtnWeb_OnClick(object sender, RoutedEventArgs e)
        {
            //Opening web help
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
            //Email Devs
            EmailPage ep = new EmailPage();
            ep.Show();
        }

        private void BtnBack_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void BtnAction_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Exit_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
