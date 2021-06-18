using System;
using System.Windows.Media.Imaging;
using static Blackjack.Classes.Variables;

namespace Blackjack.Classes
{
    public static class Draw
    {
        public static int PlayerDraw()
        {
            //Gets the suit of the card 
            var suit = Shoe.Peek().Item1;
            //Gets the value of the card
            var value = Shoe.Peek().Item2;

            //Check if the card is an Ace
            if (suit.Contains("A"))
            {
                DrawnCard = new BitmapImage(new Uri(
                    @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{suit}.bmp",
                    UriKind.RelativeOrAbsolute));
                PlayerAceCount++;
                PlayerCardCount++;
                PlayerHandValue += 11;
            }
            //Check to see if it is a face card
            else if (suit.Length == 2)
            {
                DrawnCard = new BitmapImage(new Uri(
                    @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{suit}.bmp",
                    UriKind.RelativeOrAbsolute));
                PlayerCardCount++;
                PlayerHandValue += 10;
            }
            //Any other card
            else
            {
                DrawnCard = new BitmapImage(new Uri(
                    @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{value}{suit}.bmp",
                    UriKind.RelativeOrAbsolute));
                PlayerCardCount++;
                PlayerHandValue += value;
            }

            CardsDrawn++;
            //Remove card from stack
            Shoe.Pop();
            return PlayerCardCount;
        }

        public static int DealerDraw()
        {
            //Gets the suit of the card 
            var suit = Shoe.Peek().Item1;
            //Gets the value of the card
            var value = Shoe.Peek().Item2;

            //Check if the card is an Ace
            if (suit.Contains("A"))
            {
                DrawnCard = new BitmapImage(new Uri(
                    @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{suit}.bmp",
                    UriKind.RelativeOrAbsolute));
                DealerAceCount++;
                DealerCardCount++;
                DealerHandValue += 11;
            }
            //Check to see if it is a face card
            else if (suit.Length == 2)
            {
                DrawnCard = new BitmapImage(new Uri(
                    @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{suit}.bmp",
                    UriKind.RelativeOrAbsolute));
                DealerCardCount++;
                DealerHandValue += 10;
            }
            //Any other card
            else
            {
                DrawnCard = new BitmapImage(new Uri(
                    @$"{AppDomain.CurrentDomain.BaseDirectory}//Cards//{value}{suit}.bmp",
                    UriKind.RelativeOrAbsolute));
                DealerCardCount++;
                DealerHandValue += value;
            }

            CardsDrawn++;
            //Remove card from stack
            Shoe.Pop();
            return DealerCardCount;
        }
    }
}
