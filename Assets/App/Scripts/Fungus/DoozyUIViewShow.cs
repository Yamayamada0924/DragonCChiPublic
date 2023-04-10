#if FEATURE_DOOZY
using Doozy.Engine.UI;
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
#endif

        [SerializeField] private bool instant;
        
        public override void OnEnter()
        {
#if FEATURE_DOOZY
            if(uiView != null)
            {
                uiView.Show(instant);
            }
#endif
            Continue();
        }
    }
}