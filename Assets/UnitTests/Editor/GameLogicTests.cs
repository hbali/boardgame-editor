using Core;
using Model;
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
    class GameLogicTests
    {
        IBoardGameRepository repo;
        GameLogic logic;

        [SetUp]
        public void Setup()
        {
            repo = new BoardGameRepository();
            for (int i = 0; i < 6; i++)
            {
                string guid = Guid.NewGuid().ToString();
                repo.AddModel<Player>(new Player()
                {
                    Id = guid
                }, guid);
            }
            GameLogic.SetRepo(repo);
            logic = new GameLogic();
        }

        [Test]
        public void StartInvokesNextDelegate()
        {
            called = false;
            logic.Next += Next;
            logic.Start();
            logic.Next -= Next;
            Assert.That(called);
        }

        [Test]
        public void SwitchPlayersCallsNextDelegate()
        {
            called = false;
            logic.Next += Next;
            logic.SwitchPlayers();
            logic.Next -= Next;
            Assert.That(called);
        }

        private bool called;
        private void Next(Player next)
        {
            called = true;
        }

        [TearDown]
        public void Teardown()
        {

        }
    }
}
