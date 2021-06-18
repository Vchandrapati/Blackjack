using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    }
}
