using System;
using App.Scripts.GameCommon;
#if FEATURE_DOOZY
using Doozy.Engine.UI;
#endif
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace App.Scripts.UI
{
    public class GameActionButton : MonoBehaviour
    {
#if FEATURE_DOOZY
        [SerializeField] private UIButton uiButton;
        public UIButton UIButton => uiButton;
#endif

        [SerializeField] private GameAction gameAction;
        public GameAction GameAction => gameAction;

        [SerializeField] private ObservablePointerEnterTrigger pointerEnterTrigger;
        public IObservable<GameAction> OnPointerEnter => pointerEnterTrigger.OnPointerEnterAsObservable().Select(_ => gameAction);
        
        [SerializeField] private ObservablePointerExitTrigger pointerExitTrigger;
        public IObservable<GameAction> OnPointerExit => pointerExitTrigger.OnPointerExitAsObservable().Select(_ => gameAction);

    }
}