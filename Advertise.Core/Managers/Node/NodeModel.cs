using System.Collections.Generic;

namespace Advertise.Core.Managers.Node
{
    public class NodeModel<T>
    {
        #region Internal Constructors

        internal NodeModel()
        {
        }

        #endregion Internal Constructors

        #region Public Properties

        public IList<NodeModel<T>> Children { get; internal set; }
        public T Item { get; internal set; }
        public int Level { get; internal set; }
        public NodeModel<T> Parent { get; internal set; }

        #endregion Public Properties
    }
}