using NUnit.Framework;

namespace StageStriker.Tests
{
    [TestFixture]
    public class StageGroupTests
    {
        private readonly Stage stageA = new Stage("A", "A.png");
        private readonly Stage stageB = new Stage("B", "B.png");
        private readonly Stage stageC = new Stage("C", "C.png");

        [Test]
        public void HasStage_DoesNot_False()
        {
            var testObj = new StageGroup(stageA, stageB);

            var hasStage = testObj.HasStage(stageC);

            Assert.False(hasStage);
        }

        [Test]
        public void HasStage_IsMain_True()
        {
            var testObj = new StageGroup(stageA, stageB, stageC);

            var hasStage = testObj.HasStage(stageA);

            Assert.True(hasStage);
        }

        [Test]
        public void HasStage_IsChild_True()
        {
            var testObj = new StageGroup(stageA, stageB, stageC);

            var hasStage = testObj.HasStage(stageC);

            Assert.True(hasStage);
        }
    }
}
