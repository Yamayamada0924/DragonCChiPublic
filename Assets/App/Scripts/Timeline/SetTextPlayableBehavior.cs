using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using yydlib;

public class SetTextPlayableBehavior : PlayableBehaviour
{
    public string text;

    // 非ランタイムでも呼ばれるので注意！
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var textMeshProUGUI = playerData as TextMeshProUGUI;
        if(textMeshProUGUI != null)
        {
            textMeshProUGUI.text = text;
        }
    }
}
