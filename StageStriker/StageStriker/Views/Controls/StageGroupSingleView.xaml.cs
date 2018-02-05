using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StageStriker.Views.Controls
{
    public interface IStageGroupView
    {
        void ShowWins(int wins, Player player, bool includesLastWin);
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StageGroupSingleView : IStageGroupView
    {
        private readonly StrikingViewModel strikingViewModel;
        private readonly StageGroup stageGroup;
        public readonly ICommand stageSelectedCommand;

        public StageGroupSingleView(StageGroup stageGroup, StrikingViewModel strikingViewModel)
        {
            this.stageGroup = stageGroup;
            this.strikingViewModel = strikingViewModel;

            stageSelectedCommand = new Command(OnStageSelected);

            strikingViewModel.StageStruckEvent += OnStageStruck;
            strikingViewModel.StageUnstruckEvent += OnStageUnstruck;

            InitializeComponent();

            var button = new StageButtonView(stageGroup.MainStage, view => stageSelectedCommand.Execute(stageGroup.MainStage));
            ButtonLayout.Children.Add(button);
        }

        public void ShowWins(int wins, Player player, bool includesLastWin)
        {
            var winBanners = player == Player.Player1 ? WinBanners1 : WinBanners2;
            winBanners.ShowWins(wins, includesLastWin);
        }

        private void OnStageStruck(Stage stage)
        {
            UpdateButtonStrikeStates(stage, true);
        }

        private void OnStageUnstruck(Stage stage)
        {
            UpdateButtonStrikeStates(stage, false);
        }

        private void UpdateButtonStrikeStates(Stage stage, bool isStruck)
        {
            if (stageGroup.HasStage(stage))
            {
                ButtonLayout.Children.Cast<StageButtonView>().First().MarkAsStruck(isStruck);
            }
        }

        private void OnStageSelected(object stageObj)
        {
            Stage stage = (Stage)stageObj;
            strikingViewModel.OnStageGroupClicked(stageGroup, stage);
        }
    }
}