using System;
using NUnit.Framework;

namespace StageStriker.Tests
{
    [TestFixture]
    public class SetTests
    {
        [TestCase(Player.Player1)]
        [TestCase(Player.Player2)]
        public void PlayerWon_NotEnoughWins_ReturnsFalse(Player player)
        {
            var testObj = new Set(2);

            var setOver = testObj.PlayerWon(player);

            Assert.IsFalse(setOver);
        }

        [TestCase(Player.Player1)]
        [TestCase(Player.Player2)]
        public void PlayerWon_MetRequiredWins_ReturnsTrue(Player player)
        {
            var testObj = new Set(2);

            testObj.PlayerWon(player);
            var setOver = testObj.PlayerWon(player);

            Assert.IsTrue(setOver);
        }

        [Test]
        public void PlayerWon_PlayerNone_Exception()
        {
            var testObj = new Set(1);

            Assert.Throws<ArgumentException>(() => testObj.PlayerWon(Player.None));
        }
    }
}