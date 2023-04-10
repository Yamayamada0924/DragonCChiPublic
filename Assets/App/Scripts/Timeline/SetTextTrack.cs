using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

namespace App.Scripts.Timeline
{
    [TrackClipType(typeof(SetTextPlayableAsset))]
    [TrackBindingType(typeof(TextMeshProUGUI))]
    public class SetTextTrack : TrackAsset
    {
    }
}