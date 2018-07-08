using System.Runtime.CompilerServices;

namespace JsonApiSerializer.SerializationState
{
    internal class ResourceReferenceData
    {
        private static readonly ConditionalWeakTable<object, ResourceReferenceData> PerSerializationData = new ConditionalWeakTable<object, ResourceReferenceData>();

        public static ResourceReferenceData GetResourceReferenceData(object objectValue)
        {
            object token = objectValue;
            return PerSerializationData.GetOrCreateValue(token);
        }

        public string TempId { get; set; }

    }
}
