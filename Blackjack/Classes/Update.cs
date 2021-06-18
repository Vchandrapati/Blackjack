using System;
using static Blackjack.Classes.Variables;

namespace Blackjack.Classes
{
    internal class Update
    {
        public void UpdateTable()
        {
            //Checking to see if there is a server connection
            if (!Connection) return;

            _sqlconnect.Close();
            _sqlconnect.Connect();
            _sqlconnect.Update();

            //Retrieving player data
            Level = Math.Floor(_sqlconnect.Info(User, "Points") / 1000m) - 2m;
            Losses = _sqlconnect.Info(User, "Losses");
            Wins = _sqlconnect.Info(User, "Wins");
            GamesPlayed = _sqlconnect.Info(User, "Games_Played");
            MoneyWon = _sqlconnect.Info(User, "Money_Won");
            MoneyLost = _sqlconnect.Info(User, "Money_Lost");
            Pushes = _sqlconnect.Info(User, "Pushes");
            CardsDrawn = _sqlconnect.Info(User, "Cards_Drawn");
            Money = _sqlconnect.Info(User, "Money");
            Points = _sqlconnect.Info(User, "Points");
        }
    }
}
