using Database;
using Model.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Builders
{
    class FieldBuilder
    {
        private FieldType mType;
        private float mX;
        private FieldEvent mEvent;
        private float mY;
        private Field mField;

        public FieldBuilder(Field mField)
        {
            this.mField = mField;
            mX = mField.X;
            mY = mField.Y;
            mEvent = mField.FEvent;
            mType = mField.FType;
        }

        public FieldBuilder()
        {
        }

        public static FieldBuilder In()
        {
            return new FieldBuilder();
        }

        public static FieldBuilder Edit(Field mField)
        {
            return new FieldBuilder(mField);
        }

        public FieldBuilder SetX(float x)
        {
            mX = x;
            return this;
        }

        public FieldBuilder SetType(FieldType type)
        {
            mType = type;
            return this;
        }
        public FieldBuilder SetY(float y)
        {
            mY = y;
            return this;
        }

        public FieldBuilder SetEvent(FieldEvent evnt)
        {
            mEvent = evnt;
            return this;
        }

        public Field Change()
        {
            mField.FEvent = mEvent;
            mField.X = mX;
            mField.Y = mY;
            mField.FType = mType;
            return mField;
        }

        public Field Build()
        {
            Field f = BaseModelFactory.CreateInstance<Field>();
            f.FEvent = mEvent;
            f.X = mX;
            f.Y = mY;
            f.FType = mType;
            return f;
        }
    }
}
