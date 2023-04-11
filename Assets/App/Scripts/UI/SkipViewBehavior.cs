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
    public class SkipViewBehavior : MonoBehaviour, IDisposable
    {
#if FEATURE_DOOZY
        private UIView _uiView;
#else
        private CompatibleUIView _uiView;
#endif

        
#if FEATURE_DOOZY
        [SerializeField] private UIButton skipButton;
#else
        [SerializeField] private CompatibleUIButton compatibleSkipButton;
#endif

        private readonly Subject<Unit> _onSkip = new Subject<Unit>();
        public IObservable<Unit> OnSkip => this._onSkip;
        
        private void Awake()
        {
#if FEATURE_DOOZY
            _uiView = gameObject.GetComponent<UIView>();
#else
            _uiView = gameObject.GetComponent<CompatibleUIView>();
            var skipButton = compatibleSkipButton;
#endif

            skipButton.OnClick.OnTrigger.Action += (_ =>
            {
                _onSkip.OnNext(Unit.Default);
                _uiView.Hide();
            });
        }
        public void Dispose()
        {
            _onSkip?.Dispose();
        }
    }
}
