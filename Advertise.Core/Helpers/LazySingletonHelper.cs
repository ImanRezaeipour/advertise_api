using System;
using System.Threading;

namespace Advertise.Core.Helpers
{
    public sealed class LazySingletonExtensions
    {
        #region Private Fields

        private static readonly Lazy<LazySingletonExtensions> _instance = new Lazy<LazySingletonExtensions>(() => new LazySingletonExtensions(), LazyThreadSafetyMode.ExecutionAndPublication);

        #endregion Private Fields

        #region Private Constructors

        private LazySingletonExtensions()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        public static LazySingletonExtensions Instance => _instance.Value;

        #endregion Public Properties
    }
}