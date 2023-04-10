using TMPro;
using UnityEngine;
using UnityEngine.Playables;

namespace App.Scripts.Timeline
{
    public class SetTextPlayableAsset : PlayableAsset
    {
        [SerializeField] private string text;
        

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            // PlayableBehaviourを元にPlayableを作る
            var playable = ScriptPlayable<SetTextPlayableBehavior>.Create(graph);
            // PlayableBehaviourを取得する
            var behaviour = playable.GetBehaviour();
            
            behaviour.text = text;

            return playable;
        }
    }
}
