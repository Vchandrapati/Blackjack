using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static Blackjack.Classes.Variables;

namespace Blackjack.Classes
{
    internal class Update
    {
        public void UpdateTable()
        {
            if (!Connection) return;
            _sqlconnect.Update(User, "Wins", Wins);
            _sqlconnect.Update(User, "Losses", Losses);
            _sqlconnect.Update(User, "Games_Played", Games_Played);
            _sqlconnect.Update(User, "Money_Won", Money_Won);
            _sqlconnect.Update(User, "Money_Lost", Money_Lost);
            _sqlconnect.Update(User, "Pushes", Pushes);
            _sqlconnect.Update(User, "Cards_Drawn", Cards_Drawn);

            Losses = _sqlconnect.Info(User, "Losses");
            Wins = _sqlconnect.Info(User, "Wins");
            Games_Played = _sqlconnect.Info(User, "Games_Played");
            Money_Won = _sqlconnect.Info(User, "Money_Won");
            Money_Lost = _sqlconnect.Info(User, "Money_Lost");
            Pushes = _sqlconnect.Info(User, "Pushes");
            Cards_Drawn = _sqlconnect.Info(User, "Cards_Drawn");
        }
    }
}
