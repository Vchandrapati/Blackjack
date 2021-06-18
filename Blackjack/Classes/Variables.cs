using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Blackjack.Classes
{
    public class Variables
    {
        public static bool Once { get; set; }
        public static ImageSource DealerFlipped { get; set; }
        public static ImageSource DrawnCard { get; set; }
        public static int PlayerAceCount { get; set; }
        public static int DealerAceCount { get; set; }
        public static int PlayerCardCount { get; set; }
        public static int DealerCardCount { get; set; }
        public static int PlayerHandValue { get; set; }
        public static dynamic Pot { get; set; }
        public static bool Guest { get; set; }
        public static bool Push { get; set; }
        public static bool Connection { get; set; }
        public static int DealerHandValue { get; set; }
        public static string User { get; set; }
        public static Stack<Tuple<string, int>> Shoe { get; set; }
        public static bool Stand { get; set; }
        public static dynamic Wins { get; set; }
        public static dynamic Losses { get; set; }
        public static dynamic GamesPlayed { get; set; }
        public static dynamic MoneyWon { get; set; }
        public static dynamic MoneyLost { get; set; }
        public static dynamic Pushes { get; set; }
        public static dynamic CardsDrawn { get; set; }
        public static dynamic Money { get; set; }
        public static dynamic Level { get; set; }
        public static dynamic Points { get; set; }
        public static MySQLconnect _sqlconnect { get; set; }
    }
}
