using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class AirTile : MapTile
{
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Zombieland/Tiles/AirTile")]
    public static void CreateAirTile() {
        string path =
            EditorUtility.SaveFilePanelInProject("Save Air Tile", "New Air Tile", "Asset", "Save Air Tile", "Assets");
        if (path == "") {
            return;
        }

        var instance = ScriptableObject.CreateInstance<AirTile>();
        instance.SectionType = SectionType.Air;
        AssetDatabase.CreateAsset(instance, path);
    }
#endif
}