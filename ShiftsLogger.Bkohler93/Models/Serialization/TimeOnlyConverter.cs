using System.Text.Json;
using System.Text.Json.Serialization;

namespace Models.Serialization;

public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    private const string TimeFormat = "HH:mm";

    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var timeString = reader.GetString();
            if (TimeOnly.TryParseExact(timeString, TimeFormat, out TimeOnly result))
            {
                return result;
            }
            throw new JsonException($"Invalid time format. Expected format is {TimeFormat}.");
        }

        throw new JsonException("Invalid token type for TimeOnly.");
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(TimeFormat));
    }
}
