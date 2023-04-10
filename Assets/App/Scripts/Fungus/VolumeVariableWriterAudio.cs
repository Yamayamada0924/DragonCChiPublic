using Fungus;
using UnityEngine;

namespace App.Scripts.Fungus
{
    public class VolumeVariableWriterAudio : WriterAudio
    {
        public void SetVolume(float volume)
        {
            this.volume = volume;
        }
    }
}