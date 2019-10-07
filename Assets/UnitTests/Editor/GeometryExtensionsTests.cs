using Components;
using NUnit.Framework;
using UnityEngine;
using Extensions;

namespace Assets.UnitTests.Editor
{
    [TestFixture]
    public class GeometryExtensionsTests
    {
        [Test]
		public void Polar2CartesianDegTest()
        {
            Vector2 cart = GeometryExtensions.Polar2CartesianDeg(new Vector2(0, 0), 0, 1);
            Assert.That(cart.x == 1 && cart.y == 0);
        }

        [Test]
        public void Polar2CartesianRadTest()
        {
            Vector2 cart = GeometryExtensions.Polar2CartesianRad(new Vector2(0, 0), 0, 1);
            Assert.That(cart.x == 1 && cart.y == 0);
        }


        [Test]
        public void Vector2Half()
        {
            Vector2 half = GeometryExtensions.Vector2Half(new Vector2(0, 0), new Vector2(1, 1));
            Assert.That(half.x == 0.5f && half.y == 0.5f);
        }
    }
}
