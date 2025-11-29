#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

namespace Utils.Toolbar
{
    public class ToolbarToggle
    {
        private readonly string _label;
        private readonly string _key;

        public bool IsOn
        {
            get => _isOn;
            set
            {
                if (_isOn == value) return;

                _isOn = value;
                EditorPrefs.SetBool(_key, _isOn);
                OnValueChanged?.Invoke(_isOn);
            }
        }

        public event Action<bool> OnValueChanged = null!;
        private bool _isOn;

        public ToolbarToggle(string label)
        {
            _label = label;
            _key = label.Replace(" ", string.Empty);

            Initialize();
        }

        public void DrawToggle()
        {
            var content = new GUIContent(_label);
            var size = EditorStyles.toolbarDropDown.CalcSize(content);

            EditorGUILayout.LabelField(content, GUILayout.Width(size.x));

            var aiOn = EditorPrefs.GetBool(_key, false);
            IsOn = EditorGUILayout.Toggle(aiOn, GUILayout.Width(15));
        }

        public void Save()
        {
            EditorPrefs.SetBool(_key, IsOn);
        }

        private void Initialize()
        {
            var value = EditorPrefs.GetBool(_key, false);
            IsOn = value;
        }
    }

    [InitializeOnLoad]
    public static class ToolbarLeftToggles
    {
        private static readonly ToolbarToggle StartInNewMenuToggle = new("Start In Main Scene");

        static ToolbarLeftToggles()
        {
            ToolbarExtender.LeftToolbarGUI.Add(2, OnToolbarGUI);

            StartInNewMenuToggle.OnValueChanged += OnValueChanged;

            OnValueChanged(StartInNewMenuToggle.IsOn);
        }

        private static void OnToolbarGUI()
        {
            StartInNewMenuToggle.DrawToggle();
        }

        private static void OnValueChanged(bool value)
        {
            EditorSceneManager.playModeStartScene =
                value ? AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Scenes/Main.unity") : null;
        }
    }

#endif
}
