using System;
using System.DirectoryServices.ActiveDirectory;
using System.Net;
using System.Net.Mail;
using static Blackjack.Classes.Variables;
using System.Windows;

namespace Blackjack
{
    public partial class EmailPage : Window
    {
        public EmailPage()
        {
            InitializeComponent();

            _sqlconnect = new MySQLconnect();

            Connection = _sqlconnect.Connect();
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

        private void btnEmail_OnClick(object sender, RoutedEventArgs e)
        {
            if (txtMessage.Text == string.Empty)
            {
                MessageBox.Show("Please enter a message");
            }
            else
            {
                try
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    message.From = new MailAddress("vikil.chandrapati1@gmail.com");
                    message.To.Add(new MailAddress("vikil.chandrapati1@gmail.com"));
                    message.Subject = "Blackjack Help";
                    message.Body = txtMessage.Text;
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("vikil.chandrapati1@gmail.com", "Iloveracing1");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    MessageBox.Show("Sent");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"There was an error sending the email {ex}");
                }
            }
        }

        private void BtnBack_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
