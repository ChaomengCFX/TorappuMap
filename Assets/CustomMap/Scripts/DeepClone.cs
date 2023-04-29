#if UNITY_EDITOR
// Author: ChaomengCFX
// Created: 2023/04/09

using System;
using System.Collections;
using System.Reflection;

namespace CustomMap
{

    public static class CloneUtil
    {
        /// <summary>
        /// 克隆名称相同的字段
        /// </summary>
        /// <typeparam name="FromType">参数类型</typeparam>
        /// <typeparam name="ToType">返回类型</typeparam>
        /// <param name="obj">对象实例</param>
        /// <returns>转换后的实例</returns>
        public static ToType CloneFields<FromType, ToType>(FromType obj) where FromType : class where ToType : class
        {
            return CloneFields(obj, typeof(FromType), typeof(ToType)) as ToType;
        }

        /// <summary>
        /// 克隆名称相同的字段
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <param name="fromType">参数类型</param>
        /// <param name="toType">返回类型</param>
        /// <returns>转换后的实例</returns>
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
                        if (string.IsNullOrEmpty((string)fieldValue)) // 因为Unity序列化会自动将null的字符串new一个出来，故额外处理
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
}
#endif