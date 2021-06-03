using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                txtLevel.Text = $"2";
                txtMoney.Text = $"$3000";
                txtPoints.Text = $"4000 Points";
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
            this.Close();
        }

        private void BtnBoards_OnClick(object sender, RoutedEventArgs e)
        {
            Leaderboards lb = new Leaderboards();
            lb.Show();
            this.Close();
        }

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = (Button) sender;

            if (btn.Name.Equals("btnStart"))
            {
                btnStart.Width += 10;
                btnStart.Height += 10;
            }
            else if (btn.Name.Equals("btnBoards"))
            {
                BtnBoards.Width += 10;
                BtnBoards.Height += 10;
            }
            else if (btn.Name.Equals("btnStatistics"))
            {
                btnStatistics.Width += 10;
                btnStatistics.Height += 10;
            }
            else if (btn.Name.Equals("btnShop"))
            {
                btnShop.Width += 10;
                btnShop.Height += 10;
            }
            else if (btn.Name.Equals("btnHelp"))
            {
                btnHelp.Width += 10;
                btnHelp.Height += 10;
            }
        }

        private void BtnStatistics_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Name.Equals("btnStart"))
            {
                btnStart.Width -= 10;
                btnStart.Height -= 10;
            }
            else if (btn.Name.Equals("btnBoards"))
            {
                BtnBoards.Width -= 10;
                BtnBoards.Height -= 10;
            }
            else if (btn.Name.Equals("btnStatistics"))
            {
                btnStatistics.Width -= 10;
                btnStatistics.Height -= 10;
            }
            else if (btn.Name.Equals("btnShop"))
            {
                btnShop.Width -= 10;
                btnShop.Height -= 10;
            }
            else if (btn.Name.Equals("btnHelp"))
            {
                btnHelp.Width -= 10;
                btnHelp.Height -= 10;
            }
        }
    }
}
