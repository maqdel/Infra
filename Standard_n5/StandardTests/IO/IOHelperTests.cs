using System.Reflection;
using System.Diagnostics;

using System.IO;

using NUnit.Framework;

using Shouldly;

using maqdel.Infra.IO;

namespace maqdel.Tests.Infra.IO
{
    [TestFixture]
    [SetCulture("en-US")]
    public class IOHelperTests
    {        
        [Test]
        public void create_empty_filepath()
        {            
            string textFile = maqdel.Infra.IO.IOHelper.OpenTextFile("");
            textFile.ShouldBe("");
        }

        [Test]
        public void create_empty_filepath2()
        {            
            string filePath = Directory.GetCurrentDirectory() + @"\data\test001.txt";
                        
            string textFile = maqdel.Infra.IO.IOHelper.OpenTextFile(filePath);
                        
            textFile.Length.ShouldBe(13);
        }
    }
}