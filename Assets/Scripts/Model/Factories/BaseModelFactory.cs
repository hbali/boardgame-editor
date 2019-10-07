using Database;
using Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;

namespace Model.Factories
{
    public static class BaseModelFactory
    {
        private static IBoardGameRepository _repo;

        private static readonly Dictionary<Type, Type> TypeMapping = new Dictionary<Type, Type>
        {
            { typeof(DbField), typeof(Field) },
            { typeof(DbItem), typeof(GameBoardItem) },
            { typeof(DbFieldEvent), typeof(FieldEvent) },
            { typeof(DbEdge), typeof(Edge) },
            { typeof(DbPlayer), typeof(Player) }

        };

        public static BaseModel LoadInstance(DbModel entity)
        {
            BaseModel result = null;
            Type mappedType = GetMappedType(entity);

            if (!entity.Deleted)
            {
                Guid guid = new Guid(entity.Id);

                result = _repo.GetModel(mappedType, entity.Id);     
                
                if (result == null)
                {
                    result = CreateInstance(mappedType, guid.ToString());
                }
                result.LoadSnapshot(entity);
            }
            else if (_repo.GetModel(mappedType, entity.Id) != null)
            {
                DeleteEntity(entity, mappedType);
            }
            return result;
        }

        public static void SetRepo(IBoardGameRepository repo)
        {
            _repo = repo;
        }

        public static Type GetMappedType(DbModel entity)
        {
            return GetMappedType(entity.GetType());
        }

        public static Type GetMappedType(Type sourceType)
        {
            Type mappedType;

            if (!TypeMapping.TryGetValue(sourceType, out mappedType))
            {
                 throw new ArgumentException(string.Format("There is no mapping for database type {0} to model type", sourceType.Name), "entity");
            }
            return mappedType;
        }

        private static void DeleteEntity(DbModel entity, Type type)
        {
            _repo.DeleteModel(type, entity.Id);
        }

        public static BaseModel CreateInstance(Type type, string id)
        {
            BaseModel model = Activator.CreateInstance(type) as BaseModel;
            model.Id = id;
            _repo.AddModel(model, type, model.Id);
            return model;
        }

        public static T CreateInstance<T>(string id) where T : BaseModel
        {
            return CreateInstance(typeof(T), id) as T;
        }

        public static T CreateInstance<T>() where T : BaseModel
        {
            return CreateInstance(typeof(T), Guid.NewGuid().ToString()) as T;
        }
    }
}
