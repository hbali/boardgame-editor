using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGameModels;
using Database;
using Model;
using Model.Builders;
using Model.Factories;
using UnityEngine;
using View;

namespace View.Builders
{
    class GridBuilder<T> where T : BaseGridTile
    {
        private int mSides;
        private Vector2 mCenter;
        private T mTile;
        private Transform mParent;
        private FieldEvent mEvent;
        private Field mField;
        private FieldType mType;

        public GridBuilder(T tile)
        {
            this.mTile = tile;
        }

        public GridBuilder()
        {

        }

        public static GridBuilder<T> In() 
        {
            return new GridBuilder<T>();
        }

        public static GridBuilder<T> In(T tile)
        {
            return new GridBuilder<T>(tile);
        }

        public GridBuilder<T> SetType(FieldType type)
        {
            mType = type;
            return this;
        }

        public GridBuilder<T> SetSides(int sideCount)
        {
            mSides = sideCount;
            return this;
        }

        public GridBuilder<T> SetCenter(Vector2 center)
        {
            mCenter = center;
            return this;
        }


        internal GridBuilder<T> SetParent(Transform transform)
        {
            mParent = transform;
            return this;
        }

        internal GridBuilder<T> SetEvent(FieldEvent evnt)
        {
            mEvent = evnt;
            return this;
        }

        public GridBuilder<T> SetField(Field field)
        {
            mField = field;
            return this;
        }

        public T Modify()
        {
            mTile.Center = mCenter;
            return mTile;
        }

        public T Build()
        {
            T tile = BaseViewModelFactory.CreateModel<T>() as T;
            tile.transform.position = new Vector3();
            tile.Center = mCenter;
            CreateField(tile);
            if (mParent != null) tile.transform.SetParent(mParent);
            return tile;
        }

        private void CreateField(T tile)
        {
            if (typeof(T) == typeof(GridTile))
            {
                if (mField == null)
                {
                    tile.Field = FieldBuilder.In()
                        .SetEvent(mEvent)
                        .SetX(mCenter.x)
                        .SetY(mCenter.y)
                        .SetType(mType)
                        .Build();
                }
                else
                {
                    tile.Field = mField;
                }

                tile.Field.View = tile;
            }
        }
    }
}
