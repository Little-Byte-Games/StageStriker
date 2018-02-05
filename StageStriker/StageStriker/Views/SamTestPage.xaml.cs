using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StageStriker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SamTestPage : ContentPage
    {
        int buttonClicked = 0;
        bool animatingStrike;
        bool animatingPick;

        public SamTestPage()
        {
            InitializeComponent();
            AnimateAsyncSplash();
            AnimateAsyncStrike();
        }

        public void SwitchBackgrounds(object sender, EventArgs e)
        {

            buttonClicked++;

            if (buttonClicked % 2 > 0)
            {
                animatingStrike = false;
                AnimateAsyncPick();
            }
            else
            {
                animatingPick = false;
                AnimateAsyncStrike();
            }
        }

        public void AnimateGo(object sender, EventArgs e)
        {
            AnimateAsyncGo();
        }

       public async Task AnimateAsyncStrike()
        {
            animatingStrike = true;

            Background.Source = new FileImageSource() { File = "bg_stripes_red.png" };
            Background.RotationY = 180;
            Background.VerticalOptions = LayoutOptions.End;

            while (animatingStrike)
            {
                await Background.TranslateTo(0, 0, 3000);
                await Background.TranslateTo(0, -640, 0);
            }

        }

        public async Task AnimateAsyncPick()
        {
            animatingPick = true;

            Background.Source = new FileImageSource() { File = "bg_stripes_green.png" };
            Background.VerticalOptions = LayoutOptions.Start;

            while (animatingPick)
            {
                await Background.TranslateTo(0, -640, 3000);
                await Background.TranslateTo(0, 0, 0);
            }

        }

        public async Task AnimateAsyncGo()
        {
            Go_G.Opacity = 1;
            Go_O.Opacity = 1;
            Go_Exclam.Opacity = 1;
            await Go_Exclam.TranslateTo(0, 0, 0);

            await Task.WhenAll
            (
                Go_G.TranslateTo(0, -120, 0),
                Go_O.TranslateTo(0, 120, 0),
                Go_Exclam.ScaleTo(0, 0)
            );

            await Task.WhenAll
            (
                Go_G.TranslateTo(0, 0, 200, Easing.BounceOut),
                Go_O.TranslateTo(0, 0, 200, Easing.BounceOut),
                Go_Exclam.ScaleTo(1, 200, Easing.BounceOut)
            );

            await Go_G.TranslateTo(0, 0, 500); // pause

            await Task.WhenAll
            (
                Go_G.TranslateTo(-300, 0, 100),
                Go_O.TranslateTo(300, 0, 100),
                Go_Exclam.TranslateTo(300, 0, 100)
            );
        }

        public async Task AnimateAsyncSplash()
        {
            Logo_Image.Opacity = 1;
            Logo_Little.Opacity = 1;
            Logo_Byte.Opacity = 1;
       
            await Task.WhenAll
            (
                Logo_Byte.TranslateTo(0, -400, 0),
                Logo_Little.TranslateTo(0, -400, 0),
                Logo_Image.TranslateTo(0, -400, 0)
            );

            await Logo_Byte.TranslateTo(0, 0, 500); // pause
            await Logo_Byte.TranslateTo(0, 0, 200, Easing.CubicIn);
            await Logo_Little.TranslateTo(0, 0, 200, Easing.CubicIn);
            await Logo_Image.TranslateTo(0, 0, 200, Easing.BounceOut);
            await Logo_Image.TranslateTo(0, 0, 200); // pause

            await Subtitle.FadeTo(1, 500, Easing.CubicInOut);
            await Subtitle.FadeTo(1, 1500); // pause

            await SplashScreen.FadeTo(0, 500);
        }
    }
}