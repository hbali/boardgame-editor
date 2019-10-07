using System;
using Model.Repository;
using Transaction;
using View;

namespace Model
{
    public abstract class BaseModel : Snapshot
    {
        public virtual BaseViewModel View { get; set; }

        protected static IBoardGameRepository _repo;

        public bool Deleted { get; set; }

        public string Id { get; set; }

        public abstract Memento GetSnapshot(bool deleted = false);

        public abstract void LoadSnapshot(Memento m);

        public abstract void LoadDependantFields();

        public abstract void LoadModel();

        internal static void SetRepo(IBoardGameRepository repo)
        {
            _repo = repo;
        }
    }
}