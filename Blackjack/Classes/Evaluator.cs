using System.Windows;
using static Blackjack.Classes.Variables;

namespace Blackjack.Classes
{
    public static class Evaluator
    {
        private static readonly Game_Board _gb = new Game_Board();

        public static bool DealerEvaluate()
        {
            if (DealerHandValue > 21 && DealerAceCount > 0)
            {
                while (DealerHandValue > 21)
                {
                    if (DealerAceCount != 0)
                    {
                        DealerHandValue -= 10;
                        DealerAceCount--;
                    }

                }
            }
            else if (DealerHandValue > 21 && DealerAceCount == 0 || DealerHandValue > 17 && DealerHandValue > 21)
            {
                MessageBox.Show("Dealer went bust");
                return true;
            }
            else if (DealerHandValue > 17 && DealerHandValue <= 21 && DealerHandValue > PlayerHandValue ||
                     DealerHandValue > 17 && DealerHandValue == 21 ||
                     DealerHandValue == 17 && DealerHandValue > PlayerHandValue ||
                     DealerHandValue > 17 && DealerHandValue > PlayerHandValue)
            {
                MessageBox.Show("Dealer won");
                return true;
            }
            else if (DealerHandValue == 17 && DealerHandValue < PlayerHandValue ||
                     DealerHandValue > 17 && DealerHandValue < PlayerHandValue)
            {

                MessageBox.Show("Dealer lost");
                return true;
            }
            else if (DealerHandValue == PlayerHandValue)
            {
                MessageBox.Show("Push");
                return true;
            }

            return false;
        }

        public static bool PlayerEvaluate()
        {
            if (PlayerHandValue > 21 && PlayerAceCount > 0)
            {
                while (PlayerHandValue > 21)
                {
                    if (PlayerAceCount != 0)
                    {
                        PlayerHandValue -= 10;
                        PlayerAceCount--;
                    }

                }
            }
            else if (PlayerHandValue > 21 && PlayerAceCount == 0)
            {
                MessageBox.Show("Player went bust and lost $x");
                return true;
            }
            else if (PlayerHandValue == 21)
            {
                MessageBox.Show("Player won $x");
                return true;
            }
            else if (PlayerCardCount == 4 && PlayerHandValue < 21)
                _gb.DealerTurn();

            return false;
        }
    }
}
