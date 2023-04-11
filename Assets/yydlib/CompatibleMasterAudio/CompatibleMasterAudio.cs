using UnityEngine;

namespace yydlib.CompatibleMasterAudio
{
    public static class CompatibleMasterAudio
    {
        public static float MasterVolumeLevel { get; set; } = 1.0f;
        public static float PlaylistMasterVolume { get; set; } = 1.0f;
        
        public static void PlaySound(string soundName)
        {
            CompatibleMasterAudioManager.Instance.PlaySound(soundName, MasterVolumeLevel);
        }
        public static void StartPlaylistOnClip(string playlistName, string clipName)
        {
            CompatibleMasterAudioManager.Instance.PlayPlaylistSound(clipName, PlaylistMasterVolume);
        }
        public static void StopLoopingCurrentSong()
        {
            CompatibleMasterAudioManager.Instance.StopLoopingCurrentSong();
        }
        public static void StopAllPlaylists()
        {
            CompatibleMasterAudioManager.Instance.StopAllPlaylists();
        }
    }
}