#if UNITY_EDITOR
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using CustomMap;
using Torappu;
using LevelData = CustomMap.LevelData;

[CreateAssetMenu(fileName = "LevelDataBuildView", menuName = "CustomMap/LevelDataBuildView")]
public class LevelDataBuildView : ScriptableObject, IResetable
{
    public void Reset()
    {
        mapBuildView = AssetDatabase.LoadAssetAtPath<MapBuildView>("Assets/CustomMap/MapBuildView.asset");
        routeBuildView = AssetDatabase.LoadAssetAtPath<RouteBuildView>("Assets/CustomMap/RouteBuildView.asset");
        waveBuildView = AssetDatabase.LoadAssetAtPath<WaveBuildView>("Assets/CustomMap/WaveBuildView.asset");
        levelDataResource = AssetDatabase.LoadAssetAtPath<LevelDataResource>("Assets/CustomMap/LevelData.asset");
    }

    private MapBuildView mapBuildView;
    private RouteBuildView routeBuildView;
    private WaveBuildView waveBuildView;
    private LevelDataResource levelDataResource;

    private string m_outPut;

    [BoxGroup("InOut", LabelText = "文件操作"), Button("从文本加载")]
    public void LoadFromRaw([LabelText("文本")] string rawJson)
    {
        levelDataResource.LevelData = JsonConvert.DeserializeObject<LevelData>(rawJson);
        Debug.Log("Deserialize done");
    }

    [BoxGroup("InOut", LabelText = "文件操作"), Button("从文件加载")]
    public void LoadFromFile([LabelText("路径")] string path)
    {
        levelDataResource.LevelData = JsonConvert.DeserializeObject<LevelData>(File.ReadAllText(path));
        Debug.Log("Deserialize done");
    }

    [BoxGroup("InOut", LabelText = "文件操作"), Button("生成到文件")]
    public void DumpToRaw([LabelText("路径")] string path)
    {
        m_outPut = JsonConvert.SerializeObject(levelDataResource.LevelData, new VectorConverter());
        File.WriteAllText(path, m_outPut);
        Debug.Log($"Outputed to {path}");
    }

    [BoxGroup("InOut", LabelText = "修改LevelData"), Button("从TileMap加载所有地图信息")]
    public void LoadMapData()
    {
        var map = mapBuildView.TileMap;
        MapData mapData = new MapData();
        mapData.tiles = new TileData[map.Length];
        int width = map.GetLength(0), height = map.GetLength(1);
        mapData.map = new short[height, width];
        for (short row = 0; row < map.GetLength(1); row++)
        {
            for (short col = 0; col < map.GetLength(0); col++)
            {
                var info = map[col, row];
                short index = (short)((height - row - 1) * width + col);
                mapData.map[row, col] = index;
                TileData tileData = info.TileData;
                if (tileData.effects != null && tileData.effects.Length == 0)
                    tileData.effects = null;
                if (tileData.blackboard != null && tileData.blackboard.Count == 0)
                    tileData.blackboard = null;
                mapData.tiles[index] = tileData;
            }
        }
        levelDataResource.LevelData.mapData = mapData;
    }

    [BoxGroup("InOut", LabelText = "修改LevelData"), Button("加载路线信息")]
    public void LoadRouteData()
    {
        var routeData = levelDataResource.LevelData.routes;
        routeBuildView.ApplyTo(ref routeData);
        levelDataResource.LevelData.routes = routeData;
    }

    [BoxGroup("InOut", LabelText = "修改LevelData"), Button("加载波次信息")]
    public void LoadWaveData()
    {
        var waveData = levelDataResource.LevelData.waves;
        waveBuildView.ApplyTo(ref waveData);
        levelDataResource.LevelData.waves = waveData;
    }
}
#endif