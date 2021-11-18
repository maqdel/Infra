using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

using NUnit.Framework;

using Shouldly;

using maqdel.Infra.Misc;

namespace maqdel.Tests.Infra.Misc
{
    [TestFixture]
    [SetCulture("en-US")]
    public class FizzBuzzToolTests
    {        
        [Test]
        public void Classic()
        {
            FizzBuzzTool fizzBuzzTool = new FizzBuzzTool();
            var fizzBuzz = fizzBuzzTool.Classic();

            fizzBuzz.Count.ShouldBe(100);
            fizzBuzz.Contains("Fizz");
            fizzBuzz.Contains("Buzz");
            fizzBuzz.Contains("FizzBuzz");
        }

        [Test]
        public void CustomByRange_ClassicTokens()
        {
            FizzBuzzTool fizzBuzzTool = new FizzBuzzTool();
            var fizzBuzz = fizzBuzzTool.CustomByRange(101, 1000, fizzBuzzTool.ClassicTokens());

            fizzBuzz.Count.ShouldBe(900);
            fizzBuzz.Contains("Fizz");
            fizzBuzz.Contains("Buzz");
            fizzBuzz.Contains("FizzBuzz");
        }

        [Test]
        public void CustomByRange_ABC123()
        {
            FizzBuzzTool fizzBuzzTool = new FizzBuzzTool();
            List<DivisorToken> divisorTokens = new List<DivisorToken>()
            {
                new DivisorToken
                {
                    Divisor = 3,
                    Token = "ABC"
                },
                new DivisorToken
                {
                    Divisor = 5,
                    Token = "123"
                },
            };
            var fizzBuzz = fizzBuzzTool.CustomByRange(1, 100, divisorTokens);

            fizzBuzz.Count.ShouldBe(100);
            fizzBuzz.Contains("ABC");
            fizzBuzz.Contains("123");
            fizzBuzz.Contains("ABC123");
        }

        [Test]
        public void CustomByRange_abcdefg()
        {
            FizzBuzzTool fizzBuzzTool = new FizzBuzzTool();
            List<DivisorToken> divisorTokens = new List<DivisorToken>()
            {
                new DivisorToken
                {
                    Divisor = 3,
                    Token = "a"
                },
                new DivisorToken
                {
                    Divisor = 5,
                    Token = "b"
                },
                new DivisorToken
                {
                    Divisor = 7,
                    Token = "c"
                },
                new DivisorToken
                {
                    Divisor = 9,
                    Token = "d"
                },
                new DivisorToken
                {
                    Divisor = 11,
                    Token = "e"
                },
                new DivisorToken
                {
                    Divisor = 13,
                    Token = "f"
                },
                new DivisorToken
                {
                    Divisor = 15,
                    Token = "g"
                }
            };
            var fizzBuzz = fizzBuzzTool.CustomByRange(1, 10000, divisorTokens);

            fizzBuzz.Count.ShouldBe(10000);
            fizzBuzz.Contains("abdg").ShouldBeTrue();
        }
    }
}