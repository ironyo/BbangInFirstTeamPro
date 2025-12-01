#if UNITY_EDITOR

using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityToolbarExtender;
using Object = UnityEngine.Object;

namespace Utils.Toolbar.Editor
{
    [InitializeOnLoad]
    public class ToolbarScene
    {
        private const string ScenesFilePath = "Scenes";
        private const string Extension = ".unity";

        static ToolbarScene()
        {
            ToolbarExtender.LeftToolbarGUI.Add(0, OnToolbarGUI);
        }

        private static string ClearFirstPathString(string path)
        {
            var isFirstCharIsSlash = path.StartsWith(Path.DirectorySeparatorChar)
                || path.StartsWith(Path.AltDirectorySeparatorChar)
                || path.StartsWith("/");
            if (isFirstCharIsSlash)
                return path[1..];

            return path;
        }

        private static void OnToolbarGUI()
        {
            var content = new GUIContent(SceneManager.GetActiveScene().name);
            var size = EditorStyles.toolbarDropDown.CalcSize(content);

            var filePath = $"{Application.dataPath}{Path.DirectorySeparatorChar}{ClearFirstPathString(ScenesFilePath)}";

            GUILayout.Space(5);

            if (EditorGUILayout.DropdownButton(content, FocusType.Keyboard,
                    EditorStyles.toolbarDropDown, GUILayout.Width(size.x + 5f)) == false) return;

            GenericMenu menu = new();
            MakeSceneMenus(filePath, menu);

            menu.ShowAsContext();
        }

        private static void MakeSceneMenus(string path, GenericMenu menu)
        {
            var scenes = GetScenes(path);

            CreateSceneFileSelectOption(menu, scenes);

            foreach (var scene in scenes)
            {
                if (!scene.EndsWith(Extension)) continue;
                int assetsIndex = scene.IndexOf("Assets", StringComparison.Ordinal);
                if (assetsIndex == -1) continue;

                var relativePath = GetRelativePath(scene);

                var menuPath = GetMenuPath(scene, relativePath);

                menu.AddItem(new GUIContent(menuPath), false, () =>
                {
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                        EditorSceneManager.OpenScene(scene[assetsIndex..]);
                });
            }

        }

        private static string GetRelativePath(string scene)
        {
            var directoryPath = Path.GetDirectoryName(scene);
            var dataPath = $"{Application.dataPath}/{ClearFirstPathString(ScenesFilePath)}".Replace('/', Path.DirectorySeparatorChar);

            var replacePath = directoryPath!.Replace(dataPath, string.Empty);
            return replacePath;
        }

        private static string GetMenuPath(string scene, string replacePath)
        {
            string menuPath;
            var fileName = Path.GetFileName(scene);

            if (replacePath.Length > 0)
            {
                var removeFirstSlashPath = ClearFirstPathString(replacePath);
                menuPath = $"{removeFirstSlashPath}/{fileName}";
            }
            else
            {
                menuPath = fileName;
            }

            menuPath = menuPath.Replace(Extension, string.Empty);
            return menuPath;
        }

        private static string[] GetScenes(string path)
        {
            var scenes = Array.Empty<string>();

            try
            {
                scenes = Directory.GetFiles(path, $"*{Extension}", SearchOption.AllDirectories);
            }
            catch
            {
                // ignored
            }

            return scenes;
        }

        private static void CreateSceneFileSelectOption(GenericMenu menu, string[] scenes)
        {
            var guiContent = new GUIContent("[Select SceneFile]");

            if (scenes.Length > 0)
            {
                var filePath = scenes[0];
                filePath = filePath.Replace(Application.dataPath, "Assets");
                // Get the folder as an object
                var folderObject = AssetDatabase.LoadAssetAtPath(filePath, typeof(Object));

                menu.AddItem(guiContent, false, () =>
                {
                    if (folderObject != null)
                        EditorGUIUtility.PingObject(folderObject);
                });
            }
        }
    }
}
#endif