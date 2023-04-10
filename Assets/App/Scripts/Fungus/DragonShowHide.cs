using App.Scripts.GameView;
using App.Scripts.UI;
using Fungus;
using UnityEngine;

namespace App.Scripts.Fungus
{
    [CommandInfo("Other",
        "DragonShowHide",
        "Set Dragon Show or Hide.")]
    [AddComponentMenu("")]
    public class DragonShowHide : Command
    {
        [SerializeField] private DragonControllerView dragonController;
        
        [SerializeField] private bool show;

        public override void OnEnter()
        {
            if(dragonController != null)
            {
                if (show)
                {
                    dragonController.Show();
                }
                else
                {
                    dragonController.Hide();
                }
            }
            Continue();
        }
    }
}