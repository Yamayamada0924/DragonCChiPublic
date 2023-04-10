using System;
using App.Scripts.GameCommon;
using TMPro;
using UnityEngine;
using yydlib;

namespace App.Scripts.UI
{
    public class DragonLanguageTextCreator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private void Awake()
        {
            string wordsText = "";
            for(int i = 0 ; i < GameUtil.GetEnumCount(typeof(Word)); i++)
            {
                Word word = (Word)i;
                if(word == Word.None)
                {
                    continue;
                }

                var wordParameter = AppUtil.GetWordParameter(word);
                wordsText += $"{wordParameter.DragonLanguage,-12}{wordParameter.Japanese,-12}\n";
            }

            text.text = wordsText;
        }
    }
}