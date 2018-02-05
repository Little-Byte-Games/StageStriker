using System.Collections.Generic;

namespace StageStriker
{
    public class StrikeController : IStrikeController
    {
        private readonly MatchRules matchRules;
        private readonly Player firstPlayer;
        private readonly Stack<StageGroup> strikes = new Stack<StageGroup>();

        public Player ChoosingPlayer => ConvertSlotToPlayer(matchRules.StrikeRules.ChoosingPlayer);
        public bool IsChoosing => strikes.Count == matchRules.StrikeRules.StrikeOrder.Count;
        public Player NextStriker => IsChoosing ? Player.None : ConvertSlotToPlayer(matchRules.StrikeRules.StrikeOrder[StrikeIndex]);
        private int StrikeIndex => strikes.Count;

        public StrikeController(MatchRules matchRules, Player firstPlayer)
        {
            this.matchRules = matchRules;
            this.firstPlayer = firstPlayer;
        }

        public void Strike(Stage stage)
        {
            var stageGroup = matchRules.GetStageGroup(stage);
            strikes.Push(stageGroup);
        }

        public bool WasLastStrike(Stage stage)
        {
            var stageGroup = matchRules.GetStageGroup(stage);
            return strikes.Count > 0 && strikes.Peek().Equals(stageGroup);
        }

        public void UnstrikeLastStage()
        {
            strikes.Pop();
        }

        public StageGroup GetStageGroup(Stage stage)
        {
            return matchRules.GetStageGroup(stage);
        }

        public bool CanStrike(Stage stage)
        {
            var stageGroup = matchRules.GetStageGroup(stage);
            return !strikes.Contains(stageGroup);
        }

        private Player ConvertSlotToPlayer(StrikingRules.Slot slot)
        {
            if (firstPlayer == Player.Player1)
            {
                return slot == StrikingRules.Slot.A ? Player.Player1 : Player.Player2;
            }

            return slot == StrikingRules.Slot.A ? Player.Player2 : Player.Player1;
        }
    }
}