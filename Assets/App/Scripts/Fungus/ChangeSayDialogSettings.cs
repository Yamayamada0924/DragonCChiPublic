using Fungus;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Fungus
{
    [CommandInfo("Other",
        "ChangeSayDialogSettings",
        "Change SayDialog Settings.")]
    [AddComponentMenu("")]
    public class ChangeSayDialogSettings : Command
    {
        [SerializeField] private FadeVariableSayDialog sayDialog;

        [SerializeField] private Image nameImage;
        [SerializeField] private Text nameText;

        [SerializeField] private bool isShowName = true;
        [SerializeField] private float fadeDuration = 0.0f;
        
        
        public override void OnEnter()
        {
            if(sayDialog != null)
            {
                if (nameImage != null)
                {
                    nameImage.enabled = isShowName;
                }

                if (nameText != null)
                {
                    nameText.enabled = isShowName;
                }

                sayDialog.SetFadeDuration(fadeDuration);
            }
            Continue();
        }
    }
}