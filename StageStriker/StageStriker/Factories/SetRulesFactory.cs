using StageStriker.Global;

namespace StageStriker
{
    public static class SetRulesFactory
    {
        public static SetRules CreateDefaultSetRules()
        {
            var starterStages = new[]
            {
                new StageGroup(Stages.FinalDestination),
                new StageGroup(Stages.Battlefield),
                new StageGroup(Stages.Smashville),
                new StageGroup(Stages.TownAndCity),
                new StageGroup(Stages.LylatCruise),
            };
            var starterStriking = new StrikingRules(StrikingRules.Slot.A, StrikingRules.Slot.A, StrikingRules.Slot.B, StrikingRules.Slot.B);
            var starterRules = new MatchRules(starterStages, starterStriking);

            var counterStages = new[]
            {
                new StageGroup(Stages.FinalDestination, Stages.MidgarOmega, Stages.SuzakuCastleOmega, Stages.WilyCastleOmega),
                new StageGroup(Stages.Battlefield, Stages.Dreamland),
                new StageGroup(Stages.Smashville),
                new StageGroup(Stages.TownAndCity),
                new StageGroup(Stages.LylatCruise),
            };
            var counterStriking = new StrikingRules(StrikingRules.Slot.B, StrikingRules.Slot.A);
            var counterRules = new MatchRules(counterStages, counterStriking);

            return new SetRules(starterRules, counterRules);
        }
    }
}