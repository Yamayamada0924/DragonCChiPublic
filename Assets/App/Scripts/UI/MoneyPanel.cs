using System;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace App.Scripts.UI
{
    public class MoneyPanel : MonoBehaviour
    {
        [SerializeField] private ObservablePointerEnterTrigger pointerEnterTrigger;
        public IObservable<Unit> OnPointerEnter => pointerEnterTrigger.OnPointerEnterAsObservable().Select(_=> Unit.Default);
        
        [SerializeField] private ObservablePointerExitTrigger pointerExitTrigger;
        public IObservable<Unit> OnPointerExit => pointerExitTrigger.OnPointerExitAsObservable().Select(_=> Unit.Default);

        [SerializeField] private TextMeshProUGUI moneyText;


        public void SetValue(int money)
        {
            moneyText.SetText("{0}", money);
        }
    }
}