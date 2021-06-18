using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Blackjack.MySQLconnect;
using static Blackjack.Classes.Variables;
using MySql.Data.MySqlClient;

namespace Blackjack
{
    public partial class Leaderboards : Window
    {
        public Leaderboards()
        {
            InitializeComponent();

            //Retrieving all player data and sorting by descending
            DataTable lb = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                conn.Open();
                string query = "SELECT Username, Points, Level, Money FROM user_data ORDER BY Points DESC";
                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                    da.Fill(lb);
            }

            dgLeaders.ItemsSource = lb.DefaultView;
        }

        private void dgLeaders_Loaded(object sender, RoutedEventArgs e)
        {
            //Resisizing columns
            dgLeaders.Columns[0].Width = 120;
            dgLeaders.Columns[1].Width = 100;
            dgLeaders.Columns[2].Width = 200;
            dgLeaders.Columns[3].Width = 200;
        }

        private void BtnBack_OnClick(object sender, RoutedEventArgs e)
        {
            Home_Page hp = new Home_Page();
            hp.Show();
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
