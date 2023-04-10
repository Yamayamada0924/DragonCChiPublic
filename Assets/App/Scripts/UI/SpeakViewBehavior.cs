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
    public class SpeakViewBehavior : MonoBehaviour, IDisposable
    {
#if FEATURE_DOOZY
        private UIView _uiView;
#endif

        [SerializeField] private List<SpeakButton> buttons;

        [SerializeField] private List<Button> closeButtons;
        
        [SerializeField] protected ScrollRect scrollRect;

        private readonly Subject<Word> _onSpeakAction = new Subject<Word>();
        public IObservable<Word> OnSpeakAction => this._onSpeakAction;

        private readonly Subject<Unit> _onSpeakViewShown = new Subject<Unit>();
        public IObservable<Unit> OnSpeakViewShown => this._onSpeakViewShown;

        private readonly Subject<Unit> _onSpeakViewHidden = new Subject<Unit>();
        public IObservable<Unit> OnSpeakViewHidden => this._onSpeakViewHidden;

        private readonly Subject<SpeakViewBehavior> _onSpeakViewClose = new Subject<SpeakViewBehavior>();
        public IObservable<SpeakViewBehavior> OnSpeakViewClose => this._onSpeakViewClose;
        
        private bool _fixScrollBar = true;
        
        private void Awake()
        {
#if FEATURE_DOOZY
            _uiView = gameObject.GetComponent<UIView>();

            foreach (var speakButton in buttons)
            {
                var wordParameter = AppUtil.GetWordParameter(speakButton.Word);
                speakButton.Text.text = wordParameter.GetDragonLanguage();

                speakButton.UIButton.OnClick.OnTrigger.Action += _ =>
                {
                    _uiView.Hide();
                    _onSpeakAction.OnNext(speakButton.Word);
                };
            }

            
            _uiView.ShowBehavior.OnFinished.Action += (_ =>
            {
                _onSpeakViewShown.OnNext(Unit.Default);
            });
            
            _uiView.HideBehavior.OnStart.Action += (_ =>
            {
                _onSpeakViewHidden.OnNext(Unit.Default);
            });

            foreach (var closeButton in closeButtons)
            {
                closeButton.onClick.AddListener(() =>
                {
                    _onSpeakViewClose.OnNext(this);
                    _uiView.Hide();
                });
            }
#endif 
        }
        public void Dispose()
        {
            _onSpeakAction?.Dispose();
            _onSpeakViewShown?.Dispose();
            _onSpeakViewHidden?.Dispose();
            _onSpeakViewClose?.Dispose();
        }
        private void Update()
        {
#if FEATURE_DOOZY
            if (_fixScrollBar && !_uiView.IsVisible)
            {
                scrollRect.verticalNormalizedPosition = 1.0f;
            }
            else if (_fixScrollBar)
            {
                _fixScrollBar = false;
            }
#endif
        }
        public void Show()
        {
#if FEATURE_DOOZY
            _fixScrollBar = true;
            _uiView.Show();
#endif
        }
        
        public void SetTexts(bool withJapanese)
        {
            foreach (var speakButton in buttons)
            {
                var wordParameter = AppUtil.GetWordParameter(speakButton.Word);
                speakButton.Text.text = wordParameter.GetTranslationWord(withJapanese);
            }
        }
    }
}
