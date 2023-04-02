using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CustomMap
{
    [ShowOdinSerializedPropertiesInInspector]
    public class NullableSerializedScriptableObject : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField, HideInInspector]
        private SerializationData serializationData;

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            UnitySerializationUtility.DeserializeUnityObject(this, ref serializationData);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            UnitySerializationUtility.SerializeUnityObject(this, ref serializationData);
        }
    }
}