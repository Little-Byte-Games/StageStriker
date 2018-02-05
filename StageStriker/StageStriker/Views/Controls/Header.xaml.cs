using StageStriker.Controls;
using Xamarin.Forms.Xaml;

namespace StageStriker.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Header
    {
        public Header()
        {
            InitializeComponent();
        }

        public void Initialize(StrikingViewModel strikingViewModel)
        {
            BindingContext = new HeaderViewModel(strikingViewModel);
        }
    }
}