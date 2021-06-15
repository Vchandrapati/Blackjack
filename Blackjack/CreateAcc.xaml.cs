using System.Windows;
using static Blackjack.Classes.Variables;

namespace Blackjack
{
    public partial class CreateAcc : Window
    {
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
