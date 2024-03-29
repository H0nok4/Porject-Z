 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class WallTile : MapTile
{


#if UNITY_EDITOR
    [MenuItem("Assets/Create/Zombieland/Tiles/WallTile")]
    public static void CreateWallTile() {
        string path =
            EditorUtility.SaveFilePanelInProject("Save Wall Tile", "New Wall Tile", "Asset", "Save Wall Tile", "Assets");
        if (path == "") {
            return;
        }

        var instance = ScriptableObject.CreateInstance<WallTile>();
        instance.SectionType = ConfigType.SectionType.Wall;
        instance.IsWalkable = false;
        AssetDatabase.CreateAsset(instance, path);
    }
#endif
}