#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EditorSetup : EditorWindow
{
    private static string GenerateKey => Application.productName + "_setup_shown";
    
    private static readonly Dictionary<int, string> LayerDict = new()
    {
        { 3, "Non-Interactable" },
        { 6, "Player" },
        { 7, "Enemy" },
        { 8, "Ground" },
        { 10, "Wall"},
        { 11, "Collectable" },
    };

    [InitializeOnLoadMethod]
    private static void OnInitialize()
    {
        if (EditorPrefs.GetBool(GenerateKey, false) || HasOpenInstances<EditorSetup>()) return;
        ShowWindow();
    }

    public void CreateGUI()
    {
        var label = new Label(
            "Hello,This window will setup the default layers for this package");
        label.style.whiteSpace = new StyleEnum<WhiteSpace>(WhiteSpace.Normal);
        label.style.marginBottom = 10;
        rootVisualElement.Add(label);

        var layerButton = new Button(CheckLayerConflicts);
        layerButton.text = "Setup Layers";
        layerButton.style.marginBottom = 10;
        rootVisualElement.Add(layerButton);


        var label2 = new Label(
            "Once layers have been added, be sure to set them correctly in the 'Static Variables' script that is attached to your GameManager prefab");
        label2.style.whiteSpace = new StyleEnum<WhiteSpace>(WhiteSpace.Normal);
        label2.style.marginBottom = 10;
        rootVisualElement.Add(label2);

        rootVisualElement.style.paddingLeft = 10;
        rootVisualElement.style.paddingRight = 10;
        rootVisualElement.style.paddingTop = 10;
        rootVisualElement.style.paddingBottom = 10;
    }

    [MenuItem("Setup Window")]
    public static void ShowMyEditor() => ShowWindow();

    private static void ShowWindow()
    {
        EditorWindow wnd = GetWindow<EditorSetup>();
        wnd.titleContent = new GUIContent("Setup");

        var size = new Vector2(500, 625);
        wnd.maxSize = size;
        wnd.minSize = size;
        
        EditorPrefs.SetBool(GenerateKey, true);
    }

    private static void CheckLayerConflicts()
    {
        var (_, layersProp) = GetLayerConfiguration();

        var conflicts = LayerDict.Select(l => layersProp.GetArrayElementAtIndex(l.Key)).Any(layerProp => !string.IsNullOrEmpty(layerProp.stringValue));

        if (conflicts)
        {
            if (EditorUtility.DisplayDialogComplex("Layer Conflict", "One or more layers are already defined. Would you like to overwrite them?", "Yes", "No", "Cancel") == 0) AddLayers();
        }
        else
        {
            AddLayers();
        }
    }
    private static void AddLayers()
    {
        var (tagManager, layersProp) = GetLayerConfiguration();

        foreach (var l in LayerDict)
        {
            var layerProp = layersProp.GetArrayElementAtIndex(l.Key);
            layerProp.stringValue = l.Value;
            tagManager.ApplyModifiedProperties();
        }
        
        Debug.Log("Layers have been set");
    }

    private static (SerializedObject, SerializedProperty) GetLayerConfiguration()
    {
        var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        var layersProp = tagManager.FindProperty("layers");
        return (tagManager, layersProp);
    }
}

#endif
