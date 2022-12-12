#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using NUnit.Framework;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RewardMatic_4000
{
    public class RewardGroup
    {
        public static RewardGroup[]? Available { get; set;  }
        
        private Reward[]? _rewards;

        public string? Name { get; set; }
        
        public Reward[]? Rewards
        {
            get => _rewards;
            set
            {
                _rewards = value;
                if (_rewards != null)
                    foreach (Reward reward in _rewards)
                    {
                        reward.Group = this;
                    }
            }
        }

        public static void MakeFromFileName(string filename)
        {
            string jsonString = File.ReadAllText(filename);
            RewardGroupSet? rewardGroupSet = JsonSerializer.Deserialize<RewardGroupSet>(jsonString);
            Available = rewardGroupSet?.Groups;
        }

        public RewardGroup()
        {
        }

        public Reward? GetRewardByIndex(int i)
        {
            if (i < Rewards?.Length)
            {
                return Rewards[i];
            }
            else
            {
                return null;
            }
        }
    }
}