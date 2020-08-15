using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Advertise.Core.Extensions
{
    public static class TypeExtention
    {
        #region Public Methods

        public static T GetCustomAttribut<T>(this Type type) where T : Attribute
        {
            return type.GetCustomAttributes(true).FirstOrDefault(x => x is T) as T;
        }

        public static Type GetNonNullableType(this Type type)
        {
            return !IsNullable(type) ? type : type.GetGenericArguments()[0];
        }

        public static TypeConverter GetTypeConverter(Type type)
        {
            return ConvertExtention.GetTypeConverter(type);
        }

        public static bool HasAttribute<TAttribute>(this ICustomAttributeProvider target, bool inherits) where TAttribute : Attribute
        {
            return target.IsDefined(typeof(TAttribute), inherits);
        }

        public static bool IsInteger(this Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsNullable(this Type type)
        {
            return type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        #endregion Public Methods
    }
}