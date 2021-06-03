using System;
using System.Windows;
using System.Windows.Controls;
using static Blackjack.Classes.Evaluator;
using static Blackjack.Classes.Variables;
using static Blackjack.Classes.
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Blackjack.Classes;

namespace Blackjack
{
    public partial class Game_Board
    { 
        readonly ImageSource _cardBack =
            new BitmapImage(new Uri(@$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//black_back.bmp",
                UriKind.RelativeOrAbsolute));

        private readonly Initialiser _init = new Initialiser();

        TextBlock txtResults = new TextBlock();

        public Game_Board()
        {
            InitializeComponent();
            Dealer_Card_1.Source = _cardBack;

            Start();
        }

        private void BtnHit_OnClick(object sender, RoutedEventArgs e)
        {
            var suit = Shoe.Peek().Item1;
            var value = Shoe.Peek().Item2;

            if (PlayerCardCount == 4)
            {
                stand = true;
                Stand();
                MessageBox.Show("Reached card draw limit");
                return;
            }

            if (suit.Contains("A"))
            {
                DrawnCard = new BitmapImage(new Uri(
                    @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item1}.bmp",
                    UriKind.RelativeOrAbsolute));
                PlayerCardCount++;
                PH_Value += 11;
            }
            else if (suit.Length == 2)
            {
                DrawnCard = new BitmapImage(new Uri(
                    @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item1}.bmp",
                    UriKind.RelativeOrAbsolute));
                PlayerCardCount++;
                PH_Value += 10;
            }
            else
            {
                DrawnCard = new BitmapImage(new Uri(
                    @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item2}{Shoe.Peek().Item1}.bmp",
                    UriKind.RelativeOrAbsolute));
                PlayerCardCount++;
                PH_Value += value;
            }

            switch (PlayerCardCount)
            {
                case 3:
                    Player_Card_3.Source = DrawnCard;
                    break;
                case 4:
                    Player_Card_4.Source = DrawnCard;
                    break;
                default:
                    MessageBox.Show("Error in draw");
                    break;
            }

            PlayerEvaluate();

            Shoe.Pop();
        }

        private void BtnStand_OnClick(object sender, RoutedEventArgs e)
        {
            stand = true;
            Stand();
        }

        public void Stand()
        {
            Dealer_Card_1.Source = DealerFlipped;

            while (DH_Value < 17 && stand)
            {
                if (DealerCardCount == 4)
                {
                    DealerEvaluate();
                    Start();
                    return;
                }

                var suit = Shoe.Peek().Item1;
                var value = Shoe.Peek().Item2;

                if (suit.Contains("A"))
                {
                    DrawnCard = new BitmapImage(new Uri(
                        @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item1}.bmp",
                        UriKind.RelativeOrAbsolute));
                    DealerCardCount++;
                    DH_Value += 11;
                }
                else if (suit.Length == 2)
                {
                    DrawnCard = new BitmapImage(new Uri(
                        @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item1}.bmp",
                        UriKind.RelativeOrAbsolute));
                    DealerCardCount++;
                    DH_Value += 10;
                }
                else
                {
                    DrawnCard = new BitmapImage(new Uri(
                        @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item2}{Shoe.Peek().Item1}.bmp",
                        UriKind.RelativeOrAbsolute));
                    DealerCardCount++;
                    DH_Value += value;
                }

                switch (DealerCardCount)
                {
                    case 3:
                        Dealer_Card_3.Source = DrawnCard;
                        break;
                    case 4:
                        Dealer_Card_4.Source = DrawnCard;
                        break;
                    default:
                        MessageBox.Show("Error in draw");
                        break;
                }
            }

            DealerEvaluate();
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }

        public void Start()
        {
            btnRestart.Visibility = Visibility.Hidden;
            stand = false;
            Shoe = _init.Initalise(); 
            Player_Card_3.Source = null;
            Player_Card_4.Source = null;
            Dealer_Card_3.Source = null;
            Dealer_Card_4.Source = null;
            PlayerCardCount = 0;
            DealerCardCount = 0;
            PH_Value = 0;
            DH_Value = 0;
            PA = 0;
            DA = 0;


            #region PlayerEvaluate Draw

            for (var i = 0; i < 2; i++)
            {
                var suit = Shoe.Peek().Item1;
                var value = Shoe.Peek().Item2;

                if (suit.Contains("A"))
                {
                    DrawnCard = new BitmapImage(new Uri(
                        @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item1}.bmp",
                        UriKind.RelativeOrAbsolute));
                    PlayerCardCount++;
                    PH_Value += 11;
                }
                else if (suit.Length == 2)
                {
                    DrawnCard = new BitmapImage(new Uri(
                        @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item1}.bmp",
                        UriKind.RelativeOrAbsolute));
                    PlayerCardCount++;
                }
                else
                {
                    DrawnCard = new BitmapImage(new Uri(
                        @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item2}{Shoe.Peek().Item1}.bmp",
                        UriKind.RelativeOrAbsolute));
                    PlayerCardCount++;
                }

                switch (PlayerCardCount)
                {
                    case 1:
                        Player_Card_1.Source = DrawnCard;
                        break;
                    case 2:
                        Player_Card_2.Source = DrawnCard;
                        break;
                    default:
                        MessageBox.Show("error in draw");
                        break;
                }

                PH_Value += value;

                Shoe.Pop();
            }

            #endregion

            #region DealerEvaluate Draw

            for (var i = 0; i < 2; i++)
            {
                var suit = Shoe.Peek().Item1;
                var value = Shoe.Peek().Item2;

                if (suit.Contains("A"))
                {
                    DrawnCard = new BitmapImage(new Uri(
                        @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item1}.bmp",
                        UriKind.RelativeOrAbsolute));
                    PlayerCardCount++;
                    PH_Value += 11;
                }

                if (suit.Length == 2)
                {
                    DrawnCard = new BitmapImage(new Uri(
                        @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item1}.bmp",
                        UriKind.RelativeOrAbsolute));
                    DealerCardCount++;
                }
                else
                {
                    DrawnCard = new BitmapImage(new Uri(
                        @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item2}{Shoe.Peek().Item1}.bmp",
                        UriKind.RelativeOrAbsolute));
                    DealerCardCount++;
                }

                switch (DealerCardCount)
                {
                    case 1:
                        Dealer_Card_1.Source = _cardBack;
                        DealerFlipped = DrawnCard;
                        break;
                    case 2:
                        Dealer_Card_2.Source = DrawnCard;
                        break;
                    default:
                        MessageBox.Show("error in draw");
                        break;
                }

                DH_Value += value;

                Shoe.Pop();
            }

            #endregion
        }
    }
}