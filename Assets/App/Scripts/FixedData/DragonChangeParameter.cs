using System;
using UnityEngine;

namespace App.Scripts.FixedData
{
    [Serializable]
    public class DragonChangeParameter
    {
        [SerializeField] private int energy;
        public int Energy => this.energy;

        [SerializeField] private int toilet;
        public int Toilet => this.toilet;
        
        [SerializeField] private int hungry;
        public int Hungry => this.hungry;

        [SerializeField] private int injury;
        public int Injury => this.injury;

        [SerializeField] private int wannaPlay;
        public int WannaPlay => this.wannaPlay;

        [SerializeField] private int like;
        public int Like => this.like;

        [SerializeField] private int toyLike;
        public int ToyLike => this.toyLike;

        [SerializeField] private int language;
        public int Language => this.language;
        
        public void TestValue()
        {
#if DEBUG
#endif
        }
    }
}