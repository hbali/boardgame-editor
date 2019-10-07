using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class BoardGame
    {
        public List<Field> fields;
        public string currency;
        public int startingCurrency;
        public BaseWinningEvent winningEvent;
    }

    public class Field
    {
        public int x;
        public int y;
        public BaseEvent fieldEvent;
        public string texturePath;
    }

    public class BaseEvent
    {
        public EventType eventType;
    }

    public enum EventType
    {
        MoveAheadEvent,
        GetItemEvent,
        CanGetItem,
        CurrencyEvent,
        LeftOutEvent,
        LuckyCardEvent
    }

    public enum WinningEventType
    {
        CurrencyWinningEvent,
        ItemWinningEvent,
        StartFieldWinningEvent
    }

    public class MoveAheadEvent : BaseEvent
    {
        public int amount;
    }

    public class GetItemEvent : BaseEvent
    {
        public string item;
    }

    public class CanGetItem : BaseEvent
    {
        public string item;
    }

    public class CurrencyEvent : BaseEvent
    {
        //does the player get money or no
        public bool isGet;
        public int amount;
    }

    public class LeftOutEvent : BaseEvent
    {
        public int amount;
    }

    public class LuckyCardEvent : BaseEvent
    {
        //cant be LuckyCardEvent!
        public BaseEvent luckyEvent;
    }

    public class BaseWinningEvent
    {
        public WinningEventType eventType;
    }

    public class CurrencyWinningEvent : BaseWinningEvent
    {
        public int amount;
    }

    public class ItemWinningEvent : BaseWinningEvent
    {
        public Dictionary<string, int> items;
    }

    public class StartFieldWinningEvent : BaseWinningEvent
    {
        public int amount;
    }


}
