using maqdel.Infra;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;

namespace maqdel.Tests.Infra
{
    [TestFixture]
    [SetCulture("en-US")]
    public class ExtensionsTests
    {   
        [Test]
        public void String_CharacterCount()
        {
            string s = "abcdef";
            int i = s.CharacterCount();            
            var result = i;
            result.ShouldBe(6);
        }

        [Test]
        public void String_CharacterCount_string_empty()
        {
            string s = "";
            int i = s.CharacterCount();            
            var result = i;
            result.ShouldBe(0);
        }

        [Test]
        public void String_ToCapitalize()
        {
            string s = "mario";
            string i = s.ToCapitalize();            
            var result = i;
            result.ShouldBe("Mario");
        }

        [Test]
        public void String_ToCapitalize_string_empty()
        {
            string s = "";
            string i = s.ToCapitalize();            
            var result = i;
            result.ShouldBe("");
        }

        [Test]
        public void DateTimew_ToUniversalDateTime()
        {
            DateTime s = new DateTime(2022, 1, 20);
            string i = s.ToUniversalDateTime();            
            var result = i;
            result.ShouldBe("2022-01-20 00:00:00.000");
        }



        [Test]
        public void String_WordCount()
        {
            string s = "Hello Extension Methods";
            int i = s.WordCount();            
            var result = i;
            result.ShouldBe(3);
        }        

        [Test]
        public void String_WordCount_string_empty()
        {
            string s = "";
            int i = s.WordCount();            
            var result = i;
            result.ShouldBe(0);
        }
    }
}