﻿using JsonApiSerializer.ContractResolvers;
using JsonApiSerializer.ContractResolvers.Contracts;
using JsonApiSerializer.JsonConverters;
using JsonApiSerializer.SerializationState;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace JsonApiSerializer.Util
{
    internal static class WriterUtil
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ShouldWriteProperty<T>(object value, JsonProperty prop, JsonSerializer serializer, out T propValue)
        {
            if (prop == null)
            {
                propValue = default(T);
                return false;
            }

            var rawPropValue = prop.ValueProvider.GetValue(value);

            var shouldSerialize = prop.ShouldSerialize == null || prop.ShouldSerialize(value) == true;
            var nullValuePass = (rawPropValue != null || (prop.NullValueHandling ?? serializer.NullValueHandling) == NullValueHandling.Include);
            var propType = rawPropValue?.GetType() ?? prop.PropertyType;
            var defaultValuePass = (rawPropValue == null || !rawPropValue.Equals(propType.GetDefault()) ||
                                    (prop.DefaultValueHandling ?? serializer.DefaultValueHandling) !=
                                    DefaultValueHandling.Ignore);
            if (typeof(T) == typeof(string))
            {
                rawPropValue = rawPropValue?.ToString();
            }
            propValue = (T)rawPropValue;
            return shouldSerialize && nullValuePass && defaultValuePass;
        }

        public static bool TryUseCustomConvertor(JsonWriter writer, object value, JsonSerializer serializer, JsonConverter excludeConverter)
        {
            // if they have custom convertors registered, we will respect them
            for (var index = 0; index < serializer.Converters.Count; index++)
            {
                var converter = serializer.Converters[index];

                if (converter != excludeConverter && converter.CanWrite && converter.CanConvert(value.GetType()))
                {
                    converter.WriteJson(writer, value, serializer);
                    return true;
                }
            }

            return false;
        }

        public static string CalculateDefaultJsonApiType(object obj, SerializationData serializationData, JsonSerializer serializer)
        {
            if (serializationData.ReferenceTypeNames.TryGetValue(obj, out string typeName))
                return typeName;

            var jsonApiType = CalculateDefaultJsonApiTypeFromObjectType(obj.GetType(), serializationData, serializer);
            serializationData.ReferenceTypeNames[obj] = jsonApiType;

            return jsonApiType;
        }

        private static string CalculateDefaultJsonApiTypeFromObjectType(Type objectType, SerializationData serializationData, JsonSerializer serializer)
        {
            // Hack: To keep backward compatability we are not sure what resouceObjectConverter to use
            // we need to check if either one was defined as a serializer, or if one was defined as
            // furher up the stack (i.e. a member converter)

            for (var i = 0; i < serializer.Converters.Count; i++)
            {
                var converter = serializer.Converters[i];
                if (converter is ResourceObjectConverter roc && converter.CanWrite && converter.CanConvert(objectType))
                {
                    return roc.GenerateDefaultTypeNameInternal(objectType);
                }
            }

            var contractResolver = (JsonApiContractResolver)serializer.ContractResolver;
            foreach (var converter in serializationData.ConverterStack)
            {
                if (converter == contractResolver.ResourceObjectConverter
                    && contractResolver.ResolveContract(objectType) is ResourceObjectContract defaultRoc)
                {
                    return defaultRoc.DefaultType;
                }
                else if (converter is ResourceObjectConverter roc)
                {
                    return roc.GenerateDefaultTypeNameInternal(objectType);
                }
            }

            return objectType.Name.ToLower();
        }
    }
}
