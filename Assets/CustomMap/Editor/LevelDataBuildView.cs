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

    private string m_outPut;

    [Button("从文本加载")]
    public void LoadFromRaw([LabelText("文本")] string rawJson)
    {
        levelDataResource.LevelData = JsonConvert.DeserializeObject<LevelData>(rawJson);
        Debug.Log("Deserialize done");
    }

    [Button("从文件加载")]
    public void LoadFromFile([LabelText("路径")] string path)
    {
        levelDataResource.LevelData = JsonConvert.DeserializeObject<LevelData>(File.ReadAllText(path));
        Debug.Log("Deserialize done");
    }

    [Button("生成到文件")]
    public void DumpToRaw([LabelText("路径")] string path)
    {
        m_outPut = JsonConvert.SerializeObject(levelDataResource.LevelData, new VectorConverter());
        File.WriteAllText(path, m_outPut);
    }

    [Button("Test")]
    public void Test()
    {
        var data = JsonConvert.DeserializeObject<LevelData>(File.ReadAllText(@"C:\Users\Jxr\Downloads\level_main_07-13.json"));
        m_outPut = JsonConvert.SerializeObject(data, new VectorConverter());
        File.WriteAllText(@"C:\Users\Jxr\Downloads\level_main_07-13-0.json", m_outPut);
    }
}
