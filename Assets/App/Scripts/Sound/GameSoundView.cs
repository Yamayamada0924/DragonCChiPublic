namespace App.Scripts.Sound
{
    public class GameSoundView
    {
        public void PlayDecide()
        {
            SoundManagerForMasterAudio.Instance.PlaySe(SeId.Decide);
        }
        
        public void PlayPopupOpen()
        {
            SoundManagerForMasterAudio.Instance.PlaySe(SeId.PopOpen);
        }
        
        public void PlayPopupClose()
        {
            SoundManagerForMasterAudio.Instance.PlaySe(SeId.PopClose);
        }
    }
}