using Database;
using Model;
using Model.Factories;
using Model.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.UnitTests.Editor
{
    [TestFixture]
    public class BaseModelFactoryTests
    {
        IBoardGameRepository repo;

        [Test]
        public void BaseModelFactoryFillsRepo()
        {
            repo = new BoardGameRepository();
            BaseModelFactory.SetRepo(repo);
            string guid = Guid.NewGuid().ToString();
            BaseModelFactory.LoadInstance(new DbItem() { Id = guid });
            Assert.That(repo.GetModel<GameBoardItem>(guid) != null);
        }
    }
}
