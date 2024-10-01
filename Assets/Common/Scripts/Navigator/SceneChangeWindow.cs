#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Common.Scripts.Navigator
{
    public class SceneChangeWindow : EditorWindow
    {
        private string[] sceneNames;

        private void OnEnable()
        {
            // Get the names of the scenes in the build settings
            int sceneCount = EditorBuildSettings.scenes.Length;
            sceneNames = new string[sceneCount];

            for (int i = 0; i < sceneCount; i++)
            {
                sceneNames[i] = Path.GetFileNameWithoutExtension(EditorBuildSettings.scenes[i].path);
            }
        }

        private void OnGUI()
        {
            GUILayout.Label("Scene Change", EditorStyles.boldLabel);

            for (int i = 0; i < sceneNames.Length; i++)
            {
                if (GUILayout.Button(sceneNames[i]))
                {
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        EditorSceneManager.OpenScene(EditorBuildSettings.scenes[i].path);
                    }
                }
            }
        }

        [MenuItem("Custom/Scene Change Window")]
        private static void ShowWindow()
        {
            SceneChangeWindow window = GetWindow<SceneChangeWindow>("Scene Change");
            window.Show();
        }
    }
}
#endif
