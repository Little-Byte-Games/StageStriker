using System;

namespace StageStriker
{
    public class Set
    {
        public int WinsNeeded { get; }
        public int Player1Wins { get; private set; }
        public int Player2Wins { get; private set; }

        public Set(int winsNeeded)
        {
            WinsNeeded = winsNeeded;
        }

        public bool PlayerWon(Player player)
        {
            switch(player)
            {
                case Player.Player1:
                    return ++Player1Wins == WinsNeeded;
                case Player.Player2:
                    return ++Player2Wins == WinsNeeded;
                case Player.None:
                    throw new ArgumentException($"{player} can't be a winner.");
                default:
                    throw new ArgumentOutOfRangeException(nameof(player), player, null);
            }
        }
    }
}