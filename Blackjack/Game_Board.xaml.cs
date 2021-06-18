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
        public static ImageSource CardBack =
            new BitmapImage(new Uri(@$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//black_back.bmp",
                UriKind.RelativeOrAbsolute));

        private readonly Initialiser _init = new Initialiser();
        private readonly Update _update = new Update();
        private static Random _points = new Random();

        public Game_Board()
        {
            InitializeComponent();

            Restart();
            txtMoney.Text = $"Money: ${Money}";
            Pot = 250;

            Dealer_Card_1.Source = CardBack;
        }

        private void BtnHit_OnClick(object sender, RoutedEventArgs e)
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

            PlayerEvaluate();
         }

        private void BtnStand_OnClick(object sender, RoutedEventArgs e)
        {
            Stand = true;
            Dealer_Card_1.Source = DealerFlipped;
            DealerTurn();
        }

        public void DealerTurn()
        {
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

            if(DealerHandValue >= 17)
                DealerEvaluate();
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            Restart();
        }

        public void Restart()
        {
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

            Shoe = _init.Initalise();
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

            DealerEvaluate();
            PlayerEvaluate();
        }

        private void BtnQuit_OnClick(object sender, RoutedEventArgs e)
        {
            if (Connection || !Guest)
                _update.UpdateTable();

            Home_Page hp = new Home_Page();
            hp.Show();
            Close();
        }

        public void DealerEvaluate()
        {
            if (DealerHandValue == 21 && DealerAceCount == 1)
            {
                Dealer_Card_1.Source = DealerFlipped;
                MessageBox.Show($"Dealer won with a Blackjack, Player lost ${Pot}");
                Losses++;
                MoneyLost += Pot;
                Points += _points.Next(250);
                Restart();
            }
            if (DealerHandValue > 21 && DealerAceCount > 0)
            {
                while (DealerHandValue > 21)
                {
                    if (DealerAceCount == 0) continue;
                    DealerHandValue -= 10;
                    DealerAceCount--;
                }
            }
            else if (DealerHandValue > 21 && DealerAceCount == 0 || DealerHandValue > 17 && DealerHandValue > 21)
            {
                Dealer_Card_1.Source = DealerFlipped;
                MessageBox.Show($"Dealer went bust, Player won ${Pot * 2}");
                Money += Pot * 2;
                Wins++;
                MoneyWon += Pot * 2;
                Points += _points.Next(100);
                Restart();
            }
            else if (DealerHandValue > 17 && DealerHandValue == 21 ||
                     DealerHandValue == 17 && DealerHandValue > PlayerHandValue)
            {
                Dealer_Card_1.Source = DealerFlipped;
                MessageBox.Show($"Dealer won, Player lost ${Pot}");
                Losses++;
                MoneyLost += Pot;
                Points += _points.Next(25);
                Restart();
            }
            else if (DealerHandValue == 17 && DealerHandValue < PlayerHandValue ||
                     DealerHandValue > 17 && DealerHandValue < PlayerHandValue)
            {
                Dealer_Card_1.Source = DealerFlipped;
                MessageBox.Show($"Dealer lost, Player won ${Pot * 2}");
                Money += Pot * 2;
                Wins++;
                MoneyWon += Pot * 2;
                Points += _points.Next(100);
                Restart();
            }
            else if(DealerHandValue == PlayerHandValue)
            {
                Dealer_Card_1.Source = DealerFlipped;
                MessageBox.Show($"Push");
                Money += Pot;
                Pushes++;
                Points += _points.Next(50);
                Restart();
            }
        }

        public void PlayerEvaluate()
        {
            if (PlayerHandValue == 21 && PlayerAceCount == 1)
            {
                Dealer_Card_1.Source = DealerFlipped;
                MessageBox.Show($"Player won with a Blackjack and won ${Pot * 2.5}");
                Money += Pot * 2.5;
                Wins++;
                MoneyWon += Pot * 2.5;
                Points += _points.Next(250);
                Restart();
            }

            if (PlayerHandValue > 21 && PlayerAceCount > 0)
            {
                while (PlayerHandValue > 21)
                {
                    if (PlayerAceCount == 0) continue;
                    PlayerHandValue -= 10;
                    PlayerAceCount--;
                }
            }
            else if (PlayerHandValue > 21 && PlayerAceCount == 0 || DealerHandValue == PlayerHandValue)
            {
                Dealer_Card_1.Source = DealerFlipped;
                MessageBox.Show($"Player went bust and lost ${Pot}");
                Losses++;
                MoneyLost += Pot;
                Points += _points.Next(25);
                Restart();
            }
            else if (PlayerHandValue == 21)
            {
                Dealer_Card_1.Source = DealerFlipped;
                MessageBox.Show($"Player won ${Pot * 2}");
                Money += Pot * 2;
                Wins++;
                MoneyWon += Pot * 2;
                Points += _points.Next(100);
                Restart();
            }
            else if (DealerHandValue == PlayerHandValue)
            {
                Dealer_Card_1.Source = DealerFlipped;
                MessageBox.Show($"Push");
                Money += Pot;
                Pushes++;
                Points += _points.Next(50);
                Restart();
            }
            else if (PlayerCardCount == 4 && PlayerHandValue < 21)
                DealerTurn();
        }
    }
}