namespace StageStriker
{
    public interface IStrikeController
    {
        Player ChoosingPlayer { get; }
        bool IsChoosing { get; }
        Player NextStriker { get; }

        bool CanStrike(Stage stage);
        void Strike(Stage stage);
        bool WasLastStrike(Stage stage);
        void UnstrikeLastStage();
        StageGroup GetStageGroup(Stage stage);
    }
}