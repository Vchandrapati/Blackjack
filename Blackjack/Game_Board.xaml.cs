using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Blackjack.Classes;

namespace Blackjack
{
    public partial class Game_Board
    {
        private readonly ImageSource _cardBack =
            new BitmapImage(new Uri(@$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//black_back.bmp",
                UriKind.RelativeOrAbsolute));

        private readonly Initialiser _init = new Initialiser();
        public ImageSource DealerFlipped;
        public ImageSource DrawnCard;
        public int PA, DA;
        public int PH_Counter, DH_Counter;
        public int PH_Value, DH_Value;
        public Stack<Tuple<string, int>> Shoe;
        public bool stand;

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

            if (PH_Counter == 4)
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
                PH_Counter++;
                PH_Value += 11;
            }
            else if (suit.Length == 2)
            {
                DrawnCard = new BitmapImage(new Uri(
                    @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item1}.bmp",
                    UriKind.RelativeOrAbsolute));
                PH_Counter++;
                PH_Value += 10;
            }
            else
            {
                DrawnCard = new BitmapImage(new Uri(
                    @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item2}{Shoe.Peek().Item1}.bmp",
                    UriKind.RelativeOrAbsolute));
                PH_Counter++;
                PH_Value += value;
            }

            switch (PH_Counter)
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

            Player_Evaluate();

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
                if (DH_Counter == 4)
                {
                    Dealer_Evaluate();
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
                    DH_Counter++;
                    DH_Value += 11;
                }
                else if (suit.Length == 2)
                {
                    DrawnCard = new BitmapImage(new Uri(
                        @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item1}.bmp",
                        UriKind.RelativeOrAbsolute));
                    DH_Counter++;
                    DH_Value += 10;
                }
                else
                {
                    DrawnCard = new BitmapImage(new Uri(
                        @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item2}{Shoe.Peek().Item1}.bmp",
                        UriKind.RelativeOrAbsolute));
                    DH_Counter++;
                    DH_Value += value;
                }

                switch (DH_Counter)
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

            Dealer_Evaluate();
        }

        public void Dealer_Evaluate()
        {
            if (DH_Value > 21 && DA > 0)
            {
                while (DH_Value > 21)
                {
                    if (DA != 0) DH_Value -= 10;
                }
            }
            else if (DH_Value > 21 && DA == 0 || DH_Value > 17 && DH_Value > 21)
                Instantiate_Results(Color.FromRgb(255, 0, 0), "Dealer Bust");     
            else if (DH_Value > 17 && DH_Value <= 21 && DH_Value > PH_Value || DH_Value > 17 && DH_Value == 21 ||
                     DH_Value == 17 && DH_Value > PH_Value || DH_Value > 17 && DH_Value > PH_Value)
                Instantiate_Results(Color.FromRgb(255, 0, 0), "Dealer Wins");
            
            else if (DH_Value == 17 && DH_Value < PH_Value || DH_Value > 17 && DH_Value < PH_Value)
                Instantiate_Results(Color.FromRgb(0, 255, 0), "Player Wins!");
            
            else if (DH_Value == PH_Value)
                Instantiate_Results(Color.FromRgb(255,255, 255), "Push");
            
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            Instantiate_Results(Color.FromRgb(255, 0, 0), "");
            Start();
        }

        public void Player_Evaluate()
        {
            if (PH_Value > 21 && PA > 0)
            {
                while (PH_Value > 21)
                    if (PA != 0)
                        PH_Value -= 10;
            }
            else if (PH_Value > 21 && PA == 0)         
                Instantiate_Results(Color.FromRgb(255, 0, 0), "Player Bust");           
            else if (PH_Value == 21)
                Instantiate_Results(Color.FromRgb(0, 255, 0), "Player Wins!");
            else if (PH_Counter == 4 && PH_Value < 21)
                Stand();
        }

        public void Instantiate_Results(Color color, string result)
        {
            Dealer_Card_1.Source = DealerFlipped;
            btnRestart.Visibility = Visibility.Visible;
            btnHit.Click -= BtnHit_OnClick;
            btnStand.Click -= BtnStand_OnClick;

            txtResults.Height = 50;
            txtResults.Width = 200;
            txtResults.Margin = new Thickness(45, 400, 0, 0);
            txtResults.Text = result;
            txtResults.FontWeight = FontWeight.FromOpenTypeWeight(500);
            txtResults.FontSize = 36;
            txtResults.Background = new SolidColorBrush(Colors.Transparent);
            txtResults.Foreground = new SolidColorBrush(color);

            Results.Children.Remove(txtResults);
            Results.Children.Add(txtResults);
        }

        public void Start()
        {
            btnHit.Click += BtnHit_OnClick;
            btnStand.Click += BtnStand_OnClick;
            btnRestart.Visibility = Visibility.Hidden;
            stand = false;
            Shoe = _init.Initalise(); 
            Player_Card_3.Source = null;
            Player_Card_4.Source = null;
            Dealer_Card_3.Source = null;
            Dealer_Card_4.Source = null;
            PH_Counter = 0;
            DH_Counter = 0;
            PH_Value = 0;
            DH_Value = 0;
            PA = 0;
            DA = 0;


            #region Player Draw

            for (var i = 0; i < 2; i++)
            {
                var suit = Shoe.Peek().Item1;
                var value = Shoe.Peek().Item2;

                if (suit.Contains("A"))
                {
                    DrawnCard = new BitmapImage(new Uri(
                        @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item1}.bmp",
                        UriKind.RelativeOrAbsolute));
                    PH_Counter++;
                    PH_Value += 11;
                }
                else if (suit.Length == 2)
                {
                    DrawnCard = new BitmapImage(new Uri(
                        @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item1}.bmp",
                        UriKind.RelativeOrAbsolute));
                    PH_Counter++;
                }
                else
                {
                    DrawnCard = new BitmapImage(new Uri(
                        @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item2}{Shoe.Peek().Item1}.bmp",
                        UriKind.RelativeOrAbsolute));
                    PH_Counter++;
                }

                switch (PH_Counter)
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

            #region Dealer Draw

            for (var i = 0; i < 2; i++)
            {
                var suit = Shoe.Peek().Item1;
                var value = Shoe.Peek().Item2;

                if (suit.Contains("A"))
                {
                    DrawnCard = new BitmapImage(new Uri(
                        @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item1}.bmp",
                        UriKind.RelativeOrAbsolute));
                    PH_Counter++;
                    PH_Value += 11;
                }

                if (suit.Length == 2)
                {
                    DrawnCard = new BitmapImage(new Uri(
                        @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item1}.bmp",
                        UriKind.RelativeOrAbsolute));
                    DH_Counter++;
                }
                else
                {
                    DrawnCard = new BitmapImage(new Uri(
                        @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{Shoe.Peek().Item2}{Shoe.Peek().Item1}.bmp",
                        UriKind.RelativeOrAbsolute));
                    DH_Counter++;
                }

                switch (DH_Counter)
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