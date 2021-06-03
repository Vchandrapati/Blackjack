using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using static Blackjack.Game_Board;
using static Blackjack.Classes.Variables;

namespace Blackjack
{
    public static class Draw
    {
        static Game_Board gb = new Game_Board();
        public static void PlayerDraw()
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
                    gb.Player_Card_1.Source = DrawnCard;
                    break;
                case 2:
                    gb.Player_Card_2.Source = DrawnCard;
                    break;
                case 3:
                    gb.Player_Card_3.Source = DrawnCard;
                    break;
                case 4:
                    gb.Player_Card_4.Source = DrawnCard;
                    break;
                default:
                    MessageBox.Show("error in draw");
                    break;
            }

            Shoe.Pop();
        }

        public static void DealerDraw()
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
                    gb.Dealer_Card_1.Source = gb._cardBack;
                    DealerFlipped = DrawnCard;
                    break;
                case 2:
                    gb.Dealer_Card_2.Source = DrawnCard;
                    break;
                case 3:
                    gb.Dealer_Card_3.Source = DrawnCard;
                    break;
                case 4:
                    gb.Dealer_Card_4.Source = DrawnCard;
                    break;
                default:
                    MessageBox.Show("error in draw");
                    break;
            }

            Shoe.Pop();
        }
    }
}
