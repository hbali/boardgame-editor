using Database;
using Model;
using Model.Builders;
using Model.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;

namespace Assets.UnitTests.Editor
{
    [TestFixture]
    public class RebuilderTests
    {
        IBoardGameRepository repo;
        private List<Memento> mementos;

        [SetUp]
        public void Setup()
        {
            repo = new BoardGameRepository();
            Workspace.Instance.Reset(repo);
            mementos = new List<Memento>();
            for (int i = 0; i < 10; i++)
            {
                string fieldguid = Guid.NewGuid().ToString();
                DbField field = new DbField()
                {
                    Id = fieldguid
                };
                mementos.Add(field);
                mementos.Add(new DbPlayer() { Id = Guid.NewGuid().ToString(), model = "knight", fieldId = fieldguid });
            }
        }

        [Test]
        public void RebuildLoadsModels()
        {
            Rebuilder.Changes(mementos).Build();
            Assert.That(repo.GetAllModelsOfType<Player>().Count() == 10 && !repo.GetAllModelsOfType<Player>().Any(x => string.IsNullOrEmpty(x.Id)));
        }

        [TearDown]
        public void Teardown()
        {
            Workspace.Instance.Reset();
        }

    }
}
