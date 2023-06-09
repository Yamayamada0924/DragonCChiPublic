﻿#if FEATURE_DOOZY
using Doozy.Engine.UI;
#else
using yydlib.CompatibleDoozyUI;
#endif
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using yydlib;

namespace App.Scripts.UI
{
    public class LicenceBehavior : MonoBehaviour
    {
        private enum LicenseButton
        {
            Ok,
        }


        [SerializeField]
        protected TextMeshProUGUI text;

        [SerializeField]
        protected RectTransform rectTransformHeight;

        [SerializeField]
        protected ScrollRect scrollRect;

#if FEATURE_DOOZY
        private UIPopup _uiPopup;
#else
        private CompatibleUIPopup _uiPopup;
#endif

        private string _licenseMessage;

        private bool _fixScrollBar = true;

        private const float BottomBuffer = 50.0f;
        
        public void Show()
        {
            _licenseMessage = (Resources.Load($"License", typeof(TextAsset)) as TextAsset)?.text;

            gameObject.SetActive(true);
            
#if FEATURE_DOOZY
            _uiPopup = gameObject.GetComponent<UIPopup>();
#else
            _uiPopup = gameObject.GetComponent<CompatibleUIPopup>();
#endif

            _fixScrollBar = true;

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

            _uiPopup.Data.Buttons[(int)LicenseButton.Ok].OnClick.OnTrigger.Action += _ =>
            {
                _uiPopup.Hide();
            };
        }

        private void OnDestroy()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            if (_fixScrollBar && !_uiPopup.IsVisible)
            {
                scrollRect.verticalNormalizedPosition = 1.0f;
            }
            else if (_fixScrollBar)
            {
                text.SetText(_licenseMessage);
                float preferredHeight = text.GetPreferredValues().y;
                GameUtil.Log($"TextHeight {preferredHeight}");
                rectTransformHeight.sizeDelta = new Vector2(rectTransformHeight.sizeDelta.x, preferredHeight + BottomBuffer);

                _fixScrollBar = false;
            }
        }

    }
}
