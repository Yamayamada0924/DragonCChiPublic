using Cysharp.Threading.Tasks;
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
        "DoozyUIViewWaitHidden",
        "Wait Doozy UIView hidden.")]
    [AddComponentMenu("")]
    public class DoozyUIViewWaitHidden : Command
    {
#if FEATURE_DOOZY
        [SerializeField] private UIView uiView;
#else
        [SerializeField] private CompatibleUIView compatibleUIView;
#endif

        public override async void OnEnter()
        {
#if FEATURE_DOOZY
            if(uiView != null)
            {
                await UniTask.WaitUntil(() => uiView.Visibility == VisibilityState.NotVisible);
            }
#else
            var uiView = compatibleUIView;
            if(uiView != null)
            {
                await UniTask.WaitUntil(() => uiView.Visibility == CompatibleVisibilityState.NotVisible);
            }
#endif
            Continue();
        }
    }
}