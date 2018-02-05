using System;
using System.Collections.Generic;
using System.Diagnostics;
using StageStriker.Controllers;

namespace StageStriker
{
    public class StrikingViewModel : ViewModel
    {
        public delegate void StageDelegate(Stage stage);
        public delegate void SetStartedDelegate(Set set);
        public delegate void StrikingStartedDelegate(IList<StageGroup> stages);

        public event SetStartedDelegate SetStartedEvent;
        public event StrikingStartedDelegate StrikingStartedEvent;
        public event StageDelegate StageStruckEvent;
        public event StageDelegate StageUnstruckEvent;
        public event StageDelegate StageBlockedEvent;
        public event StageDelegate StageChosenEvent;
        public event StageDelegate NeedsPermissionEvent;

        private readonly IStrikeControllerFactory strikeControllerFactory;
        private SetRules setRules;
        public SetController SetController { get; private set; }

        public IStrikeController StrikeController { get; private set; }
        public string BackgroundSource { get; set; }

        public StrikingViewModel(IStrikeControllerFactory strikeControllerFactory)
        {
            this.strikeControllerFactory = strikeControllerFactory;
        }

        public void OnStageGroupClicked(StageGroup stageGroup, Stage stage)
        {
            SelectStage(stage);
        }

        public void StartSet(SetController setController)
        {
            this.SetController = setController;
            setRules = SetRulesFactory.CreateDefaultSetRules();

            var firstPlayer = new Random().Next(0, 2) == 0 ? Player.Player1 : Player.Player2;
            StrikeController = strikeControllerFactory.Create(setRules.StarterRules, firstPlayer);

            SetStartedEvent?.Invoke(this.SetController.set);

            StrikingStartedEvent?.Invoke(setRules.StarterRules.Stages);
        }

        public void StartCounterPicks(Player winner)
        {
            StrikeController = strikeControllerFactory.Create(setRules.CounterRules, winner);
            StrikingStartedEvent?.Invoke(setRules.CounterRules.Stages);
        }

        public void SelectStage(Stage stage, bool force = false)
        {
            if(StrikeController.WasLastStrike(stage))
            {
                StrikeController.UnstrikeLastStage();
                StageUnstruckEvent?.Invoke(stage);
            }
            else if(StrikeController.IsChoosing)
            {
                if(StrikeController.CanStrike(stage))
                {
                    var lastWonStage = SetController.lastStagesWon[StrikeController.ChoosingPlayer];
                    var selectedStageGroup = StrikeController.GetStageGroup(stage);
                    if(force || lastWonStage == null || !selectedStageGroup.HasStage(lastWonStage.Value))
                    {
                        Debug.WriteLine($"Stage Chosen: {stage.Name}");
                        StageChosenEvent?.Invoke(stage);
                    }
                    else
                    {
                        Debug.WriteLine($"Stage Needs Permission: {stage.Name}");
                        NeedsPermissionEvent?.Invoke(stage);
                    }
                }
            }
            else
            {
                if(StrikeController.CanStrike(stage))
                {
                    Debug.WriteLine($"Stage Struck: {stage.Name}");
                    StrikeController.Strike(stage);
                    StageStruckEvent?.Invoke(stage);
                }
                else
                {
                    Debug.WriteLine($"Stage Can't be Struck: {stage.Name}");
                    StageBlockedEvent?.Invoke(stage);
                }
            }
        }
    }
}