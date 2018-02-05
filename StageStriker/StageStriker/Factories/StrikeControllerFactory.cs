namespace StageStriker
{
    public class StrikeControllerFactory : IStrikeControllerFactory
    {
        public IStrikeController Create(MatchRules matchRules, Player firstPlayer)
        {
            return new StrikeController(matchRules, firstPlayer);
        }
    }
}