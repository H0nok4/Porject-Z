using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class FloorTile : MapTile
{
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Zombieland/Tiles/FloorTile")]
    public static void CreateFloorTile() {
        string path =
            EditorUtility.SaveFilePanelInProject("Save Floor Tile", "New Floor Tile", "Asset", "Save Floor Tile", "Assets");
        if (path == "") {
            return;
        }

        var instance = ScriptableObject.CreateInstance<FloorTile>();
        instance.SectionType = ConfigType.SectionType.Floor;
        AssetDatabase.CreateAsset(instance, path);
    }
#endif
}