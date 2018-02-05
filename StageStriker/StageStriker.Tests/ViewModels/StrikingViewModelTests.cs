//using System.Linq;
//using NSubstitute;
//using NUnit.Framework;

//namespace StageStriker.Tests
//{
//    [TestFixture]
//    public class StrikingViewModelTests
//    {
//        private readonly Stage stageA = new Stage("A", "A.png");

//        private StrikingViewModel testObj;
//        private IStrikeControllerFactory factory;
//        private IStrikeController strikeController;

//        [SetUp]
//        public void SetUp()
//        {
//            factory = Substitute.For<IStrikeControllerFactory>();
//            strikeController = Substitute.For<IStrikeController>();
//            strikeController.GetStageGroup(stageA).Returns(new StageGroup(stageA));

//            factory.Create(null, Player.None).ReturnsForAnyArgs(strikeController);

//            testObj = new StrikingViewModel(factory);
//        }

//        [Test]
//        public void StartSet_Valid_CreateStarterStriker()
//        {
//            testObj.StartSet(3);

//            factory.Received(1).Create(Arg.Any<MatchRules>(), Arg.Any<Player>());
//        }

//        [Test]
//        public void SelectStage_IsStriking_StrikeStage()
//        {
//            var struckStage = new Stage();

//            strikeController.CanStrike(stageA).Returns(true);

//            testObj.StartSet(2);
//            testObj.StageStruckEvent += stage => struckStage = stage;

//            testObj.SelectStage(stageA);

//            Assert.AreEqual(stageA, struckStage);
//        }

//        [Test]
//        public void SelectStage_IsStrikingCantStrike_DontStrike()
//        {
//            var struckStage = new Stage();

//            testObj.StartSet(2);
//            testObj.StageBlockedEvent += stage => struckStage = stage;

//            testObj.SelectStage(stageA);

//            Assert.AreEqual(stageA, struckStage);
//        }

//        [Test]
//        public void SelectStage_IsChoosingAndCanChoose_ChooseStage()
//        {
//            var chosenStage = new Stage();

//            strikeController.IsChoosing.Returns(true);
//            strikeController.ChoosingPlayer.Returns(Player.Player1);
//            strikeController.CanStrike(stageA).Returns(true);

//            testObj.StartSet(2);
//            testObj.StageChosenEvent += stage => chosenStage = stage;

//            testObj.SelectStage(stageA);

//            Assert.AreEqual(stageA, chosenStage);
//        }

//        [Test]
//        public void SelectStage_IsChoosingAndStruck_CantChoose()
//        {
//            var called = false;

//            strikeController.IsChoosing.Returns(true);

//            testObj.StartSet(2);
//            testObj.StageChosenEvent += stage => called = true;

//            testObj.SelectStage(stageA);

//            Assert.False(called);
//        }

//        [Test]
//        public void SelectStage_IsChoosingAndLastWon_AskForPermission()
//        {
//            var chosenStage = new Stage();

//            strikeController.IsChoosing.Returns(true);
//            strikeController.ChoosingPlayer.Returns(Player.Player1);
//            strikeController.CanStrike(stageA).Returns(true);

//            testObj.StartSet(2);
//            testObj.StageWon(Player.Player1, stageA);
//            testObj.NeedsPermissionEvent += stage => chosenStage = stage;

//            testObj.SelectStage(stageA);

//            Assert.AreEqual(stageA, chosenStage);
//        }

//        [Test]
//        public void StageWon_FirstWin_CreateCounterRules()
//        {
//            var counterRules = SetRulesFactory.CreateDefaultSetRules().CounterRules;
//            const Player winner = Player.Player2;

//            strikeController.CanStrike(stageA).Returns(true);

//            testObj.StartSet(2);
//            testObj.StageWon(winner, stageA);

//            var recieved = factory.ReceivedCalls().Any(c => c.GetArguments()[0].Equals(counterRules) && c.GetArguments()[1].Equals(winner));
//            Assert.IsTrue(recieved);
//        }
//    }
//}