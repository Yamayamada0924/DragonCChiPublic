using System.Collections.Generic;
using App.Scripts.GameCommon;
using UnityEngine;
using yydlib;

namespace App.Scripts.FixedData
{
    [CreateAssetMenu(menuName = "MyScriptable/Create WordParameters", order = 0)]
    public class WordParameters : ScriptableObject
    {
        [SerializeField] private List<WordParameter> parameters;
        public List<WordParameter> Parameters => this.parameters;
        
        public WordParameter GetWordParameter(Word wordType)
        {
            int index = (int)wordType;
            GameUtil.Assert(index >= 0 && index < parameters.Count);
            return parameters[index];
        }
        public void TestValue()
        {
#if DEBUG
            GameUtil.Assert(parameters.Count == GameUtil.GetEnumCount(typeof(Word)));
            foreach (var parameter in parameters)
            {
                if(parameter != null)
                {
                    parameter.TestValue();
                }
            }
#endif
        }
    }
}