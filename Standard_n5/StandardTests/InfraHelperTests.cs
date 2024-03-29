using NUnit.Framework;

using System.Collections.Generic;

using Shouldly;

using maqdel.Infra;

namespace maqdel.Tests.Infra
{
    [TestFixture]
    [SetCulture("en-US")]
    public class InfraHelperTests
    {        
        [Test]
        public void ConvertToString_int()
        {
            int value = 999;
            var result = maqdel.Infra.InfraHelper.ConvertToString(value);

            result.ShouldBe("999");
        }

        [Test]
        public void ConvertToString_double()
        {
            double value = 999.99;
            var result = maqdel.Infra.InfraHelper.ConvertToString(value);

            result.ShouldBe("999.99");            
        }        

        [Test]
        public void ConvertToString_null()
        {
            var result = maqdel.Infra.InfraHelper.ConvertToString(null);

            result.ShouldBe("");
        }

        [Test]
        public void ConvertToYesNo_Yes()
        {
            bool value = true;
            var result = maqdel.Infra.InfraHelper.ConvertToYesNo(value);

            result.ShouldBe("Yes");
        }

        [Test]
        public void ConvertToYesNo_No()
        {
            bool value = false;
            var result = maqdel.Infra.InfraHelper.ConvertToYesNo(value);

            result.ShouldBe("No");
        }

        [Test]
        public void ConvertToInt_true()
        {
            bool value = true;
            var result = maqdel.Infra.InfraHelper.ConvertToInt(value);

            result.ShouldBe(1);
        }

        [Test]
        public void ConvertToInt_false()
        {
            bool value = false;
            var result = maqdel.Infra.InfraHelper.ConvertToInt(value);

            result.ShouldBe(0);
        }

        [Test]
        public void ConvertToInt_null()
        {            
            var result = maqdel.Infra.InfraHelper.ConvertToInt(null);

            result.ShouldBe(-1);
        }

        [Test]
        public void ConvertListToString_list_string()
        {
            List<string> value = new List<string>();
            value.Add("a");
            value.Add("b");
            value.Add("c");
            var result = maqdel.Infra.InfraHelper.ConvertListToString(value);

            result.ShouldBe("a, b, c");
        }

        [Test]
        public void ConvertListToString_list_int()
        {
            List<int> value = new List<int>();
            value.Add(1);
            value.Add(2);
            value.Add(3);
            var result = maqdel.Infra.InfraHelper.ConvertListToString(value);

            result.ShouldBe("1, 2, 3");
        }
    }
}