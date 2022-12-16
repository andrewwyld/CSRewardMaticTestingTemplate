#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework.Constraints;

namespace RewardMatic_4000
{
    public class User: IUser
    {
        private int _cumulativeRewardScoresAchieved = 0;
        
        private int _score = 0;

        private Reward? _lastAchieved;
        
        private Reward? _inProgress;
        
        // the index of the reward group in progress. starts at 0
        // only ever augmented from SetRewardByIndices(), if the reward returned by these indices is null
        // this is where this digit is rolled over
        private int _indexRewardGroupInProgress = 0;

        // the index of the reward in progress, within its group. starts at 0
        // only ever augmented from Update(int), when the score indicates a reward has been completed
        // there it will be augmented and a new reward set using SetRewardByIndices() which will perform digit rollover
        // it may be reset to 0 from SetRewardByIndices()
        private int _indexRewardInProgress = 0;

        public User()
        {
            SetRewardByIndices();
        }

        private void SetRewardByIndices()
        {
            if (RewardGroup.Available != null)
            {
                if(!UpdateReward(RewardGroup.Available))
                {
                    // no more rewards available in current reward group, so
                    // increment reward group
                    _indexRewardGroupInProgress++;
                    // start at beginning of new reward group
                    _indexRewardInProgress = 0;
                    // try again
                    UpdateReward(RewardGroup.Available);
                    // this will leave _inProgress null if there are no new reward groups left either
                }
            }
        }

        private bool UpdateReward(RewardGroup[] rewardGroups)
        {
            // otherwise, we have achieved the reward formerly in progress
            if (_inProgress != null) _lastAchieved = _inProgress;

            // our "spent" points include this achieved reward
            if (_lastAchieved != null) _cumulativeRewardScoresAchieved += _lastAchieved.ScoreDifferential;

            // nullify reward in progress
            _inProgress = null;

            if (rewardGroups.Length > _indexRewardGroupInProgress)
            {
                Reward[]? rewards = rewardGroups[_indexRewardGroupInProgress].Rewards;
                if (rewards != null && rewards.Length > _indexRewardInProgress)
                {
                    // set a new reward-in-progress
                    _inProgress = rewards[_indexRewardInProgress];
                    return true;
                }
            }

            return false;
        }

        public int Score
        {
            get { return _score; }
        }

        public void UpdateScore(int update)
        {
            _score += update;
            while (_inProgress != null && UserProgressBeyondScoreDifferential > 0)
            {
                // boost the secondary reward index by one
                _indexRewardInProgress++;
                
                // get the next reward, rolling over the reward group if needed
                SetRewardByIndices();
            }
        }

        public int UserProgressBeyondScoreDifferential { get => _score - _cumulativeRewardScoresAchieved - ScoreDifferential; }

        public int ScoreDifferential => _inProgress?.ScoreDifferential ?? 0;
        
        public Reward? GetRewardInProgress()
        {
            return _inProgress;
        }

        public Reward? GetLatestRewardReceived()
        {
            return _lastAchieved;
        }

        public RewardGroup? GetLatestRewardGroupCompleted()
        {
            if (_indexRewardGroupInProgress == 0) return null;
            return RewardGroup.Available?[_indexRewardGroupInProgress - 1];
        }

        public int GetScore() => Score;

    }
}