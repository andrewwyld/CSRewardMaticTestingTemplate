using System.Threading.Tasks;
using NUnit.Framework;

namespace RewardMatic_4000
{
    public partial class Tests
    {
        [SetUp]
        public void Setup()
        {
            RewardGroup.MakeFromFileName("../../../rewards.json");
        }

        // test to make sure a user's score updates correctly and is arithmetically consistent
        [Test]
        public void TestScoreIncrementsCorrectly()
        {
            User aspidistra = new User();

            Assert.AreEqual(0, aspidistra.Score);
            
            aspidistra.UpdateScore(250);
            
            Assert.AreEqual(250,aspidistra.Score);

            aspidistra.UpdateScore(250000);
            
            Assert.AreEqual(250250, aspidistra.Score);
        }

        // test to make sure the "reward in progress" function works correctly
        // TODO implement User.GetRewardInProgress()
        [Test]
        public void TestRewardInProgress()
        {
            User rangdo = new User();
            
            Assert.AreEqual(RewardGroup.Available?[0].GetRewardByIndex(0), rangdo.GetRewardInProgress());
            
            rangdo.UpdateScore(250);
            
            Assert.AreEqual(RewardGroup.Available?[0].GetRewardByIndex(1), rangdo.GetRewardInProgress());
            
            rangdo.UpdateScore(250000);
            
            Assert.IsNull(rangdo.GetRewardInProgress());
        }

        // test to make sure the "latest reward received" function works correctly
        [Test]
        public void TestLatestReward()
        {
            User argond = new User();
            
            Assert.IsNull(argond.GetLatestRewardReceived());
            
            argond.UpdateScore(250);
            
            Assert.AreEqual(RewardGroup.Available?[0].GetRewardByIndex(0), argond.GetLatestRewardReceived());
            
            argond.UpdateScore(250000);
            
            Assert.AreEqual(RewardGroup.Available?[4].GetRewardByIndex(5), argond.GetLatestRewardReceived());
        }
    }
}