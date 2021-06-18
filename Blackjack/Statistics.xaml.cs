using System.Windows;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Printing;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Blackjack.Classes;
using static Blackjack.Classes.Variables;

namespace Blackjack
{
    public partial class Statistics : Window
    {
        private string WR;
        public Statistics()
        {
            InitializeComponent();

            pfp.Source = Skin;

                    txtUser.Text = User;
            txtLR.Text = Losses.ToString();
            WR = (Convert.ToDecimal(Wins) / Convert.ToDecimal(GamesPlayed) * 100m).ToString("##.##") + "%";
            txtWRR.Text = WR;
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

        private void BtnPrint_OnClick(object sender, RoutedEventArgs e)
        {
            string Statistics = $"Username: {User} \r\n Losses: {Losses} \r\n Wins: {Wins} \r\n Win Rate: {WR} \r\n Games Played: {GamesPlayed} \r\n Money Won: ${MoneyWon} \r\n Money Lost: {MoneyLost} \r\n Pushes: {Pushes} \r\n Cards Drawn: {CardsDrawn}";

            PrintDocument StatsDoc = new PrintDocument();

            StatsDoc.PrintPage += delegate(object sender1, PrintPageEventArgs e1)
            {
                e1.Graphics.DrawString(Statistics, new Font("Times New Roman", 12), new SolidBrush(Color.Black),
                    new RectangleF(0, 0, StatsDoc.DefaultPageSettings.PrintableArea.Width,
                        StatsDoc.DefaultPageSettings.PrintableArea.Height));
            };

            try
            {
                StatsDoc.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error when printing. Error:{ex}");
            }
        }
        private void BtnAction_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Exit_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnRefresh_OnClick(object sender, RoutedEventArgs e)
        {
            Losses = _sqlconnect.Info(User, "Losses");
            Wins = _sqlconnect.Info(User, "Wins");
            GamesPlayed = _sqlconnect.Info(User, "Games_Played");
            MoneyWon = _sqlconnect.Info(User, "Money_Won");
            MoneyLost = _sqlconnect.Info(User, "Money_Lost");
            Pushes = _sqlconnect.Info(User, "Pushes");
            CardsDrawn = _sqlconnect.Info(User, "Cards_Drawn");
            Level = Math.Floor(_sqlconnect.Info(User, "Points") / 1000m) - 2m;
            Money = _sqlconnect.Info(User, "Money");
            Points = _sqlconnect.Info(User, "Points");
        }
    }
}
