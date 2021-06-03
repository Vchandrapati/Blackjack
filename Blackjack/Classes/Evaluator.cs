using static Blackjack.Classes.Variables;

namespace Blackjack.Classes
{
    public static class Evaluator
    {
        static Game_Board gb = new Game_Board();
        public static void DealerEvaluate()
        {
            if (DealerHandValue > 21 && DA > 0)
            {
                while (DealerHandValue > 21)
                {
                    if (DA != 0) DealerHandValue -= 10;
                }
            }
            else if (DealerHandValue > 21 && DA == 0 || DealerHandValue > 17 && DealerHandValue > 21)
                gb.Start(); 
            else if (DealerHandValue > 17 && DealerHandValue <= 21 && DealerHandValue > PlayerHandValue || DealerHandValue > 17 && DealerHandValue == 21 || DealerHandValue == 17 && DealerHandValue > PlayerHandValue || DealerHandValue > 17 && DealerHandValue > PlayerHandValue)
                gb.Start();
            else if (DealerHandValue == 17 && DealerHandValue < PlayerHandValue || DealerHandValue > 17 && DealerHandValue < PlayerHandValue)
                gb.Start();
            else if (DealerHandValue == PlayerHandValue)
                gb.Start();
        }

        public static void PlayerEvaluate()
        {
            if (PlayerHandValue > 21 && PA > 0)
            {
                while (PlayerHandValue > 21)
                    if (PA != 0)
                        PlayerHandValue -= 10;
            }
            else if (PlayerHandValue > 21 && PA == 0)
                gb.Start();
            else if (PlayerHandValue == 21)
                gb.Start();
            else if (PlayerCardCount == 4 && PlayerHandValue < 21)
                gb.DealerTurn();
        }
    }
}
