using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Assets.Scripts.Gameplay.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController :  Singleton<MapController>
{

    public Map Map;
    public void InitMap(GameObject gridObject)
    {
        Map = new Map();
        for (int i = 0; i < gridObject.transform.childCount; i++)
        {
            var tilemap = gridObject.transform.GetChild(i).GetComponent<Tilemap>();

            if (tilemap == null) continue;

            tilemap.CompressBounds();
            var size = tilemap.size;
            Debug.Log($"TileMap Size : X = {size.x} Y = {size.y} Z = {size.z}");


            StringBuilder sb = new StringBuilder();

            var mapData = Map.AddMapData();
            mapData.TileMapObject = tilemap;
            mapData.Sections = new Section[size.x * size.y];
            mapData.Width = size.x;
            mapData.Height = size.y;
            BoundsInt bounds = tilemap.cellBounds; // 获取 Tilemap 的边界范围
            int mapX = 0;
            int mapY = 0;
            for (int y = bounds.yMin; y < bounds.yMax; ++y) {

                for (int x = bounds.xMin; x < bounds.xMax; ++x)
                {
                    Vector3Int cellPosition = new Vector3Int(x, y, 0); // 构造每个单元格的位置向量

                    if (!tilemap.HasTile(cellPosition)) continue; // 判断当前单元格是否为空

                    var tile = tilemap.GetTile<MapTile>(cellPosition);
                    if (tile == null) {
                        Debug.LogError($"错误，在{cellPosition.x},{cellPosition.y},{0}位置没有找到Tile");
                        Debug.Log(sb.ToString());
                        return;
                    }
                    sb.Append(GetSectionName(tile.SectionType));

                    try
                    {
                        switch (tile.SectionType) {
                            case SectionType.Stair:
                                mapData.SetSection(new StairSection() {
                                    ParentMap = mapData,
                                    SectionType = tile.SectionType,
                                    Walkable = tile.IsWalkable,
                                    MapIndex = i,
                                    Position = new IntVec2(mapX, mapY)
                                }, mapX, mapY);
                                break;
                            default:
                                mapData.SetSection(new Section() {
                                    ParentMap = mapData,
                                    SectionType = tile.SectionType,
                                    Walkable = tile.IsWalkable,
                                    MapIndex = i,
                                    Position = new IntVec2(mapX, mapY)
                                },mapX,mapY);
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"出错的位置是：X={mapX},Y={mapY}");
                        throw;
                    }


                    mapX++;

                }

                mapY++;
                mapX = 0;

                sb.Append("\n");
            }


            Debug.Log(sb);

            
        }




        string GetSectionName(SectionType type)
        {
            switch (type)
            {
                case SectionType.Air:
                    return "空";
                case SectionType.Floor:
                    return "地";
                case SectionType.Stair:
                    return "梯";
                default:
                    return "墙";
            }
        }
    }


}
