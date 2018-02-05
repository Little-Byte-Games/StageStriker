using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using StageStriker.Views.Controls;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace StageStriker.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StrikingPage
    {
        private bool wasStriking;

        public StrikingPage()
        {
            InitializeComponent();

            var factory = new StrikeControllerFactory();
            ViewModel = new StrikingViewModel(factory);
            ViewModel.StrikingStartedEvent += OnStrikingStarted;
            ViewModel.NeedsPermissionEvent += OnNeedsPermission;
            ViewModel.StageStruckEvent += stage => AnimateBackground(!ViewModel.StrikeController.IsChoosing);
            ViewModel.StageUnstruckEvent += stage => AnimateBackground(true);
        }

        private void OnStrikingStarted(IList<StageGroup> stages)
        {
            wasStriking = false;
            AnimateBackground(true);

            StageGroupLayout.Children.Clear();
            StageGroupLayout.RowDefinitions.Clear();
            for (int i = 0; i < stages.Count; i++)
            {
                StageGroupLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                IStageGroupView stageGroupView;
                if (stages[i].GroupedStages.Count > 0)
                {
                    stageGroupView = new StageGroupView(stages[i], ViewModel);
                }
                else
                {
                    stageGroupView = new StageGroupSingleView(stages[i], ViewModel);
                }
                StageGroupLayout.Children.Add((View)stageGroupView, 0, i);
                UpdateWinBanners(stages[i], stageGroupView, Player.Player1);
                UpdateWinBanners(stages[i], stageGroupView, Player.Player2);
            }
        }

        private void UpdateWinBanners(StageGroup stageGroup, IStageGroupView stageGroupView, Player player)
        {
            var winStack = ViewModel.SetController.totalWins[player];
            int winCount = winStack.Count(stageGroup.HasStage);
            if (winCount > 0)
            {
                stageGroupView.ShowWins(winCount, player, stageGroup.HasStage(winStack.Peek()));
            }
        }

        private async void OnNeedsPermission(Stage stage)
        {
            var app = App.Instance;
            var choosingPlayer = ViewModel.StrikeController.ChoosingPlayer;
            var permittingPlayer = app.GetOtherPlayer(choosingPlayer);
            var choosingPlayerName = app.GetPlayerName(choosingPlayer);
            var permittingPlayerName = app.GetPlayerName(permittingPlayer);

            var answer = await app.MainPage.DisplayAlert($"{permittingPlayerName} Choice", $"Allow {choosingPlayerName} to chose {stage.Name}?", "Yes", "No");
            if (answer)
            {
                ViewModel.SelectStage(stage, true);
            }
        }

        private async Task AnimateBackground(bool isStriking)
        {
            if (isStriking && !wasStriking)
            {
                wasStriking = true;

                Background.Source = "bg_stripes_red.png";
                Background.RotationY = 180;
                Background.VerticalOptions = LayoutOptions.End;

                while (true)
                {
                    //await Background.TranslateTo(0, -7, 3000);
                    //await Background.TranslateTo(0, -640, 0);
                    await Background.TranslateTo(0, -7, 0);
                }
            }
            else if (!isStriking && wasStriking)
            {
                
                wasStriking = false;

                Background.Source = "bg_stripes_green.png";
                Background.RotationY = 0;
                Background.VerticalOptions = LayoutOptions.Start;

                while (true)
                {
                    //await Background.TranslateTo(0, -640, 3000);
                    await Background.TranslateTo(0, -7, 0);
                }
            }
            
        }
    }
}