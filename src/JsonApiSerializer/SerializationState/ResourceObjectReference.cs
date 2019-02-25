using System;
using System.Collections.Generic;

namespace JsonApiSerializer.SerializationState
{
    internal struct ResourceObjectReference : IEquatable<ResourceObjectReference>
    {
        public readonly string Id;

        public readonly string Type;

        public readonly string TempId;

        public ResourceObjectReference(string id, string type, string tempId = null)
        {
            Id = id;
            Type = type;
            TempId = tempId;
        }

        public override string ToString()
        {
            return Id + ":" + Type + ":" + TempId;
        }

        public bool Equals(ResourceObjectReference other)
        {
            return other.Id == this.Id && other.Type == this.Type && other.TempId == TempId;
        }

        public override bool Equals(object obj)
        {
            return obj is ResourceObjectReference reference && Equals(reference);
        }

        public override int GetHashCode()
        {
            var hashCode = 1325953389;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TempId);
            return hashCode;
        }
    }
}
