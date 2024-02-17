using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.Tilemaps;

public class MapTile : Tile
{

    //TODO:每块Tile有当前的类型

    public bool IsWalkable = true;

    public SectionType SectionType = SectionType.Air;

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        return base.StartUp(position, tilemap, go);
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
    }

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Zombieland/Tiles/MapTile")]
    public static void CreateZombielandTile() {
        string path =
            EditorUtility.SaveFilePanelInProject("Save Map Tile", "New Map Tile", "Asset", "Save Map Tile", "Assets");
        if (path == "") {
            return;
        }

        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<MapTile>(), path);
    }
#endif


}
