using UnityEditor;

namespace UnityEngine.UI
{
    /// <summary>
    /// 謠冗判縺励↑縺・乗・Image繧ｳ繝ｳ繝昴・繝阪Φ繝・
    /// </summary>
    [AddComponentMenu("UI/NonRenderImage", 14)]
    public class NoRenderImage : Graphic
    {
        protected override void OnPopulateMesh(VertexHelper vh) { vh.Clear(); }

#if UNITY_EDITOR
        [UnityEditor.CustomEditor(typeof(NoRenderImage))]
        class NonRenderImageEditor : UnityEditor.Editor
        {
            public override void OnInspectorGUI() { }

            private static Vector2 s_ImageElementSize = new Vector2(100f, 100f);

            [MenuItem("GameObject/UI/NoRender Image", false, 2003)]
            public static void CreateNoRenderImage()
            {
                // 驕ｸ謚樒憾諷九・GameObject繧貞叙蠕励☆繧・
                var parent = Selection.activeGameObject?.transform;
                // 隕ｪ繧・･門・縺ｫCanvas縺悟ｭ伜惠縺励↑縺・ｴ蜷・
                if (parent == null || parent.GetComponentInParent<Canvas>() == null)
                {
                    // 譁ｰ隕修anvas縺ｮ逕滓・
                    var canvas = new GameObject("Canvas");
                    canvas.transform.SetParent(parent);
                    // Canvas縺ｮ蛻晄悄蛹・
                    canvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                    canvas.AddComponent<CanvasScaler>();
                    canvas.AddComponent<GraphicRaycaster>();
                    // 隕ｪ縺ｮ莉倥￠譖ｿ縺・
                    parent = canvas.transform;
                }
                var go = new GameObject("NoRenderImage");
                // RectTransform縺ｮ蛻晄悄蛹・
                var rectTransform = go.AddComponent<RectTransform>();
                // 隕ｪ繧ｳ繝ｳ繝昴・繝阪Φ繝医・謖・ｮ・(null縺ｮ蝣ｴ蜷医・繝ｫ繝ｼ繝医↓縺ｪ繧九・縺ｧ蝠城｡後↑縺・
                rectTransform.SetParent(parent);
                rectTransform.sizeDelta = s_ImageElementSize;
                rectTransform.anchoredPosition = Vector2.zero;
                // 逕滓・縺励◆GameObject繧帝∈謚樒憾諷九↓縺吶ｋ
                Selection.activeGameObject = go;
                // 繧ｳ繝ｳ繝昴・繝阪Φ繝医・霑ｽ蜉
                go.AddComponent<NoRenderImage>();
            }
        }
#endif
    }
}
