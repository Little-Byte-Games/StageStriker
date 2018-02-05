namespace StageStriker.Controls
{
    public class HeaderViewModel : ViewModel
    {
        private readonly StrikingViewModel strikingViewModel;
        public string ActionLabelText { get; private set; }
        public string Player1Name { get => App.Instance.Player1Name; set => App.Instance.Player1Name = value; }
        public string Player2Name { get => App.Instance.Player2Name; set => App.Instance.Player2Name = value; }

        public HeaderViewModel(StrikingViewModel strikingViewModel)
        {
            this.strikingViewModel = strikingViewModel;

            strikingViewModel.StrikingStartedEvent += set => UpdateText();
            strikingViewModel.StageStruckEvent += stage => UpdateText();
            strikingViewModel.StageUnstruckEvent += stage => UpdateText();
            strikingViewModel.StageChosenEvent += OnStageChosen;
        }

        private void UpdateText()
        {
            var strikeController = strikingViewModel.StrikeController;
            ActionLabelText = strikeController.IsChoosing ? $"{App.Instance.GetPlayerName(strikeController.ChoosingPlayer)} chose stage!" : $"{App.Instance.GetPlayerName(strikeController.NextStriker)} strike a stage!";
        }

        private void OnStageChosen(Stage stage)
        {
            ActionLabelText = "Chose winner!";
        }
    }
}