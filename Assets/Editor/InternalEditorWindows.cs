using System.Reflection;
using UnityEditor;

public class InternalEditorWindows
{
    public static readonly Assembly EditorAssembly = typeof(EditorWindow).Assembly;
    public static EditorWindow sceneHierarchyWindow => GetEditorWindow("UnityEditor.SceneHierarchyWindow");
    public static EditorWindow gameView => GetEditorWindow("UnityEditor.GameView");
    public static EditorWindow sceneView => GetEditorWindow("UnityEditor.SceneView");
    public static EditorWindow projectBrowser => GetEditorWindow("UnityEditor.ProjectBrowser");
    public static EditorWindow consoleWindow => GetEditorWindow("UnityEditor.ConsoleWindow");
    public static EditorWindow inspectorWindow => GetEditorWindow("UnityEditor.InspectorWindow");
    public static EditorWindow animationWindow => GetEditorWindow("UnityEditor.AnimationWindow");
    public static EditorWindow profilerWindow => GetEditorWindow("UnityEditor.ProfilerWindow");
    public static EditorWindow assetStoreWindow => GetEditorWindow("UnityEditor.AssetStoreWindow");
    public static EditorWindow frameDebuggerWindow => GetEditorWindow("UnityEditor.FrameDebuggerWindow");

    static EditorWindow GetEditorWindow(string name)
    {
        var type = EditorAssembly.GetType(name);
        return EditorWindow.GetWindow(type);
    }
}
