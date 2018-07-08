using JsonApiSerializer.JsonApi.WellKnown;
using Newtonsoft.Json.Linq;
using System;

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

        public ResourceObjectReference(JObject jobj) 
            : this(jobj[PropertyNames.Id]?.ToString(), jobj[PropertyNames.Type]?.ToString(), jobj[PropertyNames.TempId]?.ToString())
        {
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
            return obj is ResourceObjectReference && Equals((ResourceObjectReference)obj);
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + Id?.GetHashCode() ?? 0;
                hash = hash * 23 + Type?.GetHashCode() ?? 0;
                hash = hash * 23 + TempId?.GetHashCode() ?? 0;
                return hash;
            }
        }
    }
}
