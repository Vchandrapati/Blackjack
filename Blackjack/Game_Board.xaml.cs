using System;
using System.Windows;
using static Blackjack.Classes.Evaluator;
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

        public Game_Board()
        {
            InitializeComponent();
            Dealer_Card_1.Source = CardBack;
            Start();
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
                    break;
                case 4:
                    Player_Card_4.Source = DrawnCard;
                    break;
                default:
                    MessageBox.Show("Maximum hand size reached");
                    break;
            }

            if (PlayerEvaluate())
            {
                Dealer_Card_1.Source = DealerFlipped;
                Start();
            }
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
                        break;
                    case 4:
                        Dealer_Card_4.Source = DrawnCard;
                        break;
                    default:
                        MessageBox.Show("Maximum hand size reached");
                        break;
                }

            if (DealerEvaluate())
            {
                Dealer_Card_1.Source = DealerFlipped;
                Start();
            }
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }

        public void Start()
        {
            btnRestart.Visibility = Visibility.Hidden;
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

            if (DealerEvaluate())
            {
                Dealer_Card_1.Source = DealerFlipped;
                Start();
            }

            if (PlayerEvaluate())
            {
                Dealer_Card_1.Source = DealerFlipped;
                Start();
            }
        }
    }
}