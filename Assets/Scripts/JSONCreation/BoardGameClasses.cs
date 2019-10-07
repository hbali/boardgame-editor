using Database;
using Model;
using Providers.DataProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGameModels
{
    public class SavedGame
    {
        public string nextPlayer;
        public Dictionary<string, DbPlayer> players;
    }

    public class BoardGame
    {
        public List<DbEdge> edges;
        public List<DbField> fields;
        public string currency;
        public int startingCurrency;
        public BaseWinningEvent winningEvent;
        public List<DbItem> items;
    }

    public enum WinningEventType
    {
        CurrencyWinningEvent,
        ItemWinningEvent,
        StartFieldWinningEvent
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
