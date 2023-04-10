using Fungus;
using UnityEngine;

namespace App.Scripts.Fungus
{
    public class FadeVariableSayDialog : SayDialog
    {
        public void SetFadeDuration(float newFadeDuration)
        {
            fadeDuration = newFadeDuration;
        }
    }
}