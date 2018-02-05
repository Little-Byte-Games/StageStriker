using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StageStriker.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StageGroupView : IStageGroupView
    {
        private readonly StageGroup stageGroup;
        private readonly StrikingViewModel strikingViewModel;
        private readonly ICommand stageSelectedCommand;
        private readonly HashSet<StageButtonView> stageButtons;

        public StageGroupView(StageGroup stageGroup, StrikingViewModel strikingViewModel)
        {
            this.stageGroup = stageGroup;
            this.strikingViewModel = strikingViewModel;

            strikingViewModel.StageStruckEvent += OnStageStruck;
            strikingViewModel.StageUnstruckEvent += OnStageUnstruck;

            InitializeComponent();

            stageSelectedCommand = new Command(OnStageSelected);
            stageButtons = CreateStageButtons(stageGroup);
        }

        public void ShowWins(int wins, Player player, bool includesLastWin)
        {
            var winBanners = player == Player.Player1 ? WinBanners1 : WinBanners2;
            winBanners.ShowWins(wins, includesLastWin);
        }

        private HashSet<StageButtonView> CreateStageButtons(StageGroup stageGroup)
        {
            var buttons = new HashSet<StageButtonView>();

            var noChildren = stageGroup.GroupedStages.Count == 0;

            var rowSpan = noChildren ? 2 : 1;
            var inverseScale = noChildren ? 1 : 1.5;

            var mainStageButton = new StageButtonView(stageGroup.MainStage, buttonView => stageSelectedCommand.Execute(buttonView.Stage));
            Grid.SetRowSpan(mainStageButton, rowSpan);
            mainStageButton.InverseScaleText(inverseScale);
            ButtonGrid.Children.Add(mainStageButton);
            buttons.Add(mainStageButton);


            inverseScale += (stageGroup.GroupedStages.Count - 1) * 0.5;
            for (int i = 0; i < stageGroup.GroupedStages.Count; i++)
            {
                GroupedStages.ColumnDefinitions.Add(new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star)});

                var groupStageButton = new StageButtonView(stageGroup.GroupedStages[i], buttonView => stageSelectedCommand.Execute(buttonView.Stage));
                groupStageButton.InverseScaleText(inverseScale);
                buttons.Add(groupStageButton);

                GroupedStages.Children.Add(groupStageButton, i, 0);
            }

            return buttons;
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
                foreach (var stageButton in stageButtons)
                {
                    stageButton.MarkAsStruck(isStruck);
                }
            }
        }

        private void OnStageSelected(object stageObj)
        {
            Stage stage = (Stage)stageObj;
            strikingViewModel.OnStageGroupClicked(stageGroup, stage);
        }
    }
}