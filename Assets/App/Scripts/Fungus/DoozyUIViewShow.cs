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
        "DoozyUIViewShow",
        "Show Doozy UIView.")]
    [AddComponentMenu("")]
    public class DoozyUIViewShow : Command
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
                uiView.Show(instant);
            }
            Continue();
        }
    }
}