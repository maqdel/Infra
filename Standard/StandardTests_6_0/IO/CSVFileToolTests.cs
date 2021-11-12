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
    public class CSVFileToolTests
    {        
        [Test]
        public void CSVFileTool_empty_data()
        {
            CSVFileTool csvFileTool = new CSVFileTool();
            csvFileTool.LoadCSVData("").ShouldBeTrue();
            csvFileTool.Rows.ShouldBe(1);
            csvFileTool.Columns.ShouldBe(1);
            csvFileTool.Fields.Length.ShouldBe(1);
        }

        [Test]
        public void CSVFileTool_1_lines()
        {
            var data = "1,2,3,4,5,6,7,8,9,0";

            CSVFileTool csvFileTool = new CSVFileTool();
            csvFileTool.LoadCSVData(data).ShouldBeTrue();
            csvFileTool.Rows.ShouldBe(1);
            csvFileTool.Columns.ShouldBe(10);
            csvFileTool.Fields.Length.ShouldBe(10);
        }

        [Test]
        public void CSVFileTool_2_lines()
        {
            var data = "1,2,3,4,5,6,7,8,9,0\n1,2,3,4,5,6,7,8,9,0";

            CSVFileTool csvFileTool = new CSVFileTool();
            csvFileTool.LoadCSVData(data).ShouldBeTrue();
            csvFileTool.Rows.ShouldBe(2);
            csvFileTool.Columns.ShouldBe(10);
            csvFileTool.Fields.Length.ShouldBe(20);
        }

        [Test]
        public void CSVFileTool_file()
        {            

            string filePath = Directory.GetCurrentDirectory() + @"\data\csv001.csv";

            CSVFileTool csvFileTool = new CSVFileTool(filePath);

            bool fileLoaded = csvFileTool.LoadFile();

            fileLoaded.ShouldBeTrue();

            csvFileTool.Rows.ShouldBe(3);
            csvFileTool.Columns.ShouldBe(3);
            csvFileTool.Fields.Length.ShouldBe(9);
            
            //filePath.ShouldBe("");
        }
    }
}