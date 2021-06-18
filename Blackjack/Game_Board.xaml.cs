using System;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Blackjack.Classes.Draw;
using static Blackjack.Classes.Variables;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Blackjack.Classes;

namespace Blackjack
{
    public partial class Game_Board
    { 
        //Setting the first card of the dealer to be face down
        public static ImageSource CardBack =
            new BitmapImage(new Uri(@$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//black_back.bmp",
                UriKind.RelativeOrAbsolute));

        //Setting local variables
        private readonly Initialiser _init = new Initialiser();
        private readonly Update _update = new Update();
        private static Random _points = new Random();

        public Game_Board()
        {
            InitializeComponent();

            //Starting game
            Restart();
            txtMoney.Text = $"Money: ${Money}";
            Pot = 250;

            Dealer_Card_1.Source = CardBack;
        }

        private void BtnHit_OnClick(object sender, RoutedEventArgs e)
        {
            //Return from the draw class to set the image of the card
            switch (PlayerDraw())
            {
                case 1:
                    Player_Card_1.Source = DrawnCard;
                    break;
                case 2:
                    Player_Card_2.Source = DrawnCard;
                    break;
                case 3:
                    Player_Card_3.Source = DrawnCard;
                    PlayerEvaluate();
                    break;
                case 4:
                    Player_Card_4.Source = DrawnCard;
                    PlayerEvaluate();
                    break;
                default:
                    MessageBox.Show("Maximum hand size reached");
                    break;
            }

            //Hand evaluation
            PlayerEvaluate();
         }

        private void BtnStand_OnClick(object sender, RoutedEventArgs e)
        {
            //Switching to dealer's turn
            Stand = true;
            //Revealing dealer's face down card
            Dealer_Card_1.Source = DealerFlipped;
            DealerTurn();
        }

        public void DealerTurn()
        {
            //If the dealer's hand value is less then 17 they must draw otherwise stand
            while (DealerHandValue < 17 && Stand)
                switch (DealerDraw())
                {
                    case 1:
                        Dealer_Card_1.Source = CardBack;
                        DealerFlipped = DrawnCard;
                        break;
                    case 2:
                        Dealer_Card_2.Source = DrawnCard;
                        break;
                    case 3:
                        Dealer_Card_3.Source = DrawnCard;
                        DealerEvaluate();
                        break;
                    case 4:
                        Dealer_Card_4.Source = DrawnCard;
                        DealerEvaluate();
                        break;
                    default:
                        MessageBox.Show("Maximum hand size reached");
                        break;
                }

            //Standing on 17 or above
            if(DealerHandValue >= 17)
                DealerEvaluate();
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            Restart();
        }

        public void Restart()
        {
            //Resetting local variables to defaults
            Push = false;
            GamesPlayed++;
            Stand = false;
            Shoe = _init.Initalise(); 
            Player_Card_3.Source = null;
            Player_Card_4.Source = null;
            Dealer_Card_3.Source = null;
            Dealer_Card_4.Source = null;
            PlayerCardCount = 0;
            DealerCardCount = 0;
            PlayerHandValue = 0;
            DealerHandValue = 0;
            PlayerAceCount = 0;
            DealerAceCount = 0;
            Pot = 250;
            txtPot.Text = "Pot: $250";
            txtMoney.Text = $"Money: ${Money}";
            if (Money > 250)
                Money -= 250;
            else
            {
                MessageBox.Show("You do not have the funds to play another round thanks for playing");
                if (Connection || !Guest)
                    _update.UpdateTable();

                Home_Page hp = new Home_Page();
                hp.Show();
                Close();
            }

            //Shuffling the deck
            Shoe = _init.Initalise();
            //Drawing cards for both players
            for (var i = 0; i < 2; i++)
            {
                switch (PlayerDraw())
                {
                    case 1:
                        Player_Card_1.Source = DrawnCard;
                        break;
                    case 2:
                        Player_Card_2.Source = DrawnCard;
                        break;
                    case 3:
                        Player_Card_3.Source = DrawnCard;
                        break;
                    case 4:
                        Player_Card_4.Source = DrawnCard;
                        DealerTurn();
                        break;
                    default:
                        MessageBox.Show("Maximum hand size reached");
                        break;
                }

                switch (DealerDraw())
                {
                    case 1:
                        Dealer_Card_1.Source = CardBack;
                        DealerFlipped = DrawnCard;
                        break;
                    case 2:
                        Dealer_Card_2.Source = DrawnCard;
                        break;
                    case 3:
                        Dealer_Card_3.Source = DrawnCard;
                        break;
                    case 4:
                        Dealer_Card_4.Source = DrawnCard;
                        break;
                    default:
                        MessageBox.Show("Maximum hand size reached");
                        break;
                }
            }

            //Evaluating Hands
            DealerEvaluate();
            PlayerEvaluate();
        }

        private void BtnQuit_OnClick(object sender, RoutedEventArgs e)
        {
            //If there is a connection to the server or if the player is not a guest update server data
            if (Connection || !Guest)
                _update.UpdateTable();

            //Back to home page
            Home_Page hp = new Home_Page();
            hp.Show();
            Close();
        }

        public void DealerEvaluate()
        {
            //Blackjack Case
            if (DealerHandValue == 21 && DealerAceCount == 1)
            {
                Dealer_Card_1.Source = DealerFlipped;
                MessageBox.Show($"Dealer won with a Blackjack, Player lost ${Pot}");
                Losses++;
                MoneyLost += Pot;
                //randomising player xp
                Points += _points.Next(250);
                Restart();
            }
            //Making sure to account for Ace's
            if (DealerHandValue > 21 && DealerAceCount > 0)
            {
                while (DealerHandValue > 21)
                {
                    if (DealerAceCount == 0) continue;
                    DealerHandValue -= 10;
                    DealerAceCount--;
                }
            }
            //Checking to see if the dealer loses
            else if (DealerHandValue > 21 && DealerAceCount == 0 || DealerHandValue > 17 && DealerHandValue > 21)
            {
                Dealer_Card_1.Source = DealerFlipped;
                MessageBox.Show($"Dealer went bust, Player won ${Pot * 2}");
                Money += Pot * 2;
                Wins++;
                MoneyWon += Pot * 2;
                //randomising player xp
                Points += _points.Next(100);
                Restart();
            }
            //Checking to see if the player loses
            else if (DealerHandValue > 17 && DealerHandValue == 21 ||
                     DealerHandValue == 17 && DealerHandValue > PlayerHandValue)
            {
                Dealer_Card_1.Source = DealerFlipped;
                MessageBox.Show($"Dealer won, Player lost ${Pot}");
                Losses++;
                MoneyLost += Pot;
                //randomising player xp
                Points += _points.Next(25);
                Restart();
            }
            //Checking to see if the Player Wins
            else if (DealerHandValue == 17 && DealerHandValue < PlayerHandValue ||
                     DealerHandValue > 17 && DealerHandValue < PlayerHandValue)
            {
                Dealer_Card_1.Source = DealerFlipped;
                MessageBox.Show($"Dealer lost, Player won ${Pot * 2}");
                Money += Pot * 2;
                Wins++;
                MoneyWon += Pot * 2;
                //randomising player xp
                Points += _points.Next(100);
                Restart();
            }
            //Checking for push
            else if(DealerHandValue == PlayerHandValue)
            {
                Dealer_Card_1.Source = DealerFlipped;
                MessageBox.Show($"Push");
                Money += Pot;
                Pushes++;
                //randomising player xp
                Points += _points.Next(50);
                Restart();
            }
        }

        public void PlayerEvaluate()
        {
            //Checking for Blackjack
            if (PlayerHandValue == 21 && PlayerAceCount == 1)
            {
                Dealer_Card_1.Source = DealerFlipped;
                MessageBox.Show($"Player won with a Blackjack and won ${Pot * 2.5}");
                Money += Pot * 2.5;
                Wins++;
                MoneyWon += Pot * 2.5;
                //randomising player xp
                Points += _points.Next(250);
                Restart();
            }
            //Accounting for Ace's
            if (PlayerHandValue > 21 && PlayerAceCount > 0)
            {
                while (PlayerHandValue > 21)
                {
                    if (PlayerAceCount == 0) continue;
                    PlayerHandValue -= 10;
                    PlayerAceCount--;
                }
            }
            //Checking to see if the Dealer wins
            else if (PlayerHandValue > 21 && PlayerAceCount == 0 || DealerHandValue == PlayerHandValue)
            {
                Dealer_Card_1.Source = DealerFlipped;
                MessageBox.Show($"Player went bust and lost ${Pot}");
                Losses++;
                MoneyLost += Pot;
                //randomising player xp
                Points += _points.Next(25);
                Restart();
            }
            //Checking to see if the Player Wins
            else if (PlayerHandValue == 21)
            {
                Dealer_Card_1.Source = DealerFlipped;
                MessageBox.Show($"Player won ${Pot * 2}");
                Money += Pot * 2;
                Wins++;
                MoneyWon += Pot * 2;
                //randomising player xp
                Points += _points.Next(100);
                Restart();
            }
            //Checking for push
            else if (DealerHandValue == PlayerHandValue)
            {
                Dealer_Card_1.Source = DealerFlipped;
                MessageBox.Show($"Push");
                Money += Pot;
                Pushes++;
                //randomising player xp
                Points += _points.Next(50);
                Restart();
            }
            else if (PlayerCardCount == 4 && PlayerHandValue < 21)
                DealerTurn();
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