using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blackjack
{
    public partial class CreateAcc : Window
    {
        private readonly MySQLconnect _sqlconnect;

        public CreateAcc()
        {
            InitializeComponent();

            _sqlconnect = new MySQLconnect();

            _sqlconnect.Connect();
        }

        private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }

        private void BtnCreate_OnClick(object sender, RoutedEventArgs e)
        {
            if (txtUser.Text == "" || txtPass.Password == "")
                MessageBox.Show("Please enter a Username and/or Password");
            MessageBox.Show(_sqlconnect.CreateUser(txtUser.Text, txtPass.Password)
                ? "Account successfully created"
                : "There was an error creating the account");
        }
    }
}
