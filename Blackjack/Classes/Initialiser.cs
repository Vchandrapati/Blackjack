using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Blackjack.Classes
{
    public class Initialiser
    {
        public Stack<Tuple<string, int>> Initalise()
        {
            //The deck itself arranged into suits into a Stack
            #region Deck
            Stack<Tuple<string, int>> Shoe = new Stack<Tuple<string, int>>();
            Shoe.Push(Tuple.Create("AS", 11));
            Shoe.Push(Tuple.Create("AC", 11));
            Shoe.Push(Tuple.Create("AH", 11));
            Shoe.Push(Tuple.Create("AD", 11));

            Shoe.Push(Tuple.Create("S", 2));
            Shoe.Push(Tuple.Create("C", 2));
            Shoe.Push(Tuple.Create("H", 2));
            Shoe.Push(Tuple.Create("D", 2));

            Shoe.Push(Tuple.Create("S", 3));
            Shoe.Push(Tuple.Create("C", 3));
            Shoe.Push(Tuple.Create("H", 3));
            Shoe.Push(Tuple.Create("D", 3));

            Shoe.Push(Tuple.Create("S", 4));
            Shoe.Push(Tuple.Create("C", 4));
            Shoe.Push(Tuple.Create("H", 4));
            Shoe.Push(Tuple.Create("D", 4));

            Shoe.Push(Tuple.Create("S", 5));
            Shoe.Push(Tuple.Create("C", 5));
            Shoe.Push(Tuple.Create("H", 5));
            Shoe.Push(Tuple.Create("D", 5));

            Shoe.Push(Tuple.Create("S", 6));
            Shoe.Push(Tuple.Create("C", 6));
            Shoe.Push(Tuple.Create("H", 6));
            Shoe.Push(Tuple.Create("D", 6));

            Shoe.Push(Tuple.Create("S", 7));
            Shoe.Push(Tuple.Create("C", 7));
            Shoe.Push(Tuple.Create("H", 7));
            Shoe.Push(Tuple.Create("D", 7));

            Shoe.Push(Tuple.Create("S", 8));
            Shoe.Push(Tuple.Create("C", 8));
            Shoe.Push(Tuple.Create("H", 8));
            Shoe.Push(Tuple.Create("D", 8));

            Shoe.Push(Tuple.Create("S", 9));
            Shoe.Push(Tuple.Create("C", 9));
            Shoe.Push(Tuple.Create("H", 9));
            Shoe.Push(Tuple.Create("D", 9));

            Shoe.Push(Tuple.Create("S", 10));
            Shoe.Push(Tuple.Create("C", 10));
            Shoe.Push(Tuple.Create("H", 10));
            Shoe.Push(Tuple.Create("D", 10));

            Shoe.Push(Tuple.Create("JS", 10));
            Shoe.Push(Tuple.Create("JC", 10));
            Shoe.Push(Tuple.Create("JH", 10));
            Shoe.Push(Tuple.Create("JD", 10));

            Shoe.Push(Tuple.Create("QS", 10));
            Shoe.Push(Tuple.Create("QC", 10));
            Shoe.Push(Tuple.Create("QH", 10));
            Shoe.Push(Tuple.Create("QD", 10));

            Shoe.Push(Tuple.Create("KS", 10));
            Shoe.Push(Tuple.Create("KC", 10));
            Shoe.Push(Tuple.Create("KH", 10));
            Shoe.Push(Tuple.Create("KD", 10));
            #endregion

            //Creating a temp deck to shuffle
            var decktmp = Shoe.ToArray();
            Shoe.Clear();
            //Shuffling
            decktmp.Shuffle();
            
            //Transferring array back to stack
            foreach (var card in decktmp)
                Shoe.Push(card);

            return Shoe;
        }
    }
}
