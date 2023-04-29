#if UNITY_EDITOR
using Newtonsoft.Json;
using System;
using UnityEngine;

/// <summary>
/// 使Json.Net可以正确序列化或反序列化Unity中的Vector数据
/// </summary>
public class VectorConverter : JsonConverter
{
    public override bool CanRead => true;
    public override bool CanWrite => true;

    public override bool CanConvert(Type objectType)
    {
        return typeof(Vector2) == objectType ||
        typeof(Vector2Int) == objectType ||
        typeof(Vector3) == objectType ||
        typeof(Vector3Int) == objectType ||
        typeof(Vector4) == objectType;
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        switch (objectType.FullName)
        {
            case "UnityEngine.Vector2":
                return JsonConvert.DeserializeObject<Vector2>(serializer.Deserialize(reader).ToString());
            case "UnityEngine.Vector2Int":
                return JsonConvert.DeserializeObject<Vector2Int>(serializer.Deserialize(reader).ToString());
            case "UnityEngine.Vector3":
                return JsonConvert.DeserializeObject<Vector3>(serializer.Deserialize(reader).ToString());
            case "UnityEngine.Vector3Int":
                return JsonConvert.DeserializeObject<Vector3Int>(serializer.Deserialize(reader).ToString());
            case "UnityEngine.Vector4":
                return JsonConvert.DeserializeObject<Vector4>(serializer.Deserialize(reader).ToString());
        }
        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        switch (value.GetType().FullName)
        {
            case "UnityEngine.Vector2":
                Vector2 v2 = (Vector2)value;
                writer.WritePropertyName("x");
                writer.WriteValue(v2.x);
                writer.WritePropertyName("y");
                writer.WriteValue(v2.y);
                break;
            case "UnityEngine.Vector2Int":
                Vector2Int v2i = (Vector2Int)value;
                writer.WritePropertyName("x");
                writer.WriteValue(v2i.x);
                writer.WritePropertyName("y");
                writer.WriteValue(v2i.y);
                break;
            case "UnityEngine.Vector3":
                Vector3 v3 = (Vector3)value;
                writer.WritePropertyName("x");
                writer.WriteValue(v3.x);
                writer.WritePropertyName("y");
                writer.WriteValue(v3.y);
                writer.WritePropertyName("z");
                writer.WriteValue(v3.z);
                break;
            case "UnityEngine.Vector3Int":
                Vector3Int v3i = (Vector3Int)value;
                writer.WritePropertyName("x");
                writer.WriteValue(v3i.x);
                writer.WritePropertyName("y");
                writer.WriteValue(v3i.y);
                writer.WritePropertyName("z");
                writer.WriteValue(v3i.z);
                break;
            case "UnityEngine.Vector4":
                Vector4 v4 = (Vector4)value;
                writer.WritePropertyName("x");
                writer.WriteValue(v4.x);
                writer.WritePropertyName("y");
                writer.WriteValue(v4.y);
                writer.WritePropertyName("z");
                writer.WriteValue(v4.z);
                writer.WritePropertyName("w");
                writer.WriteValue(v4.w);
                break;
            default:
                throw new Exception("Unexpected Error Occurred");
        }
        writer.WriteEndObject();
    }
}
#endif