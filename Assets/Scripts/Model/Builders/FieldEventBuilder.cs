using Model.Factories;
using Providers.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Builders
{
    class FieldEventBuilder
    {
        private string mText;
        private RuleChain mRules;

        public static FieldEventBuilder In()
        {
            return new FieldEventBuilder();
        }

        public FieldEventBuilder SetRules(RuleChain rules)
        {
            mRules = rules;
            return this;
        }

        public FieldEventBuilder SetText(string txt)
        {
            mText = txt;
            return this;
        }

        public FieldEvent Build()
        {
            FieldEvent evnt = BaseModelFactory.CreateInstance<FieldEvent>();
            evnt.Text = mText;
            evnt.EventRule = mRules;
            return evnt;
        }

    }
}
