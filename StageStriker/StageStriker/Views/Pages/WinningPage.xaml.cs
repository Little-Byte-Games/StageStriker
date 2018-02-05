using Xamarin.Forms.Xaml;

namespace StageStriker.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WinningPage
    {
        public WinningPage()
        {
            InitializeComponent();

            ViewModel = new WinningViewModel();
        }
    }
}