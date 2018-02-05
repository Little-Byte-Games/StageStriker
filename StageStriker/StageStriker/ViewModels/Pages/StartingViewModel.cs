using System.Windows.Input;
using Xamarin.Forms;

namespace StageStriker
{
    public class StartingViewModel : ViewModel
    {
        public delegate void SetStartDelegate(int winsNeeded);

        public event SetStartDelegate SetStartEvent;

        public ICommand StartGameCommand { get; }

        public StartingViewModel()
        {
            StartGameCommand = new Command<string>(StartGame);
        }

        private void StartGame(string gameCountString)
        {
            var games = int.Parse(gameCountString);

            SetStartEvent?.Invoke(games);
        }
    }
}