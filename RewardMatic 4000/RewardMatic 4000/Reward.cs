using System.Collections;
using System.Collections.Generic;

namespace RewardMatic_4000
{
    public class Reward
    {
        public int ScoreDifferential { get; set; }

        public string Name { get; set;  }
        
        public RewardGroup Group { get; set; }

        public Reward()
        {
        }
    }
}