using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public static class CloneUtil
{
    public static ToType CloneFields<FromType, ToType>(FromType obj) where FromType : class where ToType : class
    {
        return CloneFields(obj, typeof(FromType), typeof(ToType)) as ToType;
    }
    public static object CloneFields(object obj, Type fromType, Type toType)
    {
        if (fromType.IsArray)
        {
            IList array = (IList)obj;
            IList ret = Array.CreateInstance(toType.GetElementType(), array.Count);
            for (int i = 0; i < array.Count; i++)
            {
                ret[i] = CloneFields(array[i], fromType.GetElementType(), toType.GetElementType());
            }
            return ret;
        }
        object returnObj = Activator.CreateInstance(toType);
        FieldInfo[] fromTypeFields = fromType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        for (int i = 0; i < fromTypeFields.Length; i++)
        {
            FieldInfo fromTypeField = fromTypeFields[i];
            object fieldValue = fromTypeField.GetValue(obj);
            FieldInfo toTypeField = toType.GetField(fromTypeField.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (toTypeField == null) continue;
            if (!fromTypeField.FieldType.IsEnum && (fromTypeField.FieldType.IsValueType || fromTypeField.FieldType.Equals(typeof(string))))
            {
                if (fromTypeField.FieldType.Equals(typeof(string)))
                {
                    if (string.IsNullOrEmpty((string)fieldValue))
                    {
                        fieldValue = null;
                    }
                }
                toTypeField.SetValue(returnObj, fieldValue);
            }
            else if (fromTypeField.FieldType.IsEnum)
            {
                toTypeField.SetValue(returnObj, Enum.ToObject(toTypeField.FieldType, fieldValue));
            }
            else
            {
                toTypeField.SetValue(returnObj, CloneFields(fieldValue, fromTypeField.FieldType, toTypeField.FieldType));
            }
        }
        return returnObj;
    }
}