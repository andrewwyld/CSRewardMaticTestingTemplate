#nullable enable
namespace RewardMatic_4000
{
    public interface IUser
    {
        void UpdateScore(int update);
        Reward? GetRewardInProgress();
        Reward? GetLatestRewardReceived();
        RewardGroup? GetLatestRewardGroupCompleted();
        int GetScore();
    }
}