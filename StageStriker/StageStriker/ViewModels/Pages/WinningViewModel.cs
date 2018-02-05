using System.Windows.Input;
using Xamarin.Forms;

namespace StageStriker
{
    public class WinningViewModel : ViewModel
    {
        public delegate void MatchWonDelegate(Player player, Stage stage);

        public event MatchWonDelegate MatchWonEvent;

        private Stage currentStage;

        public string Label { get; private set; }
        public string Player1Label { get; private set; }
        public string Player2Label { get; private set; }
        public string StageName { get; private set; }
        public string StageImageSource { get; private set; }
        public ICommand WonButtonClicked { get; }

        public WinningViewModel()
        {
            WonButtonClicked = new Command(OnWonButtonClicked);
        }

        private void OnWonButtonClicked(object playerNumberData)
        {
            var playerNumber = int.Parse((string)playerNumberData);
            var player = (Player)playerNumber;
            MatchWonEvent?.Invoke(player, currentStage);
        }

        public void StartMatch(Stage stage)
        {
            currentStage = stage;

            var app = App.Instance;

            Label = $"{app.Player1Name} v {app.Player2Name} on {stage.Name}";
            Player1Label = $"{app.Player1Name} Won";
            Player2Label = $"{app.Player2Name} Won";
            StageName = currentStage.Name;
            StageImageSource = currentStage.Image;
        }
    }
}