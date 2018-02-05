using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StageStriker
{
    public class MatchRules
    {
        public ReadOnlyCollection<StageGroup> Stages { get; }
        public StrikingRules StrikeRules { get; }

        public MatchRules(IList<StageGroup> stages, StrikingRules strikeRules)
        {
            Stages = new ReadOnlyCollection<StageGroup>(stages);
            StrikeRules = strikeRules;
        }

        public StageGroup GetStageGroup(Stage stage)
        {
            return Stages.First(s => s.HasStage(stage));
        }

        public override bool Equals(object obj)
        {
            MatchRules other = obj as MatchRules;
            return other != null && other.Stages.SequenceEqual(Stages) && other.StrikeRules.Equals(StrikeRules);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Stages != null ? Stages.GetHashCode() : 0) * 397) ^ (StrikeRules != null ? StrikeRules.GetHashCode() : 0);
            }
        }
    }
}