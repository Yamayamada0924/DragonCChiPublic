using System;
using System.Collections.Generic;
using App.Scripts.FixedData;
using App.Scripts.GameCommon;
#if FEATURE_DOOZY
using Doozy.Engine.UI;
#else
using yydlib.CompatibleDoozyUI;
#endif
using Fungus;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using yydlib;

namespace App.Scripts.UI
{

    public class GiveViewBehavior : MonoBehaviour, IDisposable
    {
#if FEATURE_DOOZY
        private UIView _uiView;
#else
        private CompatibleUIView _uiView;
#endif

        [SerializeField] private List<GiveButton> buttons;

        [SerializeField] private List<Button> closeButtons;
        
        [SerializeField] protected ScrollRect scrollRect;
        
        private readonly Subject<ItemType> _onGiveAction = new Subject<ItemType>();
        public IObservable<ItemType> OnGiveAction => this._onGiveAction;

        private readonly Subject<Unit> _onGiveViewShown = new Subject<Unit>();
        public IObservable<Unit> OnGiveViewShown => this._onGiveViewShown;

        private readonly Subject<Unit> _onGiveViewHidden = new Subject<Unit>();
        public IObservable<Unit> OnGiveViewHidden => this._onGiveViewHidden;
        
        private readonly Subject<GiveViewBehavior> _onGiveViewClose = new Subject<GiveViewBehavior>();
        public IObservable<GiveViewBehavior> OnGiveViewClose => this._onGiveViewClose;
        
        private bool _fixScrollBar = true;
        
        private void Awake()
        {
#if FEATURE_DOOZY
            _uiView = gameObject.GetComponent<UIView>();
#else
            _uiView = gameObject.GetComponent<CompatibleUIView>();
#endif

            foreach (var giveButton in buttons)
            {
                var itemParameter = AppUtil.GetItemParameter(giveButton.ItemType);
                giveButton.NameText.text = itemParameter.ItemName;
                giveButton.CostText.text = $"{itemParameter.Money}";
                giveButton.Image.sprite = itemParameter.Sprite;

                giveButton.UIButton.OnClick.OnTrigger.Action += _ =>
                {
                    _uiView.Hide();
                    _onGiveAction.OnNext(giveButton.ItemType);
                };
            }

            _uiView.ShowBehavior.OnFinished.Action += (_ =>
            {
                _onGiveViewShown.OnNext(Unit.Default);
            });
            
            _uiView.HideBehavior.OnStart.Action += (_ =>
            {
                _onGiveViewHidden.OnNext(Unit.Default);
            });

            foreach (var closeButton in closeButtons)
            {
                closeButton.onClick.AddListener(() =>
                {
                    _onGiveViewClose.OnNext(this);
                    _uiView.Hide();
                });
            }
        }
        public void Dispose()
        {
            _onGiveAction?.Dispose();
            _onGiveViewShown?.Dispose();
            _onGiveViewHidden?.Dispose();
            _onGiveViewClose?.Dispose();
        }
        private void Update()
        {
            if (_fixScrollBar && !_uiView.IsVisible)
            {
                scrollRect.verticalNormalizedPosition = 1.0f;
            }
            else if (_fixScrollBar)
            {
                _fixScrollBar = false;
            }
        }
        public void Show()
        {
            _fixScrollBar = true;
            _uiView.Show();
        }

        public void SetButtonEnable(int nowMoney)
        {
            foreach (var giveButton in buttons)
            {
                var itemParameter = AppUtil.GetItemParameter(giveButton.ItemType);
                if(nowMoney >= itemParameter.Money)
                {
                    giveButton.UIButton.EnableButton();
                }
                else
                {
                    giveButton.UIButton.DisableButton();
                }
            }
        }
    }
}
