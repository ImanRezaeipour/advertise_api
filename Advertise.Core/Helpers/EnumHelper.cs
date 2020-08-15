using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using Advertise.Core.Common;

namespace Advertise.Core.Helpers
{
    public static class EnumHelper
    {
        #region Public Methods
        
        public static IList<SelectList> CastToSelectListItems<T>() where T : struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>()
                .Select(x => new SelectList
                {
                    Text = GetEnumDescription(x as Enum),
                    Value = Convert.ToInt32(x).ToString()
                })
                .OrderBy(model => model.Value)
                .ToList();
        }

        //public static SelectList EnumSelectlist<TEnum>(bool indexed = false) where TEnum : struct
        //{
        //    return new SelectList(Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Select(item => new SelectList
        //    {
        //        Text = GetEnumDescription(item as Enum),
        //        Value = indexed ? Convert.ToInt32(item).ToString() : item.ToString()
        //    }).ToList(), "Value", "Text");
        //}

        public static string GetDescription<T>(T enumerationValue) where T : struct
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
            }
            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return enumerationValue.ToString();
        }

        public static string GetDescription<T>(int enumerationValue) where T : struct
        {
            var enumType = GetEnumByValue<T>(enumerationValue);
            return GetDescription<T>(enumType);
        }

        public static T GetEnumByValue<T>(int value) where T : struct
        {
            var enumType = typeof(T);
            var result = enumType.GetEnumValues().GetValue(value);
            var a = (T)Enum.Parse(typeof(T), result.ToString());
            return a;
        }

        public static string GetEnumDescription(Type type, string value)
        {
            var name =
                Enum.GetNames(type)
                    .Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase))
                    .Select(d => d)
                    .FirstOrDefault();

            if (name == null)
            {
                return String.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string GetEnumDisplayName(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null)
                return value.ToString();

            var attributes = (DisplayAttribute[])fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false);

            if (attributes[0].ResourceType != null)
                return LookupResource(attributes[0].ResourceType, attributes[0].Name);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Name;
            return value.ToString();
        }

        public static IEnumerable<SelectList> GetItems(Type enumType, int? selectedValue)
        {
            if (!typeof(Enum).IsAssignableFrom(enumType))
            {
                throw new ArgumentException("Type must be an enum");
            }

            var names = Enum.GetNames(enumType);
            var values = Enum.GetValues(enumType).Cast<int>();

            var items = names.Zip(values, (name, value) =>
                new SelectList
                {
                    Text = GetName(enumType, name),
                    Value = value.ToString(CultureInfo.InvariantCulture),
                    Selected = value == selectedValue
                }
                );
            return items;
        }

        public static string GetName(Type enumType, string name)
        {
            var result = name;

            var attribute = enumType
                .GetField(name)
                .GetCustomAttributes(false)
                .OfType<DisplayAttribute>()
                .FirstOrDefault();

            if (attribute != null)
            {
                result = attribute.GetName();
            }

            return result;
        }

        public static string GetText<T>(int enumerationValue) where T : struct
        {
            return Enum.GetName(typeof(T), enumerationValue);
        }

        public static string LookupResource(Type resourceManagerProvider, string resourceKey)
        {
            foreach (
                var staticProperty in
                    resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic |
                                                          BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(ResourceManager))
                {
                    var resourceManager = (ResourceManager)staticProperty.GetValue(null, null);
                    return resourceManager.GetString(resourceKey);
                }
            }

            return resourceKey; // Fallback with the key name
        }

        #endregion Public Methods
    }
}