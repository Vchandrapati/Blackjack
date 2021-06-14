using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Blackjack.Classes
{
    public class Variables
    {
        public static ImageSource DealerFlipped { get; set; }
        public static ImageSource DrawnCard { get; set; }
        public static int PlayerAceCount { get; set; }
        public static int DealerAceCount { get; set; }
        public static int PlayerCardCount { get; set; }
        public static int DealerCardCount { get; set; }
        public static int PlayerHandValue { get; set; }
        public static bool Guest { get; set; }
        public static bool Connection { get; set; }
        public static int DealerHandValue { get; set; }
        public static string user { get; set; }
        public static Stack<Tuple<string, int>> Shoe { get; set; }
        public static bool Stand { get; set; }
        public static List<dynamic> PlayerHand { get; set; }
        public static List<dynamic> DealerHand { get; set; }
    }
}
