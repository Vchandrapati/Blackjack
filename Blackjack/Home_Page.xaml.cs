using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using MySql.Data.MySqlClient;
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
                txtLevel.Text = $"Level 2";
                txtMoney.Text = $"$3000";
                txtPoints.Text = $"4000 Points";
            }
            else
            {
                txtLevel.Text = $"Level {_sqlconnect.Info(user, "Level") / 1000}";
                txtMoney.Text = $"${_sqlconnect.Info(user, "Money")}";
                txtPoints.Text = $"{_sqlconnect.Info(user, "Points")} Points";
            }
        }

        private void BtnStart_OnClick(object sender, RoutedEventArgs e)
        {
            Game_Board gb = new Game_Board();
            gb.Show();
            this.Close();
        }

        private void BtnBoards_OnClick(object sender, RoutedEventArgs e)
        {
            Leaderboards lb = new Leaderboards();
            lb.Show();
            this.Close();
        }
    }
}
