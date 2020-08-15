using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Advertise.Core.Managers.Reflection
{
    public class PropertyReflector
    {
        #region Private Fields

        private const char PropertyNameSeparator = '.';

        private static readonly object[] NoParams = new object[0];
        private static readonly Type[] NoTypeParams = new Type[0];

        private readonly IDictionary<Type, ConstructorInfo> _constructorCache = new Dictionary<Type, ConstructorInfo>();
        private readonly IDictionary<Type, PropertyInfoCache> _propertyCache = new Dictionary<Type, PropertyInfoCache>();

        #endregion Private Fields

        #region Public Methods

        public Type GetType(Type targetType, string propertyName)
        {
            if (propertyName.IndexOf(PropertyNameSeparator) > -1)
            {
                var propertyList = propertyName.Split(PropertyNameSeparator);
                return propertyList.Aggregate(targetType, GetTypeImpl);
            }
            else
            {
                return GetTypeImpl(targetType, propertyName);
            }
        }

        public object GetValue(object target, string propertyName)
        {
            if (propertyName.IndexOf(PropertyNameSeparator) > -1)
            {
                var propertyList = propertyName.Split(PropertyNameSeparator);
                foreach (var currentProperty in propertyList)
                {
                    target = GetValueImpl(target, currentProperty);
                    if (target == null)
                    {
                        return null;
                    }
                }
                return target;
            }
            else
            {
                return GetValueImpl(target, propertyName);
            }
        }

        public void SetValue(object target, string propertyName, object value)
        {
            if (propertyName.IndexOf(PropertyNameSeparator) > -1)
            {
                var originalTarget = target;
                var propertyList = propertyName.Split(PropertyNameSeparator);
                for (var i = 0; i < propertyList.Length - 1; i++)
                {
                    propertyName = propertyList[i];
                    target = GetValueImpl(target, propertyName);
                    if (target == null)
                    {
                        var currentFullPropertyNameString = GetPropertyNameString(propertyList, i);
                        target = Construct(GetType(originalTarget.GetType(), currentFullPropertyNameString));
                        SetValue(originalTarget, currentFullPropertyNameString, target);
                    }
                }
                propertyName = propertyList[propertyList.Length - 1];
            }
            SetValueImpl(target, propertyName, value);
        }

        #endregion Public Methods

        #region Private Methods

        private static int CalculateDistance(Type targetObjectType, Type baseType)
        {
            if (!baseType.IsInterface)
            {
                var currType = targetObjectType;
                var level = 0;
                while (currType != null)
                {
                    if (baseType == currType)
                    {
                        return level;
                    }
                    currType = currType.BaseType;
                    level++;
                }
            }
            return -1;
        }

        private static PropertyInfo GetBestMatchingProperty(string propertyName, Type type)
        {
            var propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            PropertyInfo bestMatch = null;
            var bestMatchDistance = int.MaxValue;
            for (var i = 0; i < propertyInfos.Length; i++)
            {
                var info = propertyInfos[i];
                if (info.Name == propertyName)
                {
                    var distance = CalculateDistance(type, info.DeclaringType);
                    if (distance == 0)
                    {
                        // as close as we're gonna get...
                        return info;
                    }
                    if (distance > 0 && distance < bestMatchDistance)
                    {
                        bestMatch = info;
                        bestMatchDistance = distance;
                    }
                }
            }
            return bestMatch;
        }

        private static string GetPropertyNameString(string[] propertyList, int level)
        {
            var currentFullPropertyName = new StringBuilder();
            for (var j = 0; j <= level; j++)
            {
                if (j > 0)
                {
                    currentFullPropertyName.Append(PropertyNameSeparator);
                }
                currentFullPropertyName.Append(propertyList[j]);
            }
            return currentFullPropertyName.ToString();
        }

        private object Construct(Type type)
        {
            if (!_constructorCache.ContainsKey(type))
            {
                lock (this)
                {
                    if (!_constructorCache.ContainsKey(type))
                    {
                        var constructorInfo = type.GetConstructor(NoTypeParams);
                        if (constructorInfo == null)
                        {
                            throw new Exception(string.Format("Unable to construct instance, no parameterless constructor found in type {0}", type.FullName));
                        }
                        _constructorCache.Add(type, constructorInfo);
                    }
                }
            }
            return _constructorCache[type].Invoke(NoParams);
        }

        private PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            var propertyInfoCache = GetPropertyInfoCache(type);
            if (!propertyInfoCache.ContainsKey(propertyName))
            {
                var propertyInfo = GetBestMatchingProperty(propertyName, type);
                if (propertyInfo == null)
                {
                    throw new ArgumentException(string.Format("Unable to find public property named {0} on type {1}", propertyName, type.FullName), propertyName);
                }
                propertyInfoCache.Add(propertyName, propertyInfo);
            }
            return propertyInfoCache[propertyName];
        }

        private PropertyInfoCache GetPropertyInfoCache(Type type)
        {
            if (!_propertyCache.ContainsKey(type))
            {
                lock (this)
                {
                    if (!_propertyCache.ContainsKey(type))
                    {
                        _propertyCache.Add(type, new PropertyInfoCache());
                    }
                }
            }
            return _propertyCache[type];
        }

        private Type GetTypeImpl(Type targetType, string propertyName)
        {
            return GetPropertyInfo(targetType, propertyName).PropertyType;
        }

        private object GetValueImpl(object target, string propertyName)
        {
            return GetPropertyInfo(target.GetType(), propertyName).GetValue(target, NoParams);
        }

        private void SetValueImpl(object target, string propertyName, object value)
        {
            GetPropertyInfo(target.GetType(), propertyName).SetValue(target, value, NoParams);
        }

        #endregion Private Methods
    }

    internal class PropertyInfoCache
    {
        #region Private Fields

        private readonly IDictionary<string, PropertyInfo> _propertyInfoCache;

        #endregion Private Fields

        #region Public Constructors

        public PropertyInfoCache()
        {
            _propertyInfoCache = new Dictionary<string, PropertyInfo>();
        }

        #endregion Public Constructors

        #region Public Indexers

        public PropertyInfo this[string key]
        {
            get { return _propertyInfoCache[key]; }
            set { _propertyInfoCache[key] = value; }
        }

        #endregion Public Indexers

        #region Public Methods

        public void Add(string key, PropertyInfo value)
        {
            _propertyInfoCache.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return _propertyInfoCache.ContainsKey(key);
        }

        #endregion Public Methods
    }
}