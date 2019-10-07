using JsonDemo;
using Newtonsoft.Json;
using System;

namespace ConsoleApp2
{
    class Program
    {
        private static Random r = new Random();

        static void Main(string[] args)
        {
            BoardGame bg = new BoardGame();
            bg.currency = "forint";
            bg.startingCurrency = 23;
            bg.winningEvent = new CurrencyWinningEvent()
            {
                amount = 355
            };
            bg.fields = new System.Collections.Generic.List<Field>();
            for (int i = 0; i < 5; i++)
            {
                bg.fields.Add(new Field()
                {
                    x = i,
                    y = i,
                    texturePath = "examplePath",
                    fieldEvent = GetRandomEvent(r.Next(0, 6))
                });
            }
            string json = JsonConvert.SerializeObject(bg);
            BoardGame bg2 = JsonConvert.DeserializeObject<BoardGame>(json, new JsonBaseEventConverter(), new JsonWinningEventConverter());
            int sad = 2;
        }


        private static BaseEvent GetRandomEvent(int rand)
        {
            switch (rand)
            {
                case 0:
                    return new MoveAheadEvent() { amount = r.Next(1, 10), eventType = EventType.MoveAheadEvent };
                case 1:
                    return new GetItemEvent() { item = "exampleItem", eventType = EventType.GetItemEvent };
                case 2:
                    return new CanGetItem() { item = "exampleItem", eventType = EventType.CanGetItem };
                case 3:
                    return new CurrencyEvent() { isGet = r.Next(0, 2) == 1, amount = r.Next(10, 1000), eventType = EventType.CurrencyEvent };
                case 4:
                    return new LeftOutEvent() { amount = 3, eventType = EventType.LeftOutEvent };
                case 5:
                    return new LuckyCardEvent() { luckyEvent = GetRandomEvent(r.Next(0, 5)), eventType = EventType.LuckyCardEvent };
                default:
                    return new MoveAheadEvent() { amount = r.Next(1, 10), eventType = EventType.MoveAheadEvent };
            }
        }
    }
}
