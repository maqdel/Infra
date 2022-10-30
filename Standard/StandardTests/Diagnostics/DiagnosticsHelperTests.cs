using System.Reflection;
using System.Diagnostics;

using NUnit.Framework;

using Shouldly;

using maqdel.Infra.Diagnostics;

namespace maqdel.Tests.Infra.Diagnostics
{
    [TestFixture]
    [SetCulture("en-US")]
    public class DiagnosticsHelperTests
    {        
        [Test]
        public void GetAssemblyFileVersionInfo()
        {
            var result = DiagnosticsHelper.GetAssemblyFileVersionInfo(Assembly.GetExecutingAssembly());

            result.CompanyName.ShouldBe("StandardTests");
            result.ProductName.ShouldBe("StandardTests");
            result.ProductVersion.ShouldBe("1.0.0");
        }

        [Test]
        public void GetAssemblyProductFullName()
        {
            var result = DiagnosticsHelper.GetAssemblyProductFullName(Assembly.GetExecutingAssembly());

            result.ShouldBe("StandardTests 1.0.0");            
        }

        [Test]
        public void GetAssemblyProductName()
        {
            var result = DiagnosticsHelper.GetAssemblyProductName(Assembly.GetExecutingAssembly());

            result.ShouldBe("StandardTests");
        }

        [Test]
        public void GetAssemblyProductVersion()
        {
            var result = DiagnosticsHelper.GetAssemblyProductVersion(Assembly.GetExecutingAssembly());

            result.ShouldBe("1.0.0");
        }

        [Test]
        public void GetAssemblyCompany()
        {
            var result = DiagnosticsHelper.GetAssemblyCompany(Assembly.GetExecutingAssembly());

            result.ShouldBe("StandardTests");
        }
    }
}