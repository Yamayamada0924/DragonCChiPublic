using UnityEngine;
using UnityEngine.UI;

namespace yydlib.CompatibleDoozyUI
{
    public class CompatibleUIButton : MonoBehaviour
    {
        public CompatibleUIButtonBehavior OnClick { get; }= new CompatibleUIButtonBehavior();

        private Button _button;
        
        private void Awake()
        {
#if !FEATURE_DOOZYUI
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => OnClick.OnTrigger.Action.Invoke(gameObject));
#endif
        }
        
        public void EnableButton()
        {
            if (_button != null)
            {
                _button.interactable = true;
            }
        }
        public void DisableButton()
        {
            if (_button != null)
            {
                _button.interactable = false;
            }
        }
    }
}