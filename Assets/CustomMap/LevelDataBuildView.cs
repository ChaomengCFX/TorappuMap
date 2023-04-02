using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using CustomMap;

[CreateAssetMenu(fileName = "LevelDataBuildView", menuName = "CustomMap/LevelDataBuildView")]
public class LevelDataBuildView : ScriptableObject, IResetable
{
    public void Reset()
    {
        levelDataResource = AssetDatabase.LoadAssetAtPath<LevelDataResource>("Assets/CustomMap/LevelData.asset");
    }

    private LevelDataResource levelDataResource;

    [Button("从文本加载")]
    public void LoadFromRaw([LabelText("文本")] string rawJson)
    {
        levelDataResource.levelData = JsonConvert.DeserializeObject<LevelData>(rawJson);
    }

    [Button("从文件加载")]
    public void LoadFromFile([LabelText("路径")] string path)
    {
        levelDataResource.levelData = JsonConvert.DeserializeObject<LevelData>(File.ReadAllText(path));
    }
}
