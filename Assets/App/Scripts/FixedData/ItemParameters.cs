using System.Collections.Generic;
using UnityEngine;
using yydlib;

namespace App.Scripts.FixedData
{
    [CreateAssetMenu(menuName = "MyScriptable/Create ItemParameters", order = 0)]
    public class ItemParameters : ScriptableObject
    {
        [SerializeField] private List<ItemParameter> parameters;
        public List<ItemParameter> Parameters => this.parameters;
        
        public ItemParameter GetItemParameter(ItemType itemType)
        {
            int index = (int)itemType;
            GameUtil.Assert(index >= 0 && index < parameters.Count);
            return parameters[index];
        }
        public void TestValue()
        {
#if DEBUG
            GameUtil.Assert(parameters.Count == GameUtil.GetEnumCount(typeof(ItemType)));
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