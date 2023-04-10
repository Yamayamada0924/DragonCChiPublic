#if FEATURE_DOOZY
using Doozy.Engine.UI;
#endif
using Fungus;
using UnityEngine;

namespace App.Scripts.Fungus
{
    [CommandInfo("Other",
        "DoozyUIViewHide",
        "Hide Doozy UIView.")]
    [AddComponentMenu("")]
    public class DoozyUIViewHide : Command
    {
#if FEATURE_DOOZY
        [SerializeField] private UIView uiView;
#endif

        [SerializeField] private bool instant;
        
        public override void OnEnter()
        {
#if FEATURE_DOOZY
            if(uiView != null)
            {
                uiView.Hide(instant);
            }
#endif
            Continue();
        }
    }
}