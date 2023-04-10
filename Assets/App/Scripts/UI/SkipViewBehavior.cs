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
    public class SkipViewBehavior : MonoBehaviour, IDisposable
    {
#if FEATURE_DOOZY
        private UIView _uiView;

        [SerializeField] private UIButton skipButton;
#endif

        private readonly Subject<Unit> _onSkip = new Subject<Unit>();
        public IObservable<Unit> OnSkip => this._onSkip;
        
        private void Awake()
        {
#if FEATURE_DOOZY
            _uiView = gameObject.GetComponent<UIView>();

            skipButton.OnClick.OnTrigger.Action += (_ =>
            {
                _onSkip.OnNext(Unit.Default);
                _uiView.Hide();
            });
#endif
        }
        public void Dispose()
        {
            _onSkip?.Dispose();
        }
    }
}
