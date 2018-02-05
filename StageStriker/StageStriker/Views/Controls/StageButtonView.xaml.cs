using System;
using Xamarin.Forms.Xaml;

namespace StageStriker.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StageButtonView
    {
        private readonly Action<StageButtonView> onClick;

        public Stage Stage { get; }

        public StageButtonView(Stage stage, Action<StageButtonView> onClick)
        {
            this.onClick = onClick;
            Stage = stage;

            InitializeComponent();

            StageImage.Source = stage.Image;
            StageName.Text = stage.ToString();
        }

        public void InverseScaleText(double inverseScale)
        {
            StageName.FontSize /= inverseScale;
            StageNameBox.HeightRequest /= inverseScale;
        }

        private void OnTapped(object sender, EventArgs e)
        {
            onClick(this);
        }

        public void MarkAsStruck(bool isStruck)
        {
            StageOverlay.IsVisible = isStruck;
        }
    }
}