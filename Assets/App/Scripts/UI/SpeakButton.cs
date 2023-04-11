using System;
using App.Scripts.GameCommon;
#if FEATURE_DOOZY
using Doozy.Engine.UI;
#else
using yydlib.CompatibleDoozyUI;
#endif
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;


namespace App.Scripts.UI
{
    public class SpeakButton : MonoBehaviour
    {
#if FEATURE_DOOZY
        [SerializeField] private UIButton uiButton;
        public UIButton UIButton => uiButton;
#else
        [SerializeField] private CompatibleUIButton compatibleUIButton;
        public CompatibleUIButton UIButton => compatibleUIButton;
#endif

        [SerializeField] private Word word;
        public Word Word => word;

        [SerializeField] private TextMeshProUGUI text;
        public TextMeshProUGUI Text => this.text;
        
    }
}