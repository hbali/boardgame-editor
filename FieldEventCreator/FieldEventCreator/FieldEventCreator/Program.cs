using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldEventCreator
{
    class Program
    {
        enum Source
        {
            None,
            Bank,
            Player
        }

        enum Target
        {
            None,
            Bank,
            Player
        }

        enum FieldObject
        {
            Step,
            Money,
            Item,
            Round
        }

        class Rule
        {
            public string name { get; set; }
            public Source src { get; set; }
            public Target trg { get; set; }
            public FieldObject obj { get; set; }
        }

        class RuleChain
        {
            public List<Rule> rules;
        }

        class BoardGameEvents
        {
            public List<RuleChain> ruleChain;

            public void Add(Rule rule)
            {
                ruleChain.Add(new RuleChain()
                {
                    rules = new List<Rule>()
                    {
                        rule
                    }
                });
            }
        }

        static void Main(string[] args)
        {
            BoardGameEvents bge = new BoardGameEvents
            {
                ruleChain = new List<RuleChain>()
            };

            //step event
            bge.Add(new Rule()
            {
                src = Source.None,
                trg = Target.Player,
                obj = FieldObject.Step,
                name = "StepEvent"
            });

            //get money
            bge.Add(new Rule()
            {
                src = Source.Bank,
                trg = Target.Player,
                obj = FieldObject.Money,
                name = "GetMoneyEvent"
            });

            //give money 
            bge.Add(new Rule()
            {
                src = Source.Player,
                trg = Target.Bank,
                obj = FieldObject.Money,
                name = "GiveMoneyEvent"
            });

            //item event
            bge.Add(new Rule()
            {
                src = Source.Bank,
                trg = Target.Player,
                obj = FieldObject.Item,
                name = "GetItemEvent"
            });

            //left out event
            bge.Add(new Rule()
            {
                src = Source.None,
                trg = Target.Player,
                obj = FieldObject.Round,
                name = "LeftOutEvent"
            });

            string json = JsonConvert.SerializeObject(bge);
            File.WriteAllText("eventrules.txt", json);
        }
    }
}
