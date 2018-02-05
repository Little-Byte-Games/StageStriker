using System.Collections.ObjectModel;
using System.Linq;

namespace StageStriker
{
    public class StageGroup
    {
        public Stage MainStage { get; }
        public ReadOnlyCollection<Stage> GroupedStages { get; }

        public StageGroup(Stage mainStage, params Stage[] groupedStages)
        {
            MainStage = mainStage;
            GroupedStages = new ReadOnlyCollection<Stage>(groupedStages);
        }

        public bool HasStage(Stage stage)
        {
            return MainStage == stage || GroupedStages.Contains(stage);
        }

        public override bool Equals(object obj)
        {
            StageGroup other = obj as StageGroup;
            return other != null && other.MainStage == MainStage && other.GroupedStages.SequenceEqual(GroupedStages);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (MainStage.GetHashCode() * 397) ^ (GroupedStages != null ? GroupedStages.GetHashCode() : 0);
            }
        }
    }
}