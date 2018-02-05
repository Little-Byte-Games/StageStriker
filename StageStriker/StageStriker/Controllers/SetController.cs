using System.Collections.Generic;

namespace StageStriker.Controllers
{
    public class SetController
    {
        public readonly Set set;
        public readonly PlayerCollection<Stage?> lastStagesWon = new PlayerCollection<Stage?>(null, null);
        public readonly PlayerCollection<Stack<Stage>> totalWins = new PlayerCollection<Stack<Stage>>(new Stack<Stage>(), new Stack<Stage>());

        public SetController(Set set)
        {
            this.set = set;
        }

        public void MatchWon(Player winner, Stage stage)
        {
            lastStagesWon[winner] = stage;
            totalWins[winner].Push(stage);
        }

        public bool HasPlayerWon(Player player)
        {
            return totalWins[player].Count == set.WinsNeeded;
        }
    }
}
