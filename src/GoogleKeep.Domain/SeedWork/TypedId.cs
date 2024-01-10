namespace GoogleKeep.Domain.SeedWork
{
    public class TypedId : IEquatable<TypedId>
    {
        public Guid Value { get; }

        public TypedId(Guid value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is TypedId entityId && Equals(entityId);
        }

        public bool Equals(TypedId other)
        {
            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator == (TypedId a, TypedId b)
        {
            if (object.Equals(a, null))
            {
                if (object.Equals(b, null))
                {
                    return true;
                }

                return false;
            }

            return a.Equals(b);
        }

        public static bool operator != (TypedId a, TypedId b)
        {
            return !(a == b);
        }
    }
}
