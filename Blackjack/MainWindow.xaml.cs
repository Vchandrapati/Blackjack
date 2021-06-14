﻿using System.Diagnostics.Eventing.Reader;
using static Blackjack.Classes.Variables;
using System.Windows;

namespace Blackjack
{
    public partial class MainWindow : Window
    {
        private readonly MySQLconnect _sqlconnect;

        public MainWindow()
        {
            InitializeComponent();

            _sqlconnect = new MySQLconnect();

            Connection = _sqlconnect.Connect();
        }

        private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
        {
            if (Connection)
            {
                if (txtUser.Text == "" || txtPass.Password == "")
                    MessageBox.Show("Please enter a Username and/or Password");
                else
                {
                    if (_sqlconnect.Login(txtUser.Text, txtPass.Password))
                    {
                        user = txtUser.Text;
                        Home_Page hp = new Home_Page();
                        hp.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Username and/or Password are incorrect");
                        txtUser.Text = "";
                        txtPass.Password = "";
                    }
                }

            }
            else
                MessageBox.Show("Server connection unavailable please continue as a guest");
        }

        private void BtnCreate_OnClick(object sender, RoutedEventArgs e)
        {
            if (Connection)
            {
                CreateAcc ca = new CreateAcc();
                ca.Show();
                Close();
            }
            else
                MessageBox.Show("Server connection unavailable please continue as a guest");
        }

        private void BtnBrooker_OnClick(object sender, RoutedEventArgs e)
        {
            user = "Guest";
            Guest = true;
            Home_Page hp = new Home_Page();
            hp.Show();
            this.Close();
        }
    }
}
