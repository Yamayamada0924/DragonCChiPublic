using System.Collections.Generic;
using UnityEngine;

namespace yydlib.CompatibleMasterAudio
{
    public class CompatibleMasterAudioManager : SingletonMonoBehaviour<CompatibleMasterAudioManager>
    {
        [SerializeField] private List<AudioSource> sounds;
        [SerializeField] private List<AudioSource> playlistSounds;

        protected override bool DontDestroyOnLoad => false;
        protected override void Init()
        {
        }
        public void PlaySound(string soundName, float volume)
        {
            var audioSource = sounds.Find(x => x.name == soundName);
            if (audioSource != null)
            {
                audioSource.volume = volume;
                audioSource.Play();
            }
        }
        public void PlayPlaylistSound(string soundName, float volume)
        {
            var audioSource = playlistSounds.Find(x => x.name == soundName);
            if (audioSource != null)
            {
                audioSource.volume = volume;
                audioSource.Play();
            }
        }
        public void StopLoopingCurrentSong()
        {
            foreach (var audioSource in playlistSounds)
            {
                audioSource.Stop();
            }
        }
        public void StopAllPlaylists()
        {
            foreach (var audioSource in playlistSounds)
            {
                audioSource.Stop();
            }
        }
    }
}