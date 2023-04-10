using Cysharp.Threading.Tasks;
#if FEATURE_DOOZY
using Doozy.Engine.UI;
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
#endif

        public override async void OnEnter()
        {
#if FEATURE_DOOZY
            if(uiView != null)
            {
                await UniTask.WaitUntil(() => uiView.Visibility == VisibilityState.NotVisible);
            }
#endif
            Continue();
        }
    }
}