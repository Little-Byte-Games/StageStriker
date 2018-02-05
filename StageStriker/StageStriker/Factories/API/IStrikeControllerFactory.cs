namespace StageStriker
{
    public interface IStrikeControllerFactory
    {
        IStrikeController Create(MatchRules matchRules, Player firstPlayer);
    }
}