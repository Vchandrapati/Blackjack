using System.Windows;
using System;
using Blackjack.Classes;
using static Blackjack.Classes.Variables;

namespace Blackjack
{
    public partial class Statistics : Window
    {
        public Statistics()
        {
            InitializeComponent();

            txtUser.Text = User;
            txtLR.Text = Losses.ToString();
            txtWRR.Text = (Convert.ToDecimal(Wins) / Convert.ToDecimal(Games_Played) * 100m).ToString("##.##") + "%";
            txtWR1.Text = Wins.ToString();
            txtGPR.Text = Games_Played.ToString();
            txtMWR.Text = $"${Money_Won}";
            txtMLR.Text = $"${Money_Lost}";
            txtPR.Text = Pushes.ToString();
            txtCDR.Text = Cards_Drawn.ToString();
        }

        private void BtnBack_OnClick(object sender, RoutedEventArgs e)
        {
            Home_Page hp = new Home_Page();
            hp.Show();
            Close();
        }
    }
}
