using System;
#if FEATURE_DOOZY
using Doozy.Engine.UI;
#else
using yydlib.CompatibleDoozyUI;
#endif
using UnityEngine;
using UnityEngine.EventSystems;
using yydlib;

namespace App.Scripts.UI
{
    public class ClickHideHandlerUIView : MonoBehaviour, IPointerClickHandler
    {
#if FEATURE_DOOZY
        [SerializeField] private UIView uiView;
#else
        [SerializeField] private CompatibleUIView compatibleUIView;
#endif

        [SerializeField] private float wait;

        private float _waitTime;
        
        public void OnPointerClick(PointerEventData eventData)
        {
#if !FEATURE_DOOZY
            var uiView = compatibleUIView;
#endif
            
            GameUtil.Assert(uiView != null);
#if FEATURE_DOOZY
            if (uiView.Visibility == VisibilityState.Visible && _waitTime >= wait)
#else
            if (uiView.Visibility == CompatibleVisibilityState.Visible && _waitTime >= wait)
#endif
            {
                uiView.Hide();
            }
        }

        public void Update()
        {
            _waitTime += Time.deltaTime;
        }
    }
}