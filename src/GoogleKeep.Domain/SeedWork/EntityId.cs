namespace GoogleKeep.Domain.SeedWork
{
    public class EntityId : IEquatable<EntityId>
    {
        public Guid Value { get; }

        public EntityId(Guid value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is EntityId entityId && Equals(entityId);
        }

        public bool Equals(EntityId other)
        {
            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(EntityId a, EntityId b)
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

        public static bool operator !=(EntityId a, EntityId b)
        {
            return !(a == b);
        }
    }
}
