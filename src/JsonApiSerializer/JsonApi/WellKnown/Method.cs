using Newtonsoft.Json;

namespace JsonApiSerializer.JsonApi.WellKnown
{
    public enum Method
    {
        None,
        Create,
        Update,
        Destroy,
        Disassociate
    }
}