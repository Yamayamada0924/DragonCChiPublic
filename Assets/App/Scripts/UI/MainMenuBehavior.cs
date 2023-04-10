using System;
using System.Collections.Generic;
using App.Scripts.FixedData;
using App.Scripts.GameCommon;
#if FEATURE_DOOZY
using Doozy.Engine.UI;
#endif
using Fungus;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using yydlib;

namespace App.Scripts.UI
{
    public class MainMenuBehavior : MonoBehaviour, IDisposable
    {
#if FEATURE_DOOZY
        private UIView _uiView;
#endif

        [SerializeField] private List<GameActionButton> buttons;

        [SerializeField] private MoneyPanel moneyPanel;

        [SerializeField] private SpeakViewBehavior speakViewBehavior;

        [SerializeField] private GiveViewBehavior giveViewBehavior;
        
        

        private readonly Subject<GameAction> _onGameAction = new Subject<GameAction>();
        public IObservable<GameAction> OnGameAction => this._onGameAction;
        
        private readonly Subject<GameAction> _onActionButtonEnter = new Subject<GameAction>();
        public IObservable<GameAction> OnActionButtonEnter => this._onActionButtonEnter;
        
        private readonly Subject<GameAction> _onActionButtonExit = new Subject<GameAction>();
        public IObservable<GameAction> OnActionButtonExit => this._onActionButtonExit;
        public IObservable<Word> OnSpeak => speakViewBehavior.OnSpeakAction;
        public IObservable<ItemType> OnGiveItem => giveViewBehavior.OnGiveAction;

        private readonly Subject<Unit> _onMoneyEnter = new Subject<Unit>();
        public IObservable<Unit> OnMoneyEnter => this._onMoneyEnter;
        
        private readonly Subject<Unit> _onMoneyExit = new Subject<Unit>();
        public IObservable<Unit> OnMoneyExit => this._onMoneyExit;
        public IObservable<Unit> OnSpeakViewShown => speakViewBehavior.OnSpeakViewShown;
        public IObservable<Unit> OnSpeakViewHidden => speakViewBehavior.OnSpeakViewHidden;
        public IObservable<Unit> OnGiveViewShown => giveViewBehavior.OnGiveViewShown;
        public IObservable<Unit> OnGiveViewHidden => giveViewBehavior.OnGiveViewHidden;

        private readonly Subject<SpeakViewBehavior> _onSpeakViewShow = new Subject<SpeakViewBehavior>();
        public IObservable<SpeakViewBehavior> OnSpeakViewShow => this._onSpeakViewShow;
        public IObservable<SpeakViewBehavior> OnSpeakViewClose => speakViewBehavior.OnSpeakViewClose;
        
        private readonly Subject<GiveViewBehavior> _onGiveViewShow = new Subject<GiveViewBehavior>();
        public IObservable<GiveViewBehavior> OnGiveViewShow => this._onGiveViewShow;
        public IObservable<GiveViewBehavior> OnGiveViewClose => giveViewBehavior.OnGiveViewClose;
        
        private void Awake()
        {
#if FEATURE_DOOZY
            _uiView = gameObject.GetComponent<UIView>();

            for (int i = 0; i < GameUtil.GetEnumCount(typeof(GameAction)); i++)
            {
                GameAction gameAction = (GameAction)i;
                if(gameAction == GameAction.Speak)
                {
                    buttons[i].UIButton.OnClick.OnTrigger.Action += _ =>
                    {
                        _onSpeakViewShow.OnNext(speakViewBehavior);
                    };
                }
                else if(gameAction == GameAction.Give)
                {
                    buttons[i].UIButton.OnClick.OnTrigger.Action += _ =>
                    {
                        _onGiveViewShow.OnNext(giveViewBehavior);
                    };
                }
                else
                {
                    buttons[i].UIButton.OnClick.OnTrigger.Action += _ =>
                    {
                        _onGameAction.OnNext(gameAction);
                    };
                }

                buttons[i].OnPointerEnter.Subscribe(gameAction0 => _onActionButtonEnter.OnNext(gameAction0)).AddTo(this);
                buttons[i].OnPointerExit.Subscribe(gameAction0 => _onActionButtonExit.OnNext(gameAction0)).AddTo(this);
            }
#endif
            moneyPanel.OnPointerEnter.Subscribe(_ => _onMoneyEnter.OnNext(Unit.Default)).AddTo(this);
            moneyPanel.OnPointerExit.Subscribe(_ => _onMoneyExit.OnNext(Unit.Default)).AddTo(this);
        }
        public void Dispose()
        {
            _onGameAction?.Dispose();
            _onActionButtonEnter?.Dispose();
            _onActionButtonExit?.Dispose();
            _onMoneyEnter?.Dispose();
            _onMoneyExit?.Dispose();
        }
        private void OnDestroy()
        {
        }

        public void UpdateGame(CommonParameters.IViewParamReadOnly viewParam)
        {
            moneyPanel.SetValue(viewParam.Money);
        }
        
        public void SetSpeakViewText(bool withJapanese)
        {
            speakViewBehavior.SetTexts(withJapanese);
        }

    }
}
