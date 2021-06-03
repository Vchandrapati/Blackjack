using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Blackjack.Classes
{
    public class Variables
    {
        public static ImageSource DealerFlipped { get; set; }
        public static ImageSource DrawnCard { get; set; }
        public static int PA { get; set; }
        public static int DA { get; set; }
        public static int PlayerCardCount { get; set; }
        public static int DealerCardCount { get; set; }
        public static int PH_Value { get; set; }
        public static int DH_Value { get; set; }
        public static Stack<Tuple<string, int>> Shoe { get; set; }
        public static bool stand { get; set; }
    }
}
