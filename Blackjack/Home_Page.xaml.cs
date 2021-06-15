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
                txtLevel.Text = "2";
                txtMoney.Text = "$3000";
                txtPoints.Text = "4000 Points";
            }
            else
            {
                Losses = _sqlconnect.Info(User, "Losses");
                Wins = _sqlconnect.Info(User, "Wins");
                Games_Played = _sqlconnect.Info(User, "Games_Played");
                Money_Won = _sqlconnect.Info(User, "Money_Won");
                Money_Lost = _sqlconnect.Info(User, "Money_Lost");
                Pushes = _sqlconnect.Info(User, "Pushes");
                Cards_Drawn = _sqlconnect.Info(User, "Cards_Drawn");

                txtLevel.Text = $"{_sqlconnect.Info(User, "Level") / 1000}";
                txtMoney.Text = $"${_sqlconnect.Info(User, "Money")}";
                txtPoints.Text = $"{_sqlconnect.Info(User, "Points")}";
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
    }
}
