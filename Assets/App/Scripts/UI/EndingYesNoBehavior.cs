using System;
#if FEATURE_DOOZY
using Doozy.Engine.UI;
#else
using yydlib.CompatibleDoozyUI;
#endif
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using yydlib;

namespace App.Scripts.UI
{
    public class EndingYesNoBehavior : MonoBehaviour
    {
        private enum EndingYesNoButton
        {
            Yes,
            No,
        }

#if FEATURE_DOOZY
        private UIPopup _uiPopup;
#else
        private CompatibleUIPopup _uiPopup;
#endif

        private readonly Subject<Unit> _onEndingStart = new Subject<Unit>();
        public IObservable<Unit> OnEndingStart => this._onEndingStart;
        
        
        public void Show()
        {
            gameObject.SetActive(true);
            if (_uiPopup == null)
            {
#if FEATURE_DOOZY
                _uiPopup = gameObject.GetComponent<UIPopup>();
#else
                _uiPopup = gameObject.GetComponent<CompatibleUIPopup>();
#endif
            }

            _uiPopup.Show();
        }

        // Start is called before the first frame update
        private void Awake()
        {
#if FEATURE_DOOZY
            _uiPopup = gameObject.GetComponent<UIPopup>();
#else
            _uiPopup = gameObject.GetComponent<CompatibleUIPopup>();
#endif

            _uiPopup.Data.Buttons[(int)EndingYesNoButton.Yes].OnClick.OnTrigger.Action += _ =>
            {
                _uiPopup.Hide();
                _onEndingStart.OnNext(Unit.Default);
            };
            _uiPopup.Data.Buttons[(int)EndingYesNoButton.No].OnClick.OnTrigger.Action += _ =>
            {
                _uiPopup.Hide();
            };
        }

        private void OnDestroy()
        {
        }



    }
}
