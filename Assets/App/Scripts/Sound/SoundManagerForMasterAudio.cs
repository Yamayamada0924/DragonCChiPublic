using System.Collections.Generic;
#if FEATURE_MASTER_AUDIO
using DarkTonic.MasterAudio;
#else
using MasterAudio = yydlib.CompatibleMasterAudio.CompatibleMasterAudio;
#endif
using yydlib;

namespace App.Scripts.Sound
{

    public class SoundManagerForMasterAudio: SingletonMonoBehaviour<SoundManagerForMasterAudio>
    {
        private readonly Dictionary<SeId, string> _seMapping = new Dictionary<SeId, string>
        {
            {SeId.Decide, "Button_14"},
            {SeId.PopOpen, "Button_04"},
            {SeId.PopClose, "Button_05"},
            {SeId.Sleep , "maou_se_onepoint09"}
        };
        private readonly Dictionary<BgmId, string> _bgmMapping = new Dictionary<BgmId, string>
        {
            {BgmId.Normal, "NormalBGM"},
            {BgmId.Letter, "maou_bgm_piano34"},
        };
        
        private readonly string PlaylistName = "Default";

        protected override bool DontDestroyOnLoad => true;
        protected override void Init()
        {
        }

        
        public void PlaySe(SeId seId)
        {
            if(!_seMapping.ContainsKey(seId))
            {
                GameUtil.Assert(false);
            }
            MasterAudio.PlaySound(_seMapping[seId]);
        }
        
        public void PlayBgm(BgmId bgmId)
        {
            MasterAudio.StopLoopingCurrentSong();
            
            if(!_bgmMapping.ContainsKey(bgmId))
            {
                GameUtil.Assert(false);
            }

            MasterAudio.StartPlaylistOnClip(PlaylistName, _bgmMapping[bgmId]);
        }
        
        public void StopBgm()
        {
            MasterAudio.StopAllPlaylists();
        }
    }

}