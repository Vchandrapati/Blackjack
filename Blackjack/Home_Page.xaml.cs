using System.Windows;
using static Blackjack.MainWindow;

namespace Blackjack
{
    public partial class Home_Page : Window
    {
        private readonly MySQLconnect _sqlconnect;

        public Home_Page()
        {
            InitializeComponent();
            txtuser.Text = user;

            _sqlconnect = new MySQLconnect();

            _sqlconnect.Connect();

            if (guest)
            {
                txtLevel.Text = "2";
                txtMoney.Text = "$3000";
                txtPoints.Text = "4000 Points";
            }
            else
            {
                txtLevel.Text = $"{_sqlconnect.Info(user, "Level") / 1000}";
                txtMoney.Text = $"${_sqlconnect.Info(user, "Money")}";
                txtPoints.Text = $"{_sqlconnect.Info(user, "Points")}";
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
            if (connection)
            {
                Leaderboards lb = new Leaderboards();
                lb.Show();
                Close();
            }
            else
                MessageBox.Show("Server connection unavailable");
        }
    }
}
