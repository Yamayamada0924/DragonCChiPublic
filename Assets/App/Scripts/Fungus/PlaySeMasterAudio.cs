using App.Scripts.GameView;
using App.Scripts.Sound;
using App.Scripts.UI;
using Fungus;
using UnityEngine;

namespace App.Scripts.Fungus
{
    [CommandInfo("Other",
        "PlaySeMasterAudio",
        "Play Se by MasterAudio.")]
    [AddComponentMenu("")]
    public class PlaySeMasterAudio : Command
    {
        [SerializeField] private SeId seId;
        
        public override void OnEnter()
        {
            SoundManagerForMasterAudio.Instance.PlaySe(seId);

            Continue();
        }
    }
}