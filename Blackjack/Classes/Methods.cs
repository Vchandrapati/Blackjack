using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack.Classes
{
    public static class Methods
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random Idx = new Random();

            //Iterate through list
            for (int i = list.Count - 1; i > 0; i--)
            {
                //Get a random index 
                int temp = Idx.Next(i + 1);

                //Swapping the positions of the cards
                T card = list[temp];
                list[temp] = list[i];
                list[i] = card;
            }
        }
    }
}
