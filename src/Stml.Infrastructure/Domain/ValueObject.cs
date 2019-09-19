﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Domain
{
    public abstract class ValueObject<T> : IValueObject<T> where T : class
    {
        public override bool Equals(object obj)
        {
            var valueObject = obj as T;
            return valueObject is object && EqualsCore(valueObject);
        }

        protected abstract bool EqualsCore(T other);

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected abstract int GetHashCodeCore();

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }

        public virtual T Clone()
        {
            return (T)MemberwiseClone();
        }
    }
}
