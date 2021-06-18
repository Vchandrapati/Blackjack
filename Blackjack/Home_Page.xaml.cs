using System;
using System.Windows;
using static Blackjack.Classes.Variables;

namespace Blackjack
{
    public partial class Home_Page : Window
    {
        public Home_Page()
        {
            InitializeComponent();
            txtuser.Text = User;

            _sqlconnect = new MySQLconnect();

            Connection = _sqlconnect.Connect();

            if (Guest)
            {
                Level = 2;
                Money = 3000;
                Points = 4000;
                GamesPlayed = 0;
                CardsDrawn = 0;
                GamesPlayed = 0;
                MoneyWon = 0;
                MoneyLost = 0;
                Wins = 0;
                Losses = 0;
                Pushes = 0;

                txtLevel.Text = "2";
                txtMoney.Text = "$3000";
                txtPoints.Text = "4000 Points";
            }
            else
            {
                if (!Once)
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
                    Once = true;
                }

                txtLevel.Text = $"{Level}";
                txtMoney.Text = $"${Money}";
                txtPoints.Text = $"{Points}";
            }
        }

        private void BtnStart_OnClick(object sender, RoutedEventArgs e)
        {
            Game_Board gb = new Game_Board();
            gb.Show();
            Close();
        }

        private void BtnBoards_OnClick(object sender, RoutedEventArgs e)
        {
            if (Connection)
            {
                Leaderboards lb = new Leaderboards();
                lb.Show();
                Close();
            }
            else if (!Connection)
                MessageBox.Show("Server connection unavailable");
            else if (Guest)
                MessageBox.Show("You must be signed in to have this functionality");
        }

        private void btnStatistics_OnClick(object sender, RoutedEventArgs e)
        {
            if (Connection)
            {
                Statistics st = new Statistics();
                st.Show();
                Close();
            }
            else if (!Connection)
                MessageBox.Show("Server connection unavailable");
            else if (Guest)
                MessageBox.Show("You must be signed in to have this functionality");
        }

        private void BtnHelp_OnClick(object sender, RoutedEventArgs e)
        {
            HelpPage hp = new HelpPage();
            hp.Show();
        }
    }
}
