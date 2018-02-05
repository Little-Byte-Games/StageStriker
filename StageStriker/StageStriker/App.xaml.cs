using System;

namespace StageStriker
{
    public partial class App
    {
        public static App Instance { get; private set; }
        public string Player1Name { get; set; } = "Player 1";
        public string Player2Name { get; set; } = "Player 2";

        public App()
        {
            Instance = this;

            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public Player GetOtherPlayer(Player player)
        {
            switch (player)
            {
                case Player.Player1:
                    return Player.Player2;
                case Player.Player2:
                    return Player.Player1;
                case Player.None:
                    throw new ArgumentException($"{player} is not valid.");
                default:
                    throw new ArgumentOutOfRangeException(nameof(player), player, null);
            }
        }

        public string GetPlayerName(Player player)
        {
            switch (player)
            {
                case Player.Player1:
                    return Player1Name;
                case Player.Player2:
                    return Player2Name;
                case Player.None:
                    throw new ArgumentException($"{player} is not valid.");
                default:
                    throw new ArgumentOutOfRangeException(nameof(player), player, null);
            }
        }
    }
}