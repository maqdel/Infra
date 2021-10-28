using System;
using NUnit.Framework;
using Shouldly;

using maqdel.Infra;

namespace maqdel.Test.Infra
{
    [TestFixture]
    public class HelperTests
    {

        [Test]
        public void ToStringHappyPath()
        {
            int test = 999;

            var x = maqdel.infra.Helper.ToString(test);

            x.ShouldBe("");
        }
    }
}