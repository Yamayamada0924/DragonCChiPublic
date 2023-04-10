using App.Scripts.UI;
using Fungus;
using UnityEngine;

namespace App.Scripts.Fungus
{
    [CommandInfo("Other",
        "DayNightView",
        "Set DayNightView.")]
    [AddComponentMenu("")]
    public class SetDayNightView : Command
    {
        [SerializeField] private DayNightView dayNightView;

        [SerializeField] private bool day;
        
        public override void OnEnter()
        {
            if(dayNightView != null)
            {
                if (day)
                {
                    dayNightView.SetDay();
                }
                else
                {
                    dayNightView.SetNight();
                }
            }
            Continue();
        }
    }
}