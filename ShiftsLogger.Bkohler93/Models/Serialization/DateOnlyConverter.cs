using System.Text.Json;
using System.Text.Json.Serialization;

namespace Models.Serialization;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string DateFormat = "yyyy-MM-dd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var timeString = reader.GetString();
            if (DateOnly.TryParseExact(timeString, DateFormat, out DateOnly result))
            {
                return result;
            }
            throw new JsonException($"Invalid date format. Expected format is {DateFormat}. Value was {timeString}");
        }

        throw new JsonException("Invalid token type for DateOnly.");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateFormat));
    }
}
