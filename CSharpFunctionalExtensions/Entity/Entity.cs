namespace CSharpFunctionalExtensions
{
    public abstract class Entity<TId>
    {
        public virtual TId Id { get; protected set; }

        protected Entity()
        {
        }

        protected Entity(TId id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity<TId> other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            // When lazy-loading is enabled for EF or NHibernate, navigation properties at the runtime are derived to proxies.
            // Unproxying the type is required as we could be comparing one proxy with the same id than a non proxy.
            if (GetType().Unproxy() != other.GetType().Unproxy())
                return false;

            if (IsTransient() || other.IsTransient())
                return false;

            return Id.Equals(other.Id);
        }

        private bool IsTransient()
        {
            return Id is null || Id.Equals(default(TId));
        }

        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TId> a, Entity<TId> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().Unproxy().ToString() + Id).GetHashCode();
        }
    }

    public abstract class Entity : Entity<long>
    {
        protected Entity()
        {
        }

        protected Entity(long id)
            : base(id)
        {
        }
    }
}
