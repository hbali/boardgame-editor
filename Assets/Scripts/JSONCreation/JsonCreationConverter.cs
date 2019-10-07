using System;
using System.Linq;
using BoardGameModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonDemo
{
    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        protected abstract T Create(Type objectType, JObject jsonObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType,
          object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var target = Create(objectType, jsonObject);
            serializer.Populate(jsonObject.CreateReader(), target);
            return target;
        }

        public override void WriteJson(JsonWriter writer, object value,
       JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class JsonWinningEventConverter : JsonCreationConverter<BaseWinningEvent>
    {
        protected override BaseWinningEvent Create(Type objectType, JObject jsonObject)
        {
            WinningEventType eType = WinningEventType.StartFieldWinningEvent;
            bool success = Enum.TryParse(jsonObject["eventType"].ToString(), out eType);
            switch (eType)
            {
                case WinningEventType.CurrencyWinningEvent:
                    return new CurrencyWinningEvent();
                case WinningEventType.ItemWinningEvent:
                    return new ItemWinningEvent();
                case WinningEventType.StartFieldWinningEvent:
                    return new StartFieldWinningEvent();
            }

            return null;
        }
    }

}