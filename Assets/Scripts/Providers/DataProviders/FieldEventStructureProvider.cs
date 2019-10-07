using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Providers.DataProviders
{

    public enum Source
    {
        None,
        Bank,
        Player
    }

    public enum Target
    {
        None,
        Bank,
        Player
    }

    public enum FieldObject
    {
        Step,
        Money,
        Item,
        Round
    }

    public struct Rule
    {
        public string itemID { get; set; }
        public int amount { get; set; }        
        public string name { get; set; }
        public Source src { get; set; }
        public Target trg { get; set; }
        public FieldObject obj { get; set; }
    }

    public struct RuleChain
    {
        public List<Rule> rules { get; set; }

        public Rule First
        {
            get
            {
                return rules.FirstOrDefault();
            }
        }
    }

    class BoardGameEvents
    {        
        public List<RuleChain> ruleChain { get; set; }
    }

    class FieldEventStructureProvider : Singleton<FieldEventStructureProvider>
    {
        private const string eventsPath = "FieldEvents/eventrules";
        private readonly BoardGameEvents bge;

        public List<Rule> Rules
        {
            get; private set;
        }

        public FieldEventStructureProvider()
        {
            TextAsset rules = Resources.Load<TextAsset>(eventsPath);
            bge = JsonConvert.DeserializeObject<BoardGameEvents>(rules.text);
            Rules = new List<Rule>();
            foreach(RuleChain rc in bge.ruleChain)
            {
                Rules.Add(rc.First);
            }
        }
    }
}
