using System.Collections.Generic;
using UnityEngine;
using yydlib;

namespace App.Scripts.FixedData
{
    [CreateAssetMenu(menuName = "MyScriptable/Create TalkParameters", order = 0)]
    public class TalkParameters : ScriptableObject
    {
        [SerializeField] private List<TalkParameter> parameters;
        public List<TalkParameter> Parameters => this.parameters;
        
        public TalkParameter GetTalkParameter(int index)
        {
            GameUtil.Assert(index >= 0 && index < parameters.Count);
            return parameters[index];
        }
        
        public int GetCount()
        {
            return parameters.Count;
        }
        
        public void TestValue()
        {
#if DEBUG
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