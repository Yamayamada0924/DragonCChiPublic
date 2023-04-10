using App.Scripts.GameView;
using App.Scripts.Sound;
using App.Scripts.UI;
using Fungus;
using UnityEngine;

namespace App.Scripts.Fungus
{
    [CommandInfo("Other",
        "PlayBgmMasterAudio",
        "Play Bgm by MasterAudio.")]
    [AddComponentMenu("")]
    public class PlayBgmMasterAudio : Command
    {
        [SerializeField] private BgmId bgmId;
        
        public override void OnEnter()
        {
            SoundManagerForMasterAudio.Instance.PlayBgm(bgmId);

            Continue();
        }
    }
}