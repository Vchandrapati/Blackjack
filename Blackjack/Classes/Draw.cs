using System;
using System.Windows.Media.Imaging;
using static Blackjack.Classes.Variables;

namespace Blackjack.Classes
{
    public static class Draw
    {
        public static int PlayerDraw()
        {
            var suit = Shoe.Peek().Item1;
            var value = Shoe.Peek().Item2;

            if (suit.Contains("A"))
            {
                DrawnCard = new BitmapImage(new Uri(
                    @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{suit}.bmp",
                    UriKind.RelativeOrAbsolute));
                PlayerCardCount++;
                PlayerHandValue += 11;
            }
            else if (suit.Length == 2)
            {
                DrawnCard = new BitmapImage(new Uri(
                    @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{suit}.bmp",
                    UriKind.RelativeOrAbsolute));
                PlayerCardCount++;
            }
            else
            {
                DrawnCard = new BitmapImage(new Uri(
                    @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{value}{suit}.bmp",
                    UriKind.RelativeOrAbsolute));
                PlayerCardCount++;
            }

            Shoe.Pop();
            return PlayerCardCount;
        }

        public static int DealerDraw()
        {
            var suit = Shoe.Peek().Item1;
            var value = Shoe.Peek().Item2;

            if (suit.Contains("A"))
            {
                DrawnCard = new BitmapImage(new Uri(
                    @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{suit}.bmp",
                    UriKind.RelativeOrAbsolute));
                PlayerCardCount++;
                PlayerHandValue += 11;
            }

            if (suit.Length == 2)
            {
                DrawnCard = new BitmapImage(new Uri(
                    @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{suit}.bmp",
                    UriKind.RelativeOrAbsolute));
                DealerCardCount++;
            }
            else
            {
                DrawnCard = new BitmapImage(new Uri(
                    @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{value}{suit}.bmp",
                    UriKind.RelativeOrAbsolute));
                DealerCardCount++;
            }

            Shoe.Pop();
            return DealerCardCount;
        }
    }
}
