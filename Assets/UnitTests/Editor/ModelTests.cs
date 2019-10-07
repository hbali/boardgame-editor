using Database;
using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.UnitTests.Editor
{
    [TestFixture]
    public class ModelTests
    {

        [Test]
        public void PlayerLoadSnapshotTest()
        {
            string guid = Guid.NewGuid().ToString();
            DbPlayer p = new DbPlayer()
            {
                Id = guid,
                model = "model",
                leftout = 4,
                currencyAmount = 3444,
                anticheat = "password",
                playerName = "Balázs",
            };
            Player mp = new Player();
            mp.LoadSnapshot(p);

            Assert.That(mp.ModelPath == "model"
                            && mp.LeftOut == 4
                            && mp.CurrencyAmount == 3444
                            && mp.AntiCheat == "password"
                            && mp.PlayerName == "Balázs");
        }

        [Test]
        public void EdgeLoadSnapshotTest()
        {
            string guid = Guid.NewGuid().ToString();
            DbEdge e = new DbEdge()
            {
                Id = guid,
                end = "end",
                start = "start"
            };

            Edge edge = new Edge();
            edge.LoadSnapshot(e);
            Assert.That(edge.Start == "start" && edge.End == "end");
        }

        [Test]
        public void FieldLoadSnapshotTest()
        {
            string guid = Guid.NewGuid().ToString();
            DbField f = new DbField()
            {
                Id = guid,
                x = 1,
                y = 1,
                type = FieldType.Standard
            };

            Field field = new Field();
            field.LoadSnapshot(f);
            Assert.That(field.X == 1 && field.Y == 1 && field.FType == FieldType.Standard );
        }

        [Test]
        public void GameBoardItemLoadSnapshotTest()
        {
            string guid = Guid.NewGuid().ToString();
            DbItem i = new DbItem()
            {
                Id = guid,
                description = "item"
            };
            GameBoardItem item = new GameBoardItem();
            item.LoadSnapshot(i);

            Assert.That(item.Description == "item");
        }

        [Test] 
        public void PlayerGetSnapshotTest()
        {
            string guid = Guid.NewGuid().ToString();
            Player p = new Player()
            {
                Id = guid,
                ModelPath = "model",
                LeftOut = 4,
                CurrencyAmount = 3444,
                AntiCheat = "password",
                PlayerName = "Balázs",
            };

            DbPlayer dbp = p.GetSnapshot() as DbPlayer;
            Assert.That(dbp != null
                && dbp.Id == guid 
                && dbp.model == "model" 
                && dbp.leftout == 4
                && dbp.currencyAmount == 3444 
                && dbp.anticheat == "password" 
                && dbp.playerName == "Balázs");
        }

        [Test]
        public void EdgeGetSnapshotTest()
        {
            string guid = Guid.NewGuid().ToString();
            Edge e = new Edge()
            {
                Id = guid,
                End = "end",
                Start = "start"
            };

            DbEdge dbe = e.GetSnapshot() as DbEdge;
            Assert.That(dbe != null && dbe.Id == guid && dbe.start == "start" && dbe.end == "end");
        }

        [Test]
        public void FieldGetSnapshotTest()
        {
            string guid = Guid.NewGuid().ToString();
            Field e = new Field()
            {
                Id = guid,
                X = 1,
                Y = 1,
                FType = FieldType.Standard
            };

            DbField dbf = e.GetSnapshot() as DbField;
            Assert.That(dbf != null && dbf.Id == guid && dbf.x == 1 && dbf.y == 1 && dbf.type == FieldType.Standard);
        }

        [Test]
        public void GameBoardItemGetSnapshotTest()
        {
            string guid = Guid.NewGuid().ToString();
            GameBoardItem item = new GameBoardItem()
            {
                Id = guid,
                Description = "item"
            };

            DbItem dbi = item.GetSnapshot() as DbItem;
            Assert.That(dbi != null && dbi.Id == guid && dbi.description == "item");
        }

    }
}
