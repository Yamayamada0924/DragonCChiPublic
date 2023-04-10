using App.Scripts.GameView;
using App.Scripts.UI;
using Fungus;
using UnityEngine;

namespace App.Scripts.Fungus
{
    [CommandInfo("Other",
        "DragonParticleShowHide",
        "Set Dragon Particle Show or Hide.")]
    [AddComponentMenu("")]
    public class DragonParticleShowHide : Command
    {
        [SerializeField] private DragonControllerView dragonController;
        
        [SerializeField] private bool show;

        public override void OnEnter()
        {
            if(dragonController != null)
            {
                if (show)
                {
                    dragonController.ShowParticle();
                }
                else
                {
                    dragonController.HideParticle();
                }
            }
            Continue();
        }
    }
}