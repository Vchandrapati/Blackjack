using System;
using System.Collections.Generic;
using System.Text;
using static Blackjack.Game_Board;
using static Blackjack.Classes.Variables;
using System.Windows.Media;

namespace Blackjack.Classes
{
    public static class Evaluator
    {
        static Game_Board gb = new Game_Board();
        public static void DealerEvaluate()
        {
            if (DH_Value > 21 && DA > 0)
            {
                while (DH_Value > 21)
                {
                    if (DA != 0) DH_Value -= 10;
                }
            }
            else if (DH_Value > 21 && DA == 0 || DH_Value > 17 && DH_Value > 21)
                gb.Start(); 
            else if (DH_Value > 17 && DH_Value <= 21 && DH_Value > PH_Value || DH_Value > 17 && DH_Value == 21 || DH_Value == 17 && DH_Value > PH_Value || DH_Value > 17 && DH_Value > PH_Value)
                gb.Start();
            else if (DH_Value == 17 && DH_Value < PH_Value || DH_Value > 17 && DH_Value < PH_Value)
                gb.Start();
            else if (DH_Value == PH_Value)
                gb.Start();
        }

        public static void PlayerEvaluate()
        {
            if (PH_Value > 21 && PA > 0)
            {
                while (PH_Value > 21)
                    if (PA != 0)
                        PH_Value -= 10;
            }
            else if (PH_Value > 21 && PA == 0)
                gb.Start();
            else if (PH_Value == 21)
                gb.Start();
            else if (PlayerCardCount == 4 && PH_Value < 21)
                gb.Stand();
        }
    }
}
