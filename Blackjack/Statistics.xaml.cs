using System.Windows;
using Blackjack.Classes;
using static Blackjack.Classes.Variables;

namespace Blackjack
{
    public partial class Statistics : Window
    {
        private readonly MySQLconnect _sqlconnect;

        public Statistics()
        {
            InitializeComponent();

            _sqlconnect = new MySQLconnect();

            _sqlconnect.Connect();

            txtLR.Text = _sqlconnect.Info(user, "Losses").ToString();
            txtWRR.Text = $"%{_sqlconnect.Info(user, "Wins") / _sqlconnect.Info(user, "Games_Played") * 100}";
            txtWR1.Text = _sqlconnect.Info(user, "Wins").ToString();
            txtGPR.Text = _sqlconnect.Info(user, "Games_Played").ToString();
            txtMWR.Text = $"${_sqlconnect.Info(user, "Money_Won")}";
            txtMLR.Text = $"${_sqlconnect.Info(user, "Money_Lost")}";
            txtPR.Text = _sqlconnect.Info(user, "Pushes").ToString();
            txtCDR.Text = _sqlconnect.Info(user, "Cards_Drawn").ToString();
        }

        private void BtnBack_OnClick(object sender, RoutedEventArgs e)
        {
            Home_Page hp = new Home_Page();
            hp.Show();
            Close();
        }
    }
}
