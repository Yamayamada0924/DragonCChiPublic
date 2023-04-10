using System;
#if FEATURE_DOOZY
using Doozy.Engine.UI;
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
#endif

        [SerializeField] private float wait;

        private float _waitTime;
        
        public void OnPointerClick(PointerEventData eventData)
        {
#if FEATURE_DOOZY
            GameUtil.Assert(uiView != null);
            if (uiView.Visibility == VisibilityState.Visible && _waitTime >= wait)
            {
                uiView.Hide();
            }
#endif
        }

        public void Update()
        {
            _waitTime += Time.deltaTime;
        }
    }
}