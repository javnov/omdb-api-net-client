using Newtonsoft.Json;
using System;
using System.Linq;

namespace OpenMovieDatabase.Client.Internal
{
    internal class YearJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(int[]);
        }

        public override bool CanRead => true;
        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = (string)reader.Value;

            if (string.IsNullOrEmpty(value))
                return null;

            var values = value.Split('-', '–');
            if (values.Length > 2)
                return null;

            return values.Select(ParseValue).ToArray();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }

        private static int? ParseValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            if (int.TryParse(value, out int parsedValue))
                return parsedValue;

            return null;
        }
    }
}
