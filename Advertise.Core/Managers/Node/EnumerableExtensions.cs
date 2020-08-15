using System;
using System.Collections.Generic;
using System.Linq;

namespace Advertise.Core.Managers.Node
{
    public static class EnumerableExtensions
    {
        #region Public Methods

        public static IEnumerable<T> ToFlatten<T>(this IEnumerable<T> rootLevel, Func<T, IEnumerable<T>> nextLevel)
        {
            var accumulation = new List<T>();
            accumulation.AddRange(rootLevel);
            FlattenLevel<T>(accumulation, rootLevel, nextLevel);
            return accumulation;
        }

        public static IEnumerable<NodeModel<T>> ToHierarchy<T>(this IEnumerable<T> source, Func<T, bool> startWith, Func<T, T, bool> connectBy)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (startWith == null) throw new ArgumentNullException(nameof(startWith));
            if (connectBy == null) throw new ArgumentNullException(nameof(connectBy));
            return source.ToHierarchy(startWith, connectBy, null);
        }

        #endregion Public Methods

        #region Private Methods

        private static void FlattenLevel<T>(List<T> accumulation, IEnumerable<T> currentLevel, Func<T, IEnumerable<T>> nextLevel)
        {
            foreach (T item in currentLevel)
            {
                accumulation.AddRange(currentLevel);
                FlattenLevel<T>(accumulation, nextLevel(item), nextLevel);
            }
        }

        private static IEnumerable<NodeModel<T>> ToHierarchy<T>(this IEnumerable<T> source, Func<T, bool> startWith, Func<T, T, bool> connectBy, NodeModel<T> parent)
        {
            int level = parent?.Level + 1 ?? 0;

            var roots = from item in source
                        where startWith(item)
                        select item;
            foreach (T value in roots)
            {
                var children = new List<NodeModel<T>>();
                var newNode = new NodeModel<T>
                {
                    Level = level,
                    Parent = parent,
                    Item = value,
                    Children = children.AsReadOnly()
                };

                T tmpValue = value;
                children.AddRange(source.ToHierarchy(possibleSub => connectBy(tmpValue, possibleSub), connectBy, newNode));

                yield return newNode;
            }
        }

        #endregion Private Methods
    }
}