﻿using System;
using System.Windows;
using static Blackjack.Classes.Variables;

namespace Blackjack
{
    public partial class CreateAcc : Window
    {
        MainWindow mw = new MainWindow();
        public CreateAcc()
        {
            InitializeComponent();
        }

        private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
        {
            //Logging in
            mw.Show();
            Close();
        }

        private void BtnCreate_OnClick(object sender, RoutedEventArgs e)
        {
            //Checking to see if both fields have values
            if (txtUser.Text == string.Empty || txtPass.Password == string.Empty)
                MessageBox.Show("Please enter a Username and/or Password");
            else
            {
                //Creating the account
                MessageBox.Show(_sqlconnect.CreateUser(txtUser.Text, txtPass.Password)
                    ? "Account successfully created"
                    : "There was an error creating the account");
                mw.Show();
            }
        }
    }
}
