using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StageStriker
{
    public class StageList
    {
        private readonly ReadOnlyCollection<StageGroup> starterStages;
        private readonly ReadOnlyCollection<StageGroup> counterStages;
        private readonly StrikingRules strikingRules;

        public StageList(IList<StageGroup> starterStages, IList<StageGroup> counterStages, StrikingRules strikingRules)
        {
            this.starterStages = new ReadOnlyCollection<StageGroup>(starterStages);
            this.counterStages = new ReadOnlyCollection<StageGroup>(counterStages);
            this.strikingRules = strikingRules;
        }
    }
}