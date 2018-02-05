using StageStriker.Controllers;

namespace StageStriker
{
    public class PageNavigator
    {
        private readonly MainPage mainPage;
        private SetController setController;


        public PageNavigator(MainPage mainPage)
        {
            this.mainPage = mainPage;
            mainPage.StartingPage.ViewModel.SetStartEvent += OnSetStarted;
            mainPage.StrikingPage.ViewModel.StageChosenEvent += OnStageChosen;
            mainPage.WinningPage.ViewModel.MatchWonEvent += OnMatchWon;
        }

        private void OnSetStarted(int winsNeeded)
        {
            var set = new Set(winsNeeded);
            setController = new SetController(set);

            mainPage.StrikingPage.ViewModel.StartSet(setController);
            mainPage.SetPage(mainPage.StrikingPage);
        }

        private void OnStageChosen(Stage stage)
        {
            mainPage.WinningPage.ViewModel.StartMatch(stage);
            mainPage.SetPage(mainPage.WinningPage);
        }

        private void OnMatchWon(Player player, Stage stage)
        {
            setController.MatchWon(player, stage);

            if(setController.HasPlayerWon(player))
            {
                mainPage.SetPage(mainPage.StartingPage);
            }
            else
            {
                mainPage.StrikingPage.ViewModel.StartCounterPicks(player);
                mainPage.SetPage(mainPage.StrikingPage); 
            }
        }
    }
}