using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EmployeePayroll.Infrastructure
{
    public class CurrencyConverter : JsonConverter<Decimal>
    {
        public override Decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Debug.Assert(typeToConvert == typeof(Decimal));
            return Decimal.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Decimal value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("C"));
        }
    }
}