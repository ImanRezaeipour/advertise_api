namespace Advertise.Core.Events
{
    public class EntityUpdated<T> where T : class
    {
        #region Public Constructors

        public EntityUpdated(T entity)
        {
            this.Entity = entity;
        }

        #endregion Public Constructors

        #region Public Properties

        public T Entity { get; private set; }

        #endregion Public Properties
    }
}