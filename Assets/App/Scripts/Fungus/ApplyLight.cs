using App.Scripts.GameView;
using App.Scripts.UI;
using Fungus;
using UnityEngine;

namespace App.Scripts.Fungus
{
    [CommandInfo("Other",
        "ApplyLight",
        "Apply Light.")]
    [AddComponentMenu("")]
    public class ApplyLight : Command
    {
        public static readonly string Key = "OnApplyLight";
        public override void OnEnter()
        {
            GetFlowchart().SetBooleanVariable(Key, true);

            Continue();
        }
    }
}