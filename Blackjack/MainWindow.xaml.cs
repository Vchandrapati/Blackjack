using System.Windows;

namespace Blackjack
{
    public partial class MainWindow : Window
    {
        public static bool guest;
        public static string user = "";
        private readonly MySQLconnect _sqlconnect;

        public MainWindow()
        {
            InitializeComponent();

            _sqlconnect = new MySQLconnect();

            _sqlconnect.Connect();
        }

        private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
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
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username and/or Password are incorrect");
                    txtUser.Text = "";
                    txtPass.Password = "";
                }
            }
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
