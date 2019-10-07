using Database;
using NUnit.Framework;
using Extensions;
using Model;

namespace Assets.UnitTests.Editor
{
    [TestFixture]
    public class ReflectionExtensionsTests
    {
        [Test]
        public void InheritsFromTest()
        {
            bool inherits = (typeof(DbEdge)).InheritsFrom(typeof(DbModel));
            bool notinherits = (typeof(DbEdge)).InheritsFrom(typeof(BaseModel));
            Assert.That(inherits && !notinherits);
        }
    }
}
