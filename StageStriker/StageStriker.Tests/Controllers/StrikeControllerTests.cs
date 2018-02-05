using NUnit.Framework;
using Slot = StageStriker.StrikingRules.Slot;

namespace StageStriker.Tests
{
    [TestFixture]
    public class StrikeControllerTests
    {
        private readonly Stage stageA = new Stage("A", "A.png");

        [TestCase(Player.Player1)]
        [TestCase(Player.Player2)]
        public void ChoosingPlayer_Valid_GetsPlayer(Player player)
        {
            var testObj = CreateTestObj(player);

            Assert.AreEqual(player, testObj.ChoosingPlayer);
        }

        [Test]
        public void IsChoosing_HasStrikes_False()
        {
            var testObj = CreateTestObj(Player.Player1, Slot.A);

            Assert.False(testObj.IsChoosing);
        }

        [Test]
        public void IsChoosing_NoStrikes_True()
        {
            var testObj = CreateTestObj(Player.Player1);

            Assert.True(testObj.IsChoosing);
        }

        [TestCase(Player.Player1)]
        [TestCase(Player.Player2)]
        public void NextStriker_HasStrikes_GetsPlayer(Player player)
        {
            var testObj = CreateTestObj(player, Slot.A);

            Assert.AreEqual(player, testObj.NextStriker);
        }

        [Test]
        public void NextStriker_NoStrikes_GetsPlayer()
        {
            var testObj = CreateTestObj(Player.Player1);

            Assert.AreEqual(Player.None, testObj.NextStriker);
        }

        [Test]
        public void Strike_HasStrikes_IsNotChoosing()
        {
            var testObj = CreateTestObj(Player.Player1, Slot.A, Slot.B);

            testObj.Strike(stageA);

            Assert.False(testObj.IsChoosing);
        }

        [Test]
        public void Strike_NoStrikes_IsChoosing()
        {
            var testObj = CreateTestObj(Player.Player1, Slot.A);

            testObj.Strike(stageA);

            Assert.True(testObj.IsChoosing);
        }

        [TestCase(Player.Player1, Player.Player2)]
        [TestCase(Player.Player2, Player.Player1)]
        public void Strike_DifferentPlayer_NextStrikerUpdated(Player first, Player next)
        {
            var testObj = CreateTestObj(first, Slot.A, Slot.B);

            testObj.Strike(stageA);

            Assert.AreEqual(next, testObj.NextStriker);
        }

        [Test]
        public void CanStrike_NotStruck_True()
        {
            var testObj = CreateTestObj(Player.Player1);

            var canStrike = testObj.CanStrike(stageA);

            Assert.True(canStrike);
        }

        [Test]
        public void CanStrike_Struck_False()
        {
            var testObj = CreateTestObj(Player.Player1);

            testObj.Strike(stageA);
            var canStrike = testObj.CanStrike(stageA);

            Assert.False(canStrike);
        }

        private StrikeController CreateTestObj(Player firstPlayer, params Slot[] strikes)
        {
            var matchRules = CreateMatchRules(strikes);
            return new StrikeController(matchRules, firstPlayer);
        }

        private MatchRules CreateMatchRules(params Slot[] strikes)
        {
            var stages = new[]
            {
                new StageGroup(stageA),
            };

            var rules = new StrikingRules(Slot.A, strikes);

            return new MatchRules(stages, rules);
        }
    }
}