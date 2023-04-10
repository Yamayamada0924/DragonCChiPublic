using Cysharp.Threading.Tasks;
#if FEATURE_MASTER_AUDIO
using DarkTonic.MasterAudio;
#endif
using Fungus;
using UnityEngine;
using UnityEngine.Playables;

namespace App.Scripts.Fungus
{
    [CommandInfo("Other",
        "PlayableDirector",
        "Play PlayableDirector.")]
    [AddComponentMenu("")]
    public class PlayableDirector : Command
    {
        [SerializeField] private UnityEngine.Playables.PlayableDirector playableDirector;

        [SerializeField] private string audioTrackName;

        [SerializeField] private float defaultVolume;
        
        public override async void OnEnter()
        {
            if(playableDirector != null && playableDirector.playableAsset != null)
            {
                if(audioTrackName != "")
                {
#if FEATURE_MASTER_AUDIO
                    float masterVolumeLevel = MasterAudio.MasterVolumeLevel;
                    AppUtil.ChangeTrackVolume(audioTrackName, playableDirector, defaultVolume * masterVolumeLevel);
#endif
                }
                playableDirector.Play();
                await UniTask.WaitWhile(() => playableDirector.state == PlayState.Playing);
            }
            Continue();
        }
    }
}