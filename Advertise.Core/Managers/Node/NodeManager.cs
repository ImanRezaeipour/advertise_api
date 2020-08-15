using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advertise.Core.Managers.Node
{
    public class NodeManager<T> : IEqualityComparer, IEnumerable<T>, IEnumerable<NodeManager<T>>
    {
        #region Private Fields

        private readonly List<NodeManager<T>> _children = new List<NodeManager<T>>();

        #endregion Private Fields

        #region Public Constructors

        public NodeManager(T value)
        {
            Value = value;
        }

        #endregion Public Constructors

        #region Public Properties

        public IEnumerable<NodeManager<T>> All => Root.SelfAndDescendants;

        public IEnumerable<NodeManager<T>> Ancestors => IsRoot ? Enumerable.Empty<NodeManager<T>>() : Parent.ToIEnumarable().Concat(Parent.Ancestors);

        public IEnumerable<NodeManager<T>> Children => _children;

        public IEnumerable<NodeManager<T>> Descendants => SelfAndDescendants.Skip(1);

        public bool IsRoot => Parent == null;

        public int Level => Ancestors.Count();

        public NodeManager<T> Parent { get; private set; }

        public NodeManager<T> Root => SelfAndAncestors.Last();

        public IEnumerable<NodeManager<T>> SameLevel => SelfAndSameLevel.Where(Other);

        public IEnumerable<NodeManager<T>> SelfAndAncestors => this.ToIEnumarable().Concat(Ancestors);

        public IEnumerable<NodeManager<T>> SelfAndChildren => this.ToIEnumarable().Concat(Children);

        public IEnumerable<NodeManager<T>> SelfAndDescendants => this.ToIEnumarable().Concat(Children.SelectMany(c => c.SelfAndDescendants));

        public IEnumerable<NodeManager<T>> SelfAndSameLevel => GetNodeManagersAtLevel(Level);

        public IEnumerable<NodeManager<T>> SelfAndSiblings => IsRoot ? this.ToIEnumarable() : Parent.Children;

        public IEnumerable<NodeManager<T>> Siblings => SelfAndSiblings.Where(Other);

        public T Value { get; set; }

        #endregion Public Properties

        #region Public Indexers

        public NodeManager<T> this[int index] => _children[index];

        #endregion Public Indexers

        #region Public Methods

        public static IEnumerable<NodeManager<T>> CreateTree<TId>(IEnumerable<T> values, Func<T, TId> idSelector, Func<T, TId?> parentIdSelector)
            where TId : struct
        {
            var valuesCache = values.ToList();
            if (!valuesCache.Any())
                return Enumerable.Empty<NodeManager<T>>();
            T itemWithIdAndParentIdIsTheSame = valuesCache.FirstOrDefault(v => IsSameId(idSelector(v), parentIdSelector(v)));
            if (itemWithIdAndParentIdIsTheSame != null) // Hier verwacht je ook een null terug te kunnen komen
            {
                throw new ArgumentException("At least one value has the samen Id and parentId [{0}]".FormatInvariant(itemWithIdAndParentIdIsTheSame));
            }

            var nodeManagers = valuesCache.Select(v => new NodeManager<T>(v));
            return CreateTree(nodeManagers, idSelector, parentIdSelector);
        }

        public static IEnumerable<NodeManager<T>> CreateTree<TId>(IEnumerable<NodeManager<T>> rootNodeManagers, Func<T, TId> idSelector, Func<T, TId?> parentIdSelector)
            where TId : struct

        {
            var rootNodeManagersCache = rootNodeManagers.ToList();
            var duplicates = rootNodeManagersCache.Duplicates(n => n).ToList();
            if (duplicates.Any())
            {
                throw new ArgumentException("One or more values contains {0} duplicate keys. The first duplicate is: [{1}]".FormatInvariant(duplicates.Count, duplicates[0]));
            }

            foreach (var rootNodeManager in rootNodeManagersCache)
            {
                var parentId = parentIdSelector(rootNodeManager.Value);
                var parent = rootNodeManagersCache.FirstOrDefault(n => IsSameId(idSelector(n.Value), parentId));

                if (parent != null)
                {
                    parent.Add(rootNodeManager);
                }
                else if (parentId != null)
                {
                    throw new ArgumentException("A value has the parent ID [{0}] but no other NodeManagers has this ID".FormatInvariant(parentId.Value));
                }
            }
            var result = rootNodeManagersCache.Where(n => n.IsRoot);
            return result;
        }

        public static bool operator !=(NodeManager<T> value1, NodeManager<T> value2)
        {
            return !(value1 == value2);
        }

        public static bool operator ==(NodeManager<T> value1, NodeManager<T> value2)
        {
            if ((object)(value1) == null && (object)value2 == null)
            {
                return true;
            }
            return ReferenceEquals(value1, value2);
        }

        public NodeManager<T> Add(T value, int index = -1)
        {
            var childNodeManager = new NodeManager<T>(value);
            Add(childNodeManager, index);
            return childNodeManager;
        }

        public void Add(NodeManager<T> childNodeManager, int index = -1)
        {
            if (index < -1)
            {
                throw new ArgumentException("The index can not be lower then -1");
            }
            if (index > Children.Count() - 1)
            {
                throw new ArgumentException("The index ({0}) can not be higher then index of the last iten. Use the AddChild() method without an index to add at the end".FormatInvariant(index));
            }
            if (!childNodeManager.IsRoot)
            {
                throw new ArgumentException("The child NodeManager with value [{0}] can not be added because it is not a root NodeManager.".FormatInvariant(childNodeManager.Value));
            }

            if (Root == childNodeManager)
            {
                throw new ArgumentException("The child NodeManager with value [{0}] is the rootNodeManager of the parent.".FormatInvariant(childNodeManager.Value));
            }

            if (childNodeManager.SelfAndDescendants.Any(n => this == n))
            {
                throw new ArgumentException("The childNodeManager with value [{0}] can not be added to itself or its descendants.".FormatInvariant(childNodeManager.Value));
            }
            childNodeManager.Parent = this;
            if (index == -1)
            {
                _children.Add(childNodeManager);
            }
            else
            {
                _children.Insert(index, childNodeManager);
            }
        }

        public NodeManager<T> AddFirstChild(T value)
        {
            var childNodeManager = new NodeManager<T>(value);
            AddFirstChild(childNodeManager);
            return childNodeManager;
        }

        public void AddFirstChild(NodeManager<T> childNodeManager)
        {
            Add(childNodeManager, 0);
        }

        public NodeManager<T> AddFirstSibling(T value)
        {
            var childNodeManager = new NodeManager<T>(value);
            AddFirstSibling(childNodeManager);
            return childNodeManager;
        }

        public void AddFirstSibling(NodeManager<T> childNodeManager)
        {
            Parent.AddFirstChild(childNodeManager);
        }

        public NodeManager<T> AddLastSibling(T value)
        {
            var childNodeManager = new NodeManager<T>(value);
            AddLastSibling(childNodeManager);
            return childNodeManager;
        }

        public void AddLastSibling(NodeManager<T> childNodeManager)
        {
            Parent.Add(childNodeManager);
        }

        public NodeManager<T> AddParent(T value)
        {
            var newNodeManager = new NodeManager<T>(value);
            AddParent(newNodeManager);
            return newNodeManager;
        }

        public void AddParent(NodeManager<T> parentNodeManager)
        {
            if (!IsRoot)
            {
                throw new ArgumentException("This NodeManager [{0}] already has a parent".FormatInvariant(Value), "parentNodeManager");
            }
            parentNodeManager.Add(this);
        }

        public void Disconnect()
        {
            if (IsRoot)
            {
                throw new InvalidOperationException("The root NodeManager [{0}] can not get disconnected from a parent.".FormatInvariant(Value));
            }
            Parent._children.Remove(this);
            Parent = null;
        }

        public override bool Equals(Object anderePeriode)
        {
            var valueThisType = anderePeriode as NodeManager<T>;
            return this == valueThisType;
        }

        public bool Equals(NodeManager<T> value)
        {
            return this == value;
        }

        public bool Equals(NodeManager<T> value1, NodeManager<T> value2)
        {
            return value1 == value2;
        }

        bool IEqualityComparer.Equals(object value1, object value2)
        {
            var valueThisType1 = value1 as NodeManager<T>;
            var valueThisType2 = value2 as NodeManager<T>;

            return Equals(valueThisType1, valueThisType2);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _children.Values().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        public IEnumerator<NodeManager<T>> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        public int GetHashCode(object obj)
        {
            return GetHashCode(obj as NodeManager<T>);
        }

        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        public int GetHashCode(NodeManager<T> value)
        {
            return base.GetHashCode();
        }

        public IEnumerable<NodeManager<T>> GetNodeManagersAtLevel(int level)
        {
            return Root.GetNodeManagersAtLevelInternal(level);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        #endregion Public Methods

        #region Private Methods

        private static bool IsSameId<TId>(TId id, TId? parentId)
            where TId : struct
        {
            return parentId != null && id.Equals(parentId.Value);
        }

        private IEnumerable<NodeManager<T>> GetNodeManagersAtLevelInternal(int level)
        {
            if (level == Level)
            {
                return this.ToIEnumarable();
            }
            return Children.SelectMany(c => c.GetNodeManagersAtLevelInternal(level));
        }

        private bool Other(NodeManager<T> nodeManager)
        {
            return !ReferenceEquals(nodeManager, this);
        }

        #endregion Private Methods
    }
}