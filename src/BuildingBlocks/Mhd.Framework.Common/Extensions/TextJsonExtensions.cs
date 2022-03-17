using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Mhd.Framework.Common
{
    public static class TextJsonExtensions
    {

        private static readonly JsonSerializerOptions options = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        };

        public static T? Deserialize<T>(this string json, JsonSerializerOptions? jsonOptions = null) => JsonSerializer.Deserialize<T>(json, jsonOptions ?? options);
        public static object? Deserialize(this string json, Type t, JsonSerializerOptions? jsonOptions = null) => JsonSerializer.Deserialize(json, t, jsonOptions ?? options);
        public static object? Deserialize(this ref Utf8JsonReader reader, Type returnType, JsonSerializerOptions? jsonOptions = null) => JsonSerializer.Deserialize(ref reader, returnType, jsonOptions ?? options);
        public static T? Deserialize<T>(this ref Utf8JsonReader reader, JsonSerializerOptions? jsonOptions = null) => JsonSerializer.Deserialize<T>(ref reader, jsonOptions ?? options);
        public static object? Deserialize(this ReadOnlySpan<byte> utf8Json, Type returnType, JsonSerializerOptions? jsonOptions = null) => JsonSerializer.Deserialize(utf8Json, returnType, jsonOptions ?? options);
        public static T? Deserialize<T>(this ReadOnlySpan<byte> utf8Json, JsonSerializerOptions? jsonOptions = null) => JsonSerializer.Deserialize<T>(utf8Json, jsonOptions ?? options);
        public static string Serialize<T>(this T data, JsonSerializerOptions? jsonOptions = null) => JsonSerializer.Serialize(data, jsonOptions ?? options);
        public static void Serialize<T>(this T data, Utf8JsonWriter writer, JsonSerializerOptions? jsonOptions = null) => JsonSerializer.Serialize(writer, data, jsonOptions ?? options);
        public static void Serialize(this object? data, Utf8JsonWriter writer, Type inputType, JsonSerializerOptions? jsonOptions = null) => JsonSerializer.Serialize(writer, data, inputType, jsonOptions ?? options);
        public static object? Serialize(this object? data, Type inputType, JsonSerializerOptions? jsonOptions = null) => JsonSerializer.Serialize(data, inputType, jsonOptions ?? options);
    }
}
