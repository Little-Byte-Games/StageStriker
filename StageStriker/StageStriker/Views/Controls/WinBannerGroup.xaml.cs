using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StageStriker.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WinBannerGroup
    {
        private readonly Image[] banners;

        public WinBannerGroup()
        {
            InitializeComponent();

            banners = new[]
            {
                Banner1,
                Banner2,
            };
        }

        public void ShowWins(int wins, bool includesLastWin)
        {
            for(int i = 0; i < wins; i++)
            {
                if(includesLastWin && i == wins - 1)
                {
                    banners[i].Source = "winbanner_gold.png";
                }

                banners[i].IsVisible = true;
            }
        }
    }
}