using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace yydlib.CompatibleDoozyUI
{
    public class CompatibleUIView : MonoBehaviour
    {
        [SerializeField] private bool hideOnWake;

        public CompatibleUIViewBehavior ShowBehavior { get; } = new CompatibleUIViewBehavior();
        public CompatibleUIViewBehavior HideBehavior { get; } = new CompatibleUIViewBehavior();
        
        public bool IsVisible => _canvas.enabled;
        public bool IsHidden => !_canvas.enabled;
        public bool IsHiding => false;
        public bool IsShowing => false;
        
        private Canvas _canvas;
        
        public CompatibleVisibilityState Visibility
        {
            get
            {
                if (IsVisible)
                {
                    return CompatibleVisibilityState.Visible;
                }
                else if (IsHiding)
                {
                    return CompatibleVisibilityState.Hiding;
                }
                else if (IsShowing)
                {
                    return CompatibleVisibilityState.Showing;
                }
                else
                {
                    return CompatibleVisibilityState.NotVisible;
                }
            }
        }
        
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
#if !FEATURE_DOOZYUI
            if(hideOnWake)
            {
                _canvas.enabled = false;
            }
#endif
        }

        public void Show(bool instant = false)
        {
            _canvas.enabled = true;
            ShowBehavior.OnStart.Action.Invoke(gameObject);
            ShowBehavior.OnFinished.Action.Invoke(gameObject);
        }
        public void Hide(bool instant = false)
        {
            HideBehavior.OnStart.Action.Invoke(gameObject);
            HideBehavior.OnFinished.Action.Invoke(gameObject);
            _canvas.enabled = false;
        }
    }
}