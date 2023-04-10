using System;
using System.Collections.Generic;
using App.Scripts.GameCommon;
using UnityEngine;
using UnityEngine.Serialization;
using yydlib;

namespace App.Scripts.FixedData
{
    
    [CreateAssetMenu(menuName = "MyScriptable/Create TalkParameter", order = 0)]
    public class TalkParameter : ScriptableObject
    {
        [Serializable]
        public class ConditionParameter
        {
            public DragonParamType dragonParamType;
            public int threshold;
            public ConditionType conditionType;
            
            public bool IsConditionTrue(int value)
            {
                return (conditionType == ConditionType.GraterEqual && value >= threshold) ||
                       (conditionType == ConditionType.LesserEqual && value <= threshold);
            }
        }

        [SerializeField] private Word previousWord;
        public Word PreviousWord => this.previousWord;

        [SerializeField] private Word nextWord;
        public Word NextWord => this.nextWord;

        [SerializeField] private List<Word> answer;
        public List<Word> Answer => this.answer;

        [SerializeField] private List<ConditionParameter> conditions;
        public IReadOnlyList<ConditionParameter> Conditions => this.conditions;

        [SerializeField] private DragonChangeParameter dragonChangeParameter;
        public DragonChangeParameter DragonChangeParameter => this.dragonChangeParameter;
        
        public void TestValue()
        {
#if DEBUG
            dragonChangeParameter.TestValue();
#endif
        }
    }
}