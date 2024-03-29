using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class StairTile : MapTile
{
    public bool IsUp;

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Zombieland/Tiles/StairTile")]
    public static void CreateStairTile() {
        string path =
            EditorUtility.SaveFilePanelInProject("Save Stair Tile", "New Stair Tile", "Asset", "Save Stair Tile", "Assets");
        if (path == "") {
            return;
        }

        var instance = ScriptableObject.CreateInstance<StairTile>();
        instance.SectionType = ConfigType.SectionType.Stair;
        AssetDatabase.CreateAsset(instance, path);
    }
#endif
}