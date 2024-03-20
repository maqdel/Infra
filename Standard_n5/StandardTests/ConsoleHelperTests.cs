using System;
using NUnit.Framework;

using Shouldly;

using maqdel.Infra;

namespace maqdel.Tests.Infra
{
    [TestFixture]
    [SetCulture("en-US")]
    public class ConsoleHelperTests
    {        
        [Test]
        public void SetConsoleColor_textcolor()
        {
            ConsoleHelper.SetConsoleColor(ConsoleColor.Red);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Red);            
        }

        [Test]
        public void SetConsoleColor_textcolor_backgroundcolor()
        {
            ConsoleHelper.SetConsoleColor(ConsoleColor.Red, ConsoleColor.Gray);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Red);
            Console.BackgroundColor.ShouldBe(ConsoleColor.Gray);
        }

        [Test]
        public void Cls_textcolor()
        {
            ConsoleHelper.Cls(ConsoleColor.Red);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Red);            
        }

        [Test]
        public void Cls_textcolor_backgroundcolor()
        {
            ConsoleHelper.Cls(ConsoleColor.Red, ConsoleColor.Gray);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Red);
            Console.BackgroundColor.ShouldBe(ConsoleColor.Gray);
        }

        [Test]
        public void WriteIn()
        {
            ConsoleHelper.Cls(ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleHelper.WriteIn(40, 10, "abc");
            
            Console.CursorLeft.ShouldBe(40);
            Console.CursorTop.ShouldBe(10);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Red);
            Console.BackgroundColor.ShouldBe(ConsoleColor.Gray);
        }

        [Test]
        public void WriteIn_textcolor()
        {
            ConsoleHelper.Cls(ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleHelper.WriteIn(40, 10, "abc", ConsoleColor.Yellow);
            
            Console.CursorLeft.ShouldBe(40);
            Console.CursorTop.ShouldBe(10);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Yellow);
            Console.BackgroundColor.ShouldBe(ConsoleColor.Gray);
        }

        [Test]
        public void WriteIn_textcolor_backgroundcolor()
        {
            ConsoleHelper.Cls(ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleHelper.WriteIn(40, 10, "abc", ConsoleColor.Yellow, ConsoleColor.Black);
            
            Console.CursorLeft.ShouldBe(40);
            Console.CursorTop.ShouldBe(10);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Yellow);
            Console.BackgroundColor.ShouldBe(ConsoleColor.Black);
        }

        [Test]
        public void WriteCol()
        {
            ConsoleHelper.Cls(ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleHelper.WriteCol(40, 10, 5);
            
            Console.CursorLeft.ShouldBe(40);
            Console.CursorTop.ShouldBe(14);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Red);
            Console.BackgroundColor.ShouldBe(ConsoleColor.Gray);
        }

        [Test]
        public void WriteCol_textcolor()
        {
            ConsoleHelper.Cls(ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleHelper.WriteCol(40, 10, 5, ConsoleColor.Cyan);
            
            Console.CursorLeft.ShouldBe(40);
            Console.CursorTop.ShouldBe(14);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Cyan);
            Console.BackgroundColor.ShouldBe(ConsoleColor.Gray);
        }

        [Test]
        public void WriteCol_textcolor_background()
        {
            ConsoleHelper.Cls(ConsoleColor.Red, ConsoleColor.Black);
            ConsoleHelper.WriteCol(40, 10, 5, ConsoleColor.Cyan);
            
            Console.CursorLeft.ShouldBe(40);
            Console.CursorTop.ShouldBe(14);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Cyan);
            Console.BackgroundColor.ShouldBe(ConsoleColor.Black);
        }

        [Test]
        public void WriteCol_filler()
        {
            ConsoleHelper.Cls(ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleHelper.WriteCol(40, 10, 5, "x");
            
            Console.CursorLeft.ShouldBe(40);
            Console.CursorTop.ShouldBe(14);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Red);
            Console.BackgroundColor.ShouldBe(ConsoleColor.Gray);
        }

        [Test]
        public void WriteCol_textcolor_filler()
        {
            ConsoleHelper.Cls(ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleHelper.WriteCol(40, 10, 5, "x", ConsoleColor.Cyan);
            
            Console.CursorLeft.ShouldBe(40);
            Console.CursorTop.ShouldBe(14);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Cyan);
            Console.BackgroundColor.ShouldBe(ConsoleColor.Gray);
        }

        [Test]
        public void WriteCol_textcolor_background_filler()
        {
            ConsoleHelper.Cls(ConsoleColor.Red, ConsoleColor.Black);
            ConsoleHelper.WriteCol(40, 10, 5, "x", ConsoleColor.Cyan);
            
            Console.CursorLeft.ShouldBe(40);
            Console.CursorTop.ShouldBe(14);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Cyan);
            Console.BackgroundColor.ShouldBe(ConsoleColor.Black);
        }

        [Test]
        public void WriteRow()
        {
            ConsoleHelper.Cls(ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleHelper.WriteRow(40, 10, 5);
            
            Console.CursorLeft.ShouldBe(44);
            Console.CursorTop.ShouldBe(10);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Red);
            Console.BackgroundColor.ShouldBe(ConsoleColor.Gray);
        }

         [Test]
        public void WriteRow_textcolor()
        {
            ConsoleHelper.Cls(ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleHelper.WriteRow(40, 10, 5, ConsoleColor.Cyan);
            
            Console.CursorLeft.ShouldBe(44);
            Console.CursorTop.ShouldBe(10);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Cyan);
            Console.BackgroundColor.ShouldBe(ConsoleColor.Gray);
        }

        [Test]
        public void WriteRow_textcolor_background()
        {
            ConsoleHelper.Cls(ConsoleColor.Red, ConsoleColor.Black);
            ConsoleHelper.WriteRow(40, 10, 5, ConsoleColor.Cyan);
            
            Console.CursorLeft.ShouldBe(44);
            Console.CursorTop.ShouldBe(10);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Cyan);
            Console.BackgroundColor.ShouldBe(ConsoleColor.Black);
        }

        [Test]
        public void WriteRow_filler()
        {
            ConsoleHelper.Cls(ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleHelper.WriteRow(40, 10, 5, "x");
            
            Console.CursorLeft.ShouldBe(44);
            Console.CursorTop.ShouldBe(10);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Red);
            Console.BackgroundColor.ShouldBe(ConsoleColor.Gray);
        }

        [Test]
        public void WriteRow_textcolor_filler()
        {
            ConsoleHelper.Cls(ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleHelper.WriteRow(40, 10, 5, "x", ConsoleColor.Cyan);
            
            Console.CursorLeft.ShouldBe(44);
            Console.CursorTop.ShouldBe(10);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Cyan);
            Console.BackgroundColor.ShouldBe(ConsoleColor.Gray);
        }

        [Test]
        public void WriteRow_textcolor_background_filler()
        {
            ConsoleHelper.Cls(ConsoleColor.Red, ConsoleColor.Black);
            ConsoleHelper.WriteRow(40, 10, 5, "x", ConsoleColor.Cyan);
            
            Console.CursorLeft.ShouldBe(44);
            Console.CursorTop.ShouldBe(10);

            Console.ForegroundColor.ShouldBe(ConsoleColor.Cyan);
            Console.BackgroundColor.ShouldBe(ConsoleColor.Black);
        }
    }
}