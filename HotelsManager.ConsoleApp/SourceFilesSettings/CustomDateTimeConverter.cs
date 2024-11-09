using Newtonsoft.Json.Converters;

namespace AccessGroupCodeChallenge;

public class CustomDateTimeConverter : IsoDateTimeConverter
{
    public CustomDateTimeConverter()
    {
        DateTimeFormat = "yyyyMMdd";
    }
}