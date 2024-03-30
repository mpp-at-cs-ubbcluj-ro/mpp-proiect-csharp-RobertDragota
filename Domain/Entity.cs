using System;

namespace Lab3.Domain
{
    [Serializable]
    public class Entity<T> 
    {
        protected T id;

        public T Id
        {
            get { return id; }
            set { id = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var entity = (Entity<T>)obj;
            return Equals(Id, entity.Id);
        }

        // Assuming ID is a value type or has a proper GetHashCode implementation if it's a reference type
        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public override string ToString()
        {
            return $"Entity{{id={id}}}";
        }
    }
}

