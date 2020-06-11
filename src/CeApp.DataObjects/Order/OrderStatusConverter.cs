using System;
using Newtonsoft.Json;

namespace CeApp.DataObjects.Order
{
    public class OrderStatusConverter : JsonConverter<OrderStatus>
    {
        public override void WriteJson(JsonWriter writer, OrderStatus value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Name);
        }

        public override OrderStatus ReadJson(JsonReader reader, Type objectType, OrderStatus existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            return OrderStatus.Parse((string) reader.Value);
        }
    }
}