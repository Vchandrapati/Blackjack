using System;
using System.DirectoryServices.ActiveDirectory;
using System.Net;
using System.Net.Mail;
using static Blackjack.Classes.Variables;
using System.Windows;
using System.Windows.Input;

namespace Blackjack
{
    public partial class EmailPage : Window
    {
        public EmailPage()
        {
            InitializeComponent();
        }

        private void btnEmail_OnClick(object sender, RoutedEventArgs e)
        {
            //Checking to see if there is a message
            if (txtMessage.Text == string.Empty)
            {
                MessageBox.Show("Please enter a message");
            }
            else
            {
                try
                {
                    //Initialising mail utils
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    //Setting message
                    message.From = new MailAddress("vikil.chandrapati1@gmail.com");
                    message.To.Add(new MailAddress("vikil.chandrapati1@gmail.com"));
                    message.Subject = "Blackjack Help";
                    message.Body = txtMessage.Text;
                    //SMTP config settings
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("vikil.chandrapati1@gmail.com", "******");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //Sending message
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
