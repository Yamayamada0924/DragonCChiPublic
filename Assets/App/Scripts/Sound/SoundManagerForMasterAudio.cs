using System.Collections.Generic;
#if FEATURE_MASTER_AUDIO
using DarkTonic.MasterAudio;
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
#if FEATURE_MASTER_AUDIO
            MasterAudio.PlaySound(_seMapping[seId]);
#endif
        }
        
        public void PlayBgm(BgmId bgmId)
        {
#if FEATURE_MASTER_AUDIO
            MasterAudio.StopLoopingCurrentSong();
            
            if(!_bgmMapping.ContainsKey(bgmId))
            {
                GameUtil.Assert(false);
            }

            MasterAudio.StartPlaylistOnClip(PlaylistName, _bgmMapping[bgmId]);
#endif
        }
        
        public void StopBgm()
        {
#if FEATURE_MASTER_AUDIO
            MasterAudio.StopAllPlaylists();
#endif
        }
    }

}