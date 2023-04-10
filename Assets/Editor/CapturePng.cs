using System.IO;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CaptureAsset
{
    private static int _captureCount = 0;
    
    [MenuItem("Tools/Screen Capture Trans")]
    public static void CaptureTrans()
    {
        // Game 画面のサイズを取得
        var size = new Vector2Int((int)Handles.GetMainGameViewSize().x, (int)Handles.GetMainGameViewSize().y);
        var render = new RenderTexture(size.x, size.y, 32);
        var texture = new Texture2D(size.x, size.y, TextureFormat.RGBA32, false);
        var camera = Camera.main;
        if (camera == null || !camera.gameObject.activeSelf)
        {
            camera = GameObject.FindObjectOfType<Camera>();
        }

        try
        {
            // カメラ画像を RenderTexture に描画
            camera.targetTexture = render;
            camera.Render();

            // RenderTexture の画像を読み取る
            RenderTexture.active = render;
            texture.ReadPixels(new Rect(0, 0, size.x, size.y), 0, 0);
            texture.Apply();
        }
        finally
        {
            camera.targetTexture = null;
            RenderTexture.active = null;
        }

        // PNG 画像としてファイル保存
        File.WriteAllBytes(
            $"{Application.dataPath}/image{_captureCount:00}.png",
            texture.EncodeToPNG());
        Debug.Log($"Write { Application.dataPath}/ image{_captureCount:00}.png");

        _captureCount++;
    }
}
