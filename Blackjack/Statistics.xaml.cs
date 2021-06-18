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
            txtWRR.Text = (Convert.ToDecimal(Wins) / Convert.ToDecimal(GamesPlayed) * 100m).ToString("##.##") + "%";
            txtWR1.Text = Wins.ToString();
            txtGPR.Text = GamesPlayed.ToString();
            txtMWR.Text = $"${MoneyWon}";
            txtMLR.Text = $"${MoneyLost}";
            txtPR.Text = Pushes.ToString();
            txtCDR.Text = CardsDrawn.ToString();
        }

        private void BtnBack_OnClick(object sender, RoutedEventArgs e)
        {
            Home_Page hp = new Home_Page();
            hp.Show();
            Close();
        }
    }
}
