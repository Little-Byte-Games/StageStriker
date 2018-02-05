using Xamarin.Forms.Xaml;

namespace StageStriker.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartingPage
    {
        public StartingPage()
        {
            InitializeComponent();

            ViewModel = new StartingViewModel();
        }
    }
}