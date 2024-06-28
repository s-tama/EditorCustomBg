using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;

[InitializeOnLoad]
public class EditorCustomBg
{
    static EditorCustomBg()
    {
        EditorApplication.delayCall += Refresh;
    }

    public static void Refresh()
    {
        foreach (var window in Resources.FindObjectsOfTypeAll<EditorWindow>())
        {
            Debug.Log("Exist: " + window.GetType());
            UpdateBg(window);
        }
    }

    static void UpdateBg(EditorWindow window)
    {
        var rootVisualElement = window.rootVisualElement;

        var settings = EditorCustomBgSettings.LoadAll();

        var windowType = window.GetType();
        var assembly = typeof(EditorWindow).Assembly;

        if (settings == null || settings.Length == 0)
            return;

        foreach (var setting in settings)
        {
            if (string.IsNullOrEmpty(setting.className))
                continue;

            var settingType = assembly.GetType(setting.className);
            if (settingType == null)
                continue;

            if (settingType == windowType)
            {
                var bg = rootVisualElement.Q<Image>(className: "custom-bg");
                if (bg == null)
                {
                    bg = new Image();
                    bg.AddToClassList("custom-bg");
                    rootVisualElement.Insert(0, bg);
                }

                bg.sprite = setting.sprite;
                var bgStyle = bg.style;
                bgStyle.position = Position.Absolute;
                bgStyle.top = new StyleLength(Length.Auto());
                bgStyle.left = new StyleLength(Length.Auto());
                bgStyle.right = 0;
                bgStyle.bottom = 0;
                bgStyle.opacity = setting.opacity;
                bgStyle.width = setting.size.x;
                bgStyle.height = setting.size.y;
            }
        }
    }
}

public class EditorCustomBgMenu
{
    [MenuItem("Project/Refresh Editor")]
    public static void Reload()
    {
        EditorCustomBg.Refresh();
    }
}
