using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static Blackjack.MySQLconnect;
using MySql.Data.MySqlClient;

namespace Blackjack
{
    public partial class Leaderboards : Window
    {
        public static bool guest;
        public static string user = "";
        private readonly MySQLconnect _sqlconnect;

        public Leaderboards()
        {
            InitializeComponent();

            _sqlconnect = new MySQLconnect();

            _sqlconnect.Connect();

            DataTable lb = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                conn.Open();
                string query = "SELECT Username, Level, Points, Money FROM user_data";
                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                    da.Fill(lb);
            }

            foreach (DataGridColumn column in dgLeaders.Columns)
                column.Width = 500;

            dgLeaders.ItemsSource = lb.DefaultView;
        }

        private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCreate_OnClick(object sender, RoutedEventArgs e)
        {
            CreateAcc ca = new CreateAcc();
            ca.Show();
            this.Close();
        }

        private void BtnBrooker_OnClick(object sender, RoutedEventArgs e)
        {
            user = "Guest";
            guest = true;
            Home_Page hp = new Home_Page();
            hp.Show();
            this.Close();
        }
    }
}
