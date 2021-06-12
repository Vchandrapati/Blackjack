using System.Windows;
using static Blackjack.Classes.Variables;

namespace Blackjack.Classes
{
    public static class Evaluator
    {
        private static Game_Board gb = new Game_Board();
        public static void DealerEvaluate()
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
                MessageBox.Show("Dealer went bust");
            else if (DealerHandValue > 17 && DealerHandValue <= 21 && DealerHandValue > PlayerHandValue || DealerHandValue > 17 && DealerHandValue == 21 || DealerHandValue == 17 && DealerHandValue > PlayerHandValue || DealerHandValue > 17 && DealerHandValue > PlayerHandValue)
                MessageBox.Show("Dealer won");
            else if (DealerHandValue == 17 && DealerHandValue < PlayerHandValue || DealerHandValue > 17 && DealerHandValue < PlayerHandValue)
                MessageBox.Show("Dealer lost");
            else if (DealerHandValue == PlayerHandValue)
                MessageBox.Show("Push");
        }

        public static void PlayerEvaluate()
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
                MessageBox.Show("Player went bust and lost $x");
            else if (PlayerHandValue == 21)
                MessageBox.Show("Player won $x");
            else if (PlayerCardCount == 4 && PlayerHandValue < 21)
                gb.DealerTurn();
        }
    }
}
