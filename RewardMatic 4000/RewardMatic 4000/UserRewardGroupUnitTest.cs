using NUnit.Framework;

namespace RewardMatic_4000
{
    public partial class Tests
    {
        // test to make sure the "reward in progress" function works correctly
        [Test]
        public void TestRewardGroupInProgress()
        {
            User rangdo = new User();
            
            Assert.AreEqual(RewardGroup.Available?[0], rangdo.GetRewardInProgress()?.Group);
            
            rangdo.UpdateScore(250);
            
            Assert.AreEqual(RewardGroup.Available?[0], rangdo.GetRewardInProgress()?.Group);
            
            rangdo.UpdateScore(250000);
            
            Assert.IsNull(rangdo.GetRewardInProgress()?.Group);
        }

        // test to make sure the "latest reward received" function works correctly
        [Test]
        public void TestLatestRewardReceivedGroup()
        {
            User argond = new User();
            
            Assert.IsNull(argond.GetLatestRewardReceived());
            
            argond.UpdateScore(250);
            
            Assert.AreEqual(RewardGroup.Available?[0], argond.GetLatestRewardReceived()?.Group);
            
            argond.UpdateScore(250000);
            
            Assert.AreEqual(RewardGroup.Available?[4], argond.GetLatestRewardReceived()?.Group);
        }

        // test to make sure the "latest reward received" function works correctly
        [Test]
        public void TestLatestRewardGroupFullyCompleted()
        {
            User argond = new User();
            
            Assert.IsNull(argond.GetLatestRewardGroupCompleted());
            
            argond.UpdateScore(250);
            
            Assert.IsNull(argond.GetLatestRewardGroupCompleted());
            
            argond.UpdateScore(250000);
            
            Assert.AreEqual(RewardGroup.Available?[4], argond.GetLatestRewardGroupCompleted());
        }
    }
}