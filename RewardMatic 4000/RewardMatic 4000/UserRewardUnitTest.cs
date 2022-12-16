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
            IUser aspidistra = new User();

            Assert.AreEqual(0, aspidistra.GetScore());
            
            aspidistra.UpdateScore(250);
            
            Assert.AreEqual(250,aspidistra.GetScore());

            aspidistra.UpdateScore(250000);
            
            Assert.AreEqual(250250, aspidistra.GetScore());
        }
    }
}