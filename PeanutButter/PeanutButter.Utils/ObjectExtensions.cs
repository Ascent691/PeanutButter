﻿using System;
using System.Diagnostics;
using System.Linq;

namespace PeanutButter.Utils
{
    public static class ObjectExtensions
    {
        private static Type[] _simpleTypes;

        static ObjectExtensions()
        {
            _simpleTypes = new[] {
                typeof(int),
                typeof(char),
                typeof(byte),
                typeof(long),
                typeof(string),
                typeof(float),
                typeof(double),
                typeof(decimal),
                typeof(bool),
                typeof(DateTime)
            };
        }
        public static bool AllPropertiesMatch(this object objSource, object objCompare, params string[] ignorePropertiesByName)
        {
            if (objSource == null && objCompare == null) return true;
            if (objSource == null || objCompare == null) return false;
            var srcPropInfos = objSource.GetType().GetProperties();
            var comparePropInfos = objCompare.GetType().GetProperties();
            foreach (var srcProp in srcPropInfos)
            {
                if (ignorePropertiesByName.Contains(srcProp.Name))
                {
                    continue;
                }
                var comparePropInfo = comparePropInfos.FirstOrDefault(pi => pi.Name == srcProp.Name);
                if (comparePropInfo == null)
                {
                    Debug.WriteLine("Unable to find comparison property with name: '" + srcProp.Name + "'");
                    return false;
                }
                if (comparePropInfo.PropertyType != srcProp.PropertyType)
                {
                    Debug.WriteLine("Source property has type '" + srcProp.PropertyType.Name + "' but comparison property has type '" + comparePropInfo.PropertyType.Name + "'");
                    return false;
                }
                var srcValue = srcProp.GetValue(objSource, null);
                var compareValue = comparePropInfo.GetValue(objCompare, null);
                if (_simpleTypes.Any(st => st == srcProp.PropertyType))
                {
                    var srcString = StringOf(srcValue);
                    var compareString = StringOf(compareValue);
                    if (srcValue.ToString() != compareValue.ToString())
                    {
                        Debug.WriteLine(srcProp.Name + " value mismatch: (" + srcString + ") vs (" + compareString + ")");
                        return false;
                    }
                    continue;
                }
                if (!srcValue.AllPropertiesMatch(compareValue))
                    return false;
            }
            return true;
        }

        public static void CopyPropertiesTo(this object src, object dst, bool deep = true)
        {
            if (src == null || dst == null) return;
            var srcPropInfos = src.GetType().GetProperties();
            var dstPropInfos = dst.GetType().GetProperties();

            foreach (var srcPropInfo in srcPropInfos)
            {
                if (!srcPropInfo.CanRead) continue;
                var matchingTarget = dstPropInfos.FirstOrDefault(dp => dp.Name == srcPropInfo.Name && dp.PropertyType == srcPropInfo.PropertyType);
                if (matchingTarget == null) continue;
                if (!matchingTarget.CanWrite) continue;

                var srcVal = srcPropInfo.GetValue(src, null);
                if (!deep || IsSimpleTypeOrNullableOfSimpleType(srcPropInfo.PropertyType))
                {
                    matchingTarget.SetValue(dst, srcVal, null);
                }
                else
                {
                    if (srcVal != null)
                    {
                        var targetVal = matchingTarget.GetValue(dst,null);
                        srcVal.CopyPropertiesTo(targetVal);
                    }
                    else
                    {
                        matchingTarget.SetValue(dst, null, null);
                    }
                }
            }
        }

        private static bool IsSimpleTypeOrNullableOfSimpleType(Type t)
        {
            return _simpleTypes.Any(si => si == t || 
                                          (t.IsGenericType && 
                                          t.GetGenericTypeDefinition() == typeof(Nullable<>) && 
                                          Nullable.GetUnderlyingType(t) == si));
        }

        private static string StringOf(object srcValue)
        {
            return srcValue == null ? "[null]" : srcValue.ToString();
        }

        public static T Get<T>(this object src, string propertyName, T defaultValue = default(T))
        {
            var propInfo = src.GetType()
                                .GetProperties()
                                .FirstOrDefault(pi => pi.Name == propertyName);
            if (propInfo == null)
                return defaultValue;
            if (!propInfo.PropertyType.IsAssignableTo<T>())
                throw new ArgumentException(
                    "Get<> must be invoked with a type to which the property value could be assigned");
            return (T) propInfo.GetValue(src, null);
        }

        public static bool IsAssignableTo<T>(this Type type)
        {
            return type.IsAssignableFrom(typeof (T));
        }
    }
}
