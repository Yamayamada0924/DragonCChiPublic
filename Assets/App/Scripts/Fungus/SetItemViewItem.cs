using App.Scripts.UI;
using Fungus;
using UnityEngine;

namespace App.Scripts.Fungus
{
    [CommandInfo("Other",
        "SetItemViewItem",
        "Set ItemView item.")]
    [AddComponentMenu("")]
    public class SetItemViewItem : Command
    {
        [SerializeField] private ItemViewBehavior itemViewBehavior;
        
        [SerializeField] private Sprite sprite;

        public override void OnEnter()
        {
            if(itemViewBehavior != null)
            {
                itemViewBehavior.SetImage(sprite);
            }
            Continue();
        }
    }
}