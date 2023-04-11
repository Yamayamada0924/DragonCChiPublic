using UnityEngine;

namespace yydlib.CompatibleDoozyUI
{
    public class CompatibleUIPopup : MonoBehaviour
    {
        public CompatibleUIPopupContentReferences Data;
        
        public CompatibleUIViewBehavior ShowBehavior { get; }= new CompatibleUIViewBehavior();
        public CompatibleUIViewBehavior HideBehavior { get; }= new CompatibleUIViewBehavior();
        
        public bool IsVisible => gameObject.activeSelf;
        public bool IsHidden => !gameObject.activeSelf;
        public bool IsHiding => false;
        public bool IsShowing => false;
        
        private void Awake()
        {
#if !FEATURE_DOOZYUI
            gameObject.SetActive(false);
#endif
        }

        public void Show()
        {
            gameObject.SetActive(true);
            ShowBehavior.OnStart.Action.Invoke(gameObject);
            ShowBehavior.OnFinished.Action.Invoke(gameObject);
        }
        public void Hide()
        {
            HideBehavior.OnStart.Action.Invoke(gameObject);
            HideBehavior.OnFinished.Action.Invoke(gameObject);
            gameObject.SetActive(false);
        }
    }
}