using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

public class ToolWindow : EditorWindow
{
    private EditorUtil.SelectFolderParam selectFolderParam = new EditorUtil.SelectFolderParam();

    [MenuItem("Tools/ToolWindow")]
    static void Init()
    {
        ToolWindow window = (ToolWindow)EditorWindow.GetWindow(typeof(ToolWindow));
        window.Show();
    }

    private void OnEnable()
    {
        EditorUtil.SelectFolderSetup(ref selectFolderParam);
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        GUILayout.Label("シーン");

        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("タイトル"))
            {
                EditorUtil.OpenScene("Assets/Scenes/Title.unity");
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(8);

        EditorGUILayout.Space(8);
        GUILayout.Label("ProjectWindow選択");

        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Scripts"))
            {
                EditorUtil.SelectFolder("Assets/Scripts", selectFolderParam);
            }
            if (GUILayout.Button("Editor"))
            {
                EditorUtil.SelectFolder("Assets/Editor", selectFolderParam);
            }
            if (GUILayout.Button("UI"))
            {
                EditorUtil.SelectFolder("Assets/UI", selectFolderParam);
            }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Shaders"))
            {
                EditorUtil.SelectFolder("Assets/Shaders", selectFolderParam);
            }
            if (GUILayout.Button("?"))
            {
            }
            if (GUILayout.Button("?"))
            {
            }
		}
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(8);
        GUILayout.Label("アセットを開く");
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("MiscParam"))
            {
                EditorUtil.OpenAsset("Assets/AddressableAssets/Misc/MiscParameters.asset");
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(8);
        GUILayout.Label("操作");

        if (GUILayout.Button("Asset全保存"))
        {
            AssetDatabase.SaveAssets();
        }

        EditorGUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Capture"))
            {
                EditorUtil.Capture();
            }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

    }

}
