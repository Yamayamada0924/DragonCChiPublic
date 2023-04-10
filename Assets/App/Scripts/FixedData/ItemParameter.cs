using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using yydlib;

namespace App.Scripts.FixedData
{
    [CreateAssetMenu(menuName = "MyScriptable/Create ItemParameter", order = 0)]
    public class ItemParameter : ScriptableObject
    {
        [SerializeField] private string itemName;
        public string ItemName => this.itemName;

        [SerializeField] private ItemKind itemKind;
        public ItemKind ItemKind => this.itemKind;

        [SerializeField] private int money;
        public int Money => this.money;

        [SerializeField] private Sprite sprite;
        public Sprite Sprite => this.sprite;

        [SerializeField] private DragonChangeParameter dragonChangeParameter;
        public DragonChangeParameter DragonChangeParameter => this.dragonChangeParameter;

        public void TestValue()
        {
#if DEBUG
            GameUtil.Assert(Money >= 0);
            GameUtil.Assert(itemKind != ItemKind.None);
            dragonChangeParameter.TestValue();
#endif
        }
    }
}