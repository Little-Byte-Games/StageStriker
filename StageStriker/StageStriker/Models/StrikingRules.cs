using System.Collections.ObjectModel;
using System.Linq;

namespace StageStriker
{
    public class StrikingRules
    {
        public enum Slot
        {
            A,
            B
        }

        public Slot ChoosingPlayer { get; }
        public ReadOnlyCollection<Slot> StrikeOrder { get; }

        public StrikingRules(Slot choosingPlayer, params Slot[] strikes)
        {
            ChoosingPlayer = choosingPlayer;
            StrikeOrder = new ReadOnlyCollection<Slot>(strikes);
        }

        public override bool Equals(object obj)
        {
            StrikingRules other = obj as StrikingRules;
            return other != null && other.ChoosingPlayer == ChoosingPlayer && other.StrikeOrder.SequenceEqual(StrikeOrder);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)ChoosingPlayer * 397) ^ (StrikeOrder != null ? StrikeOrder.GetHashCode() : 0);
            }
        }
    }
}