#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework.Constraints;

namespace RewardMatic_4000
{
    // a bad implementation of IUser
    public class Misuser: IUser
    {
        // indicate error state if none in progress
        private const int NoneInProgress = -1;

        private int _totalSoFar = 0;
        
        private int _score = 0;

        private Reward? _actualLastAchieved;
        
        private Reward? _actualRewardInProgress;
        
        private int _groupInProgress = 0;

        private int _rewardInProgress = 0;

        public Misuser()
        {
            SetupRewards();
        }

        private void SetupRewards()
        {
            if (RewardGroup.Available != null)
            {
                bool ret = false;
                if (_actualRewardInProgress != null) _actualLastAchieved = _actualRewardInProgress;

                if (_actualLastAchieved != null) _totalSoFar += _actualLastAchieved.ScoreDifferential;

                if (RewardGroup.Available.Length > _groupInProgress)
                {
                    Reward[]? rewards = RewardGroup.Available[_groupInProgress].Rewards;
                    if (rewards != null && rewards.Length > _rewardInProgress)
                    {
                        _actualRewardInProgress = rewards[_rewardInProgress];
                        ret = true;
                    }
                }

                if(!ret)
                {
                    _groupInProgress++;
                    _rewardInProgress = 0;
                    if (_actualRewardInProgress != null) _actualLastAchieved = _actualRewardInProgress;
                    if (_actualLastAchieved != null) _totalSoFar += _actualLastAchieved.ScoreDifferential;
                    _actualRewardInProgress = null;

                    if (RewardGroup.Available.Length > _groupInProgress)
                    {
                        Reward[]? rewards = RewardGroup.Available[_groupInProgress].Rewards;
                        if (rewards != null && rewards.Length > _rewardInProgress)
                        {
                            _actualRewardInProgress = rewards[_rewardInProgress];
                        }
                    }
                }
            }
        }

        public int Score
        {
            get { return _score; }
        }

        public void UpdateScore(int update)
        {
            _score += update;
            while (_actualRewardInProgress != null && ProgressTowardsNextReward > 0)
            {
                // boost the secondary reward index by one
                _rewardInProgress++;
                
                // get the next reward, rolling over the reward group if needed
                SetupRewards();
            }
        }

        public int ProgressTowardsNextReward { get => _score - _totalSoFar - NextRewardScore; }

        public int NextRewardScore => _actualRewardInProgress?.ScoreDifferential ?? NoneInProgress;
        
        public Reward? GetRewardInProgress()
        {
            return _actualRewardInProgress;
        }

        public Reward? GetLatestRewardReceived()
        {
            return _actualLastAchieved;
        }

        public RewardGroup? GetLatestRewardGroupCompleted()
        {
            return RewardGroup.Available?[_groupInProgress - 1];
        }

        public int GetScore() => Score;

    }
}