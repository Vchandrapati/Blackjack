using System;
using System.Windows;
using static Blackjack.Classes.Variables;
using MySql.Data.MySqlClient;

namespace Blackjack
{
    public class MySQLconnect
    {
        private const string Server = "us-cdbr-east-03.cleardb.com";
        private const string Database = "heroku_9286e40e3bf28d9";
        private const string Uid = "b40c5e8fda029a";
        private const string Password = "e0061431";
        private const string Port = "3306";
        private MySqlConnection _connection;
        public static string connectionstring = "";

        public MySQLconnect()
        {
            Init();
        }

        private void Init()
        {
            connectionstring = $"Server={Server}; Port={Port}; Database={Database}; Uid={Uid}; Pwd={Password};";
            _connection = new MySqlConnection(connectionstring);
        }

        public bool Connect()
        {
            try
            {
                _connection.Open();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }

            return true;
        }
        public bool Close()
        {
            try
            {
                _connection.Close();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }

            return true;
        }
        public bool CreateUser(string user, string password)
        {
            var query = $"INSERT INTO user_data (Username, Password, Level, Points, Money, Wins, Losses, Games_Played, Money_Won, Money_Lost, Pushes, Cards_Drawn) VALUES ('{user}', '{password}', 0, 0, 10500, 1, 0, 1, 0, 0, 0, 0);";
            try
            {
                var cmd = new MySqlCommand(query, _connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("There was an error connecting to the server");
            }
            return false;
        }

        public bool Login(string user, string password)
        {
            var query = $"SELECT EXISTS(SELECT * FROM user_data WHERE username = '{user}' AND password = '{password}');";
            var cmd = new MySqlCommand(query, _connection);

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Error connecting to server");
            }

            return false;
        }

        public void Update()
        {
            var query = $"UPDATE user_data SET Level={Math.Floor(Points / 1000m) - 2m}, Points={Points}, Money={Money}, wins={Wins}, Losses={Losses}, Games_Played={GamesPlayed}, Money_Won={MoneyWon}, Money_Lost={MoneyLost}, Pushes={Pushes}, Cards_Drawn={CardsDrawn}  WHERE username = '{User}';";
            var cmd = new MySqlCommand(query, _connection);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Error connecting to server");
            }
        }

        public int Info(string user, string info)
        {
            var query = $"SELECT {info} FROM user_data WHERE username = '{user}';";
            var cmd = new MySqlCommand(query, _connection);

            cmd.ExecuteNonQuery();
            var read = cmd.ExecuteReader();

            while (read.Read())
            {
                int output = int.Parse(read[0].ToString() ?? string.Empty);
                read.Close();
                return output;
            }

            read.Close();
            return 0;
        }
    }
}
