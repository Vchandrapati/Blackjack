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

            for (int i = list.Count - 1; i > 0; i--)
            {
                int temp = Idx.Next(i + 1);
                T card = list[temp];
                list[temp] = list[i];
                list[i] = card;
            }
        }
    }
}
