using System.Collections.Generic;
using App.Scripts.AppDebug;
using UnityEngine;
using UnityEngine.Serialization;
using yydlib;

namespace App.Scripts.FixedData
{
    [CreateAssetMenu(menuName = "MyScriptable/Create WordParameter", order = 0)]
    public class WordParameter : ScriptableObject
    {
        [SerializeField] private string japanese;
        public string Japanese => this.japanese;

        [SerializeField] private string dragonLanguage;
        public string DragonLanguage => this.dragonLanguage;

        private readonly string NoneString = "…";
        
        public string GetJapanese(bool noneIsEmpty = false)
        {
            if(noneIsEmpty && Japanese == NoneString)
            {
                return "";
            }
            return Japanese;
        }
        
        public string GetDragonLanguage(bool noneIsEmpty = false)
        {
            if(noneIsEmpty && DragonLanguage == NoneString)
            {
                return "";
            }
#if DEBUG
            if(DebugFlagManager.Instance.JapaneseMode)
            {
                return $"{DragonLanguage}[{Japanese}]";
            }   
#endif
            return DragonLanguage;
        }

        public string GetTranslationWord(bool withJapanese)
        {
            if(withJapanese)
            {
                return $"{DragonLanguage}[{Japanese}]";
            }
            return DragonLanguage;
        }
        
        public void TestValue()
        {
#if DEBUG
#endif
        }
    }
}