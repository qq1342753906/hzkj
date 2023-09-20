using UnityEditor;

using UnityEngine;

public class FindReferencesInProjectWindow : EditorWindow
{
  [MenuItem("Assets/Find References In Project", false, 20)]
  private static void FindReferencesInProject()
  {
    Object selectedObject = Selection.activeObject;
    string selectedAssetPath = AssetDatabase.GetAssetPath(selectedObject);

    string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
    int count = 0;

    foreach (string assetPath in allAssetPaths)
    {
      string[] dependencies = AssetDatabase.GetDependencies(assetPath, true);
      foreach (string dependency in dependencies)
      {
        if (dependency == selectedAssetPath)
        {
          count++;
          Debug.Log($"Found reference in: {assetPath}");
        }
      }
    }

    if (count == 0)
    {
      Debug.Log("No references found in the project.");
    }
  }
}
