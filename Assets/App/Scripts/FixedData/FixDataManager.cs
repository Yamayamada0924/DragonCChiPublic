using UnityEngine;
using UnityEngine.Serialization;
using yydlib;

namespace App.Scripts.FixedData
{
    /// <summary>
    /// 固定パラメータの管理
    /// </summary>
    public class FixDataManager : SingletonMonoBehaviour<FixDataManager>
    {

        [SerializeField] private ItemParameters itemParameters;
        public ItemParameters ItemParameters => this.itemParameters;

        [SerializeField] private TalkParameters talkParameters;
        public TalkParameters TalkParameters => this.talkParameters;

        [SerializeField] private WordParameters wordParameters;
        public WordParameters WordParameters => this.wordParameters;
        
        [SerializeField] private MiscParameters miscParameters;
        public MiscParameters MiscParameters => this.miscParameters;

        protected override bool DontDestroyOnLoad => true;
        protected override void Init()
        {
            // 個数チェック
            itemParameters.TestValue();
            talkParameters.TestValue();
            wordParameters.TestValue();
        }
    }
}