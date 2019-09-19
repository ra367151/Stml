using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Domain
{
    public abstract class Entity : Entity<int>
    {

    }

    public abstract class Entity<TKey> : IEntity<TKey>
    {
        public TKey Id { get; protected set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;
            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;
            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity<TKey> a, Entity<TKey> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TKey> a, Entity<TKey> b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
