using System;
using App.Scripts.GameView;
using App.Scripts.UI;
using Fungus;
using UnityEngine;
using yydlib;

namespace App.Scripts.Fungus
{
    [CommandInfo("Other",
        "DragonAnimation",
        "Play Dragon Animation.")]
    [AddComponentMenu("")]
    public class DragonAnimation : Command
    {
        private enum AnimationType {
            Idle,
            Eat,
            Sleep,
        }
        
        [SerializeField] private DragonControllerView dragonController;
        
        [SerializeField] private AnimationType animationType;

        public override void OnEnter()
        {
            if(dragonController != null)
            {
                switch (animationType)
                {
                    case AnimationType.Idle:
                        dragonController.PlayIdle();
                        break;
                    case AnimationType.Eat:
                        dragonController.PlayEat();
                        break;
                    case AnimationType.Sleep:
                        dragonController.PlaySleep();
                        break;
                    default:
                        GameUtil.Assert(false);
                        break;
                }
            }
            Continue();
        }
    }
}