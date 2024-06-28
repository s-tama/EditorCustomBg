using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Editor Custom Bg Settings", fileName = "EditorCustomBgSettings")]
public class EditorCustomBgSettings : ScriptableObject
{
    public string className;
    public Sprite sprite;
    public Vector2 size;
    [Range(0, 1f)] public float opacity;

    public static EditorCustomBgSettings[] LoadAll()
    {
        var guids =  AssetDatabase.FindAssets($"t:{typeof(EditorCustomBgSettings).Name}");
        var assets = new EditorCustomBgSettings[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            assets[i] = AssetDatabase.LoadAssetAtPath<EditorCustomBgSettings>(path);
        }
        return assets;
    }
}

[CustomEditor(typeof(EditorCustomBgSettings))]
public class EditorCustomBgSettingsEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        var visualElement = new VisualElement();
        InspectorElement.FillDefaultInspector(visualElement, serializedObject, this);

        var button = new Button(OnApplyButtonClicked)
        {
            text = "Apply"
        };
        var style = button.style;
        style.position = Position.Relative;
        style.marginLeft = new StyleLength(StyleKeyword.Auto);
        style.marginTop = new StyleLength(10);
        style.width = 100;
        style.height = new StyleLength(StyleKeyword.Auto);
        visualElement.Add(button);

        return visualElement;
    }

    void OnApplyButtonClicked()
    {
        EditorCustomBg.Refresh();
    }
}
