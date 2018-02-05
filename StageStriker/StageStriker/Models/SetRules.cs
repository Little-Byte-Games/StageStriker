namespace StageStriker
{
    public class SetRules
    {
        public MatchRules StarterRules { get; }
        public MatchRules CounterRules { get; }

        public SetRules(MatchRules starterRules, MatchRules counterRules)
        {
            StarterRules = starterRules;
            CounterRules = counterRules;
        }
    }
}