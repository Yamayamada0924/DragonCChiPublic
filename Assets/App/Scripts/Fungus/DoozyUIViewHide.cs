#if FEATURE_DOOZY
using Doozy.Engine.UI;
#else
using yydlib.CompatibleDoozyUI;
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
#else
        [SerializeField] private CompatibleUIView compatibleUIView;
#endif

        [SerializeField] private bool instant;
        
        public override void OnEnter()
        {
#if !FEATURE_DOOZY
            var uiView = compatibleUIView;
#endif
            if(uiView != null)
            {
                uiView.Hide(instant);
            }
            Continue();
        }
    }
}