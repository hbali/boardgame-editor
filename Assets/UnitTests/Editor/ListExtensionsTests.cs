using Database;
using Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.UnitTests.Editor
{
    [TestFixture]
    public class ListExtensionsTests
    {
        private List<DbItem> items;
        private DbItem i1;
        private DbItem i2;
        private DbItem i3;
        private DbItem i4;

        [SetUp]
        public void Setup()
        {
            i1 = new DbItem();
            i2 = new DbItem();
            i3 = new DbItem();
            i4 = new DbItem();

            items = new List<DbItem>() { i1, i2, i3, i4 };
        }

        [Test]
        public void ReplaceOrAddTestDoesntAddIfContains()
        {
            items.ReplaceOrAdd(i1);
            Assert.That(items.Where(x => x == i1).Count() == 1);
        }

        [Test]
        public void ReplaceOrAddTestAddsIfNotContains()
        {
            items.Remove(i1);
            items.ReplaceOrAdd(i1);
            Assert.That(items.Contains(i1));
        }


    }
}
