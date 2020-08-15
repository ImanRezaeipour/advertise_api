using System;

namespace Advertise.Core.Domain.Common
{
    public abstract class Entity
    {
        public virtual Guid Id { get; set; }

        public static bool operator !=(Entity x, Entity y)
        {
            return !(x == y);
        }

        public static bool operator ==(Entity x, Entity y)
        {
            return Equals(x, y);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity);
        }

        public virtual bool Equals(Entity other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(Id, other.Id))
            {
                var otherType = other.GetUnProxiedType();
                var thisType = GetUnProxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                       otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (Equals(Id, Guid.Empty))
                return base.GetHashCode();
            return Id.GetHashCode();
        }

        private static bool IsTransient(Entity obj)
        {
            return obj != null && Equals(obj.Id, Guid.Empty);
        }

        private Type GetUnProxiedType()
        {
            return GetType();
        }
    }
}