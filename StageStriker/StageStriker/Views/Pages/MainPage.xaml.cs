using System;
using StageStriker.Views.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StageStriker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage
    {
        public StartingPage StartingPage { get; } = new StartingPage();
        public StrikingPage StrikingPage { get; } = new StrikingPage();
        public WinningPage WinningPage { get; }

        public MainPage()
        {
            WinningPage = new WinningPage();

            InitializeComponent();

            Header.Initialize(StrikingPage.ViewModel);

            var pageNavigator = new PageNavigator(this);
            GC.KeepAlive(pageNavigator);

            SetPage(StartingPage);
        }

        public void SetPage(ContentView page)
        {
            Page.Content = page;
        }
    }
}