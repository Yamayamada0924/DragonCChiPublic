using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Reflection;

public static class EditorUtil
{

    public static void OpenScene(string scenePath)
    {
        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            return;
        }

        EditorSceneManager.OpenScene(scenePath);
    }

    public static void OpenAsset(string assetPath)
    {
        var asset = AssetDatabase.LoadMainAssetAtPath(assetPath);
        AssetDatabase.OpenAsset(asset);
    }

    public static void SelectAsset(string assetPath)
    {
        var asset = AssetDatabase.LoadMainAssetAtPath(assetPath);
        Selection.activeObject = asset;
    }

    public struct SelectFolderParam
    {
        public EditorWindow projectWindow;
        public System.Type viewModeType;
        public MethodInfo methodInfoGetFolderInstanceIDs;
        public MethodInfo methodInfoSetFolderSelection;
        public MethodInfo methodInfoInitViewMode;
    }

    public static void SelectFolderSetup(ref SelectFolderParam selectFolderParam)
    {
        //メソッド検索オプションを設定
        var flag = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        //UnityEditor.dllを取得
        var asm = Assembly.Load("UnityEditor");

        //ProjectBrowserクラスを取得
        var projectWindowType = asm.GetType("UnityEditor.ProjectBrowser");

        //列挙体 ProjectBrowser.ViewMode を取得
        selectFolderParam.viewModeType = asm.GetType("UnityEditor.ProjectBrowser+ViewMode");

        //フォルダIDを取得するメソッドを取得
        selectFolderParam.methodInfoGetFolderInstanceIDs = projectWindowType.GetMethod("GetFolderInstanceIDs", flag);

        //任意IDのフォルダを選択するメソッドを取得
        selectFolderParam.methodInfoSetFolderSelection = projectWindowType.GetMethod("SetFolderSelection", flag, null, new System.Type[] { typeof(int[]), typeof(bool) }, null );

        //ビューモードを設定するメソッドを取得
        selectFolderParam.methodInfoInitViewMode = projectWindowType.GetMethod("InitViewMode", flag);

        //プロジェクトウィンドウを取得
        selectFolderParam.projectWindow = EditorWindow.GetWindow(projectWindowType, false, "Project", false);
    }

    public static void SelectFolder(string path, SelectFolderParam selectFolderParam)
    {
        //プロジェクトウィンドウにフォーカス
        selectFolderParam.projectWindow.Focus();

        //プロジェクトウィンドウを２カラム表示に変更
        selectFolderParam.methodInfoInitViewMode.Invoke(selectFolderParam.projectWindow, new[] { System.Enum.GetValues(selectFolderParam.viewModeType).GetValue(1) });

        //渡されたパスのフォルダIDを取得
        int[] folderids = (int[])selectFolderParam.methodInfoGetFolderInstanceIDs.Invoke(null, new[] { new string[] { path } });

        //取得したIDのフォルダを選択（第二引数はとりあえずfalse）
        selectFolderParam.methodInfoSetFolderSelection.Invoke(selectFolderParam.projectWindow, new object[] { folderids, false });
    }

    public static void Capture()
    {
        System.DateTime now = System.DateTime.Now;
        string fileName = string.Format($"{now.Year}-{now.Month:00}-{now.Day:00}-{now.Hour:00}{now.Minute:00}{now.Second:00}.png");

        ScreenCapture.CaptureScreenshot($"{Application.dataPath}/../ScreenShots/{fileName}");

        Debug.Log($"ScreenCapture wrote ScreenShots/{fileName}");
    }

}
