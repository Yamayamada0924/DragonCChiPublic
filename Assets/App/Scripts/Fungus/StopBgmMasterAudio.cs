using App.Scripts.GameView;
using App.Scripts.Sound;
using App.Scripts.UI;
using Fungus;
using UnityEngine;

namespace App.Scripts.Fungus
{
    [CommandInfo("Other",
        "StopBgmMasterAudio",
        "Stop Bgm by MasterAudio.")]
    [AddComponentMenu("")]
    public class StopBgmMasterAudio : Command
    {
        
        public override void OnEnter()
        {
            SoundManagerForMasterAudio.Instance.StopBgm();

            Continue();
        }
    }
}