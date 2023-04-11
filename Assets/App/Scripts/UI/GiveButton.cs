using System;
using App.Scripts.FixedData;
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
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace App.Scripts.UI
{
    public class GiveButton : MonoBehaviour
    {
#if FEATURE_DOOZY
        [SerializeField] private UIButton uiButton;
        public UIButton UIButton => uiButton;
#else
        [SerializeField] private CompatibleUIButton compatibleUIButton;
        public CompatibleUIButton UIButton => compatibleUIButton;
#endif


        [SerializeField] private ItemType itemType;
        public ItemType ItemType => itemType;

        [SerializeField] private TextMeshProUGUI nameText;
        public TextMeshProUGUI NameText => this.nameText;
        
        [SerializeField] private TextMeshProUGUI costText;
        public TextMeshProUGUI CostText => this.costText;
        
        [SerializeField] private Image image;
        public Image Image => this.image;
    }
}