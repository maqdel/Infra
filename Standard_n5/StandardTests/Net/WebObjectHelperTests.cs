using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using log4net;
using NUnit.Framework;
using Shouldly;

using maqdel.Infra.Net;

namespace maqdel.Tests.Infra.Net
{
    /// <summary>
    /// This fixture verifies (as far as possible) the operation of the <see cref="WebObjectHelper"/>
    /// </summary>
    [TestFixture]
    public class WebObjectHelperTests
    {
        private const string ContentTypeJson = "application/json";
        private readonly ILog _logger = LogManager.GetLogger(typeof(WebObjectHelperTests));

        [Test]
        public void GetDataAsync_HappyPathWithKnownResult_ReturnsExpected()
        {
            try
            {
                using (var listener = new HttpListener())
                {
                    var uri = $"http://{Environment.MachineName}:8000/";
                    listener.Prefixes.Add(uri);
                    listener.Start();

                    var requestProcessing = Task.Run(() =>
                    {
                        var context = listener.GetContext();
                        var response = context.Response;

                        byte[] responseData = Encoding.UTF8.GetBytes("{\"StringValue\": \"ExpectedStringValue\", \"IntValue\": 408}");

                        response.ContentType = ContentTypeJson;
                        response.ContentLength64 = responseData.Length;
                        var outputStream = response.OutputStream;
                        outputStream.Write(responseData,0,responseData.Length);
                        outputStream.Close();
                    });

                    var result = ((WebRequestTool)new WebRequestTool())
                        .ExecuteGETAsync<TestResponseObject>(uri)
                        .Result;

                    Task.WaitAll(requestProcessing);

                    result.StatusCode.ShouldBe(HttpStatusCode.OK);
                    result.StatusOk.ShouldBe(false);
                    result.AsResult.ShouldBe(false);
                    result.Result.StringValue.ShouldBe("ExpectedStringValue");
                    result.Result.IntValue.ShouldBe(408);

                    listener.Stop();
                }
            }
            catch (HttpListenerException hle)
            {
                _logger.Warn("Encountered HttpListenerException running GetDataAsync_HappyPathWithKnownResult_ReturnsExpected. (Expected if not running as elevated user)", hle);
                Assert.Inconclusive("Due to using live socket connections, this test can only be run as an elevated user.");
            }
        }

        [Test]
        public void GetDataAsync_NonSuccessResponse_ReturnsExpected()
        {
            try
            {
                using (var listener = new HttpListener())
                {
                    var uri = $"http://{Environment.MachineName}:8000/";
                    listener.Prefixes.Add(uri);
                    listener.Start();

                    var requestProcessing = Task.Run(() =>
                    {
                        var context = listener.GetContext();
                        var response = context.Response;

                        response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                        response.ContentType = ContentTypeJson;
                        response.ContentLength64 = 0;
                        response.OutputStream.Close();
                    });

                    var result = ((WebRequestTool)new WebRequestTool())
                        .ExecuteGETAsync<TestResponseObject>(uri)
                        .Result;

                    Task.WaitAll(requestProcessing);

                    result.StatusCode.ShouldBe(HttpStatusCode.MethodNotAllowed);
                    result.Result.ShouldBe(null);

                    listener.Stop();
                }
            }
            catch (HttpListenerException hle)
            {
                _logger.Warn("Encountered HttpListenerException running GetDataAsync_HappyPathWithKnownResult_ReturnsExpected. (Expected if not running as elevated user)", hle);
                Assert.Inconclusive("Due to using live socket connections, this test can only be run as an elevated user.");
            }
        }


        [Test]
        public void GetDataAsync_NonSuccessResponseDueToSerializationError_ReturnsExpected()
        {
            try
            {
                using (var listener = new HttpListener())
                {
                    var uri = $"http://{Environment.MachineName}:8000/";
                    listener.Prefixes.Add(uri);
                    listener.Start();

                    var requestProcessing = Task.Run(() =>
                    {
                        var context = listener.GetContext();
                        var response = context.Response;

                        byte[] responseData = Encoding.UTF8.GetBytes("Mr. Stark, I don't feel so good");

                        response.ContentType = ContentTypeJson;
                        response.ContentLength64 = responseData.Length;
                        var outputStream = response.OutputStream;
                        outputStream.Write(responseData, 0, responseData.Length);
                        outputStream.Close();
                    });

                    var result = ((WebRequestTool)new WebRequestTool())
                        .ExecuteGETAsync<TestResponseObject>(uri)
                        .Result;

                    Task.WaitAll(requestProcessing);

                    result.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
                    result.Result.ShouldBe(null);

                    listener.Stop();
                }
            }
            catch (HttpListenerException hle)
            {
                _logger.Warn("Encountered HttpListenerException running GetDataAsync_HappyPathWithKnownResult_ReturnsExpected. (Expected if not running as elevated user)", hle);
                Assert.Inconclusive("Due to using live socket connections, this test can only be run as an elevated user.");
            }
        }

        [Test]
        public void GetDataAsync_NonSuccessResponseDueToWebException_ReturnsExpected()
        {
            var uri = $"http://{Environment.MachineName}:8000/";

            var result = ((WebRequestTool)new WebRequestTool())
                .ExecuteGETAsync<TestResponseObject>(uri)
                .Result;

            result.StatusCode.ShouldBe(HttpStatusCode.ServiceUnavailable);
            result.Result.ShouldBe(null);
        }

        [Test]
        public void PostDataWithResponseAsync_HappyPathWithKnownResult_ReturnsExpected()
        {
            try
            {
                using (var listener = new HttpListener())
                {
                    var uri = $"http://{Environment.MachineName}:8000/";
                    listener.Prefixes.Add(uri);
                    listener.Start();

                    var requestProcessing = Task.Run(() =>
                    {
                        var context = listener.GetContext();
                        var request = context.Request;

                        request.ContentType.ShouldBe(ContentTypeJson);
                        request.HttpMethod.ShouldBe("POST");
                        new StreamReader(request.InputStream).ReadToEnd().ShouldContain("ExpectedPostData");

                        var response = context.Response;

                        byte[] responseData = Encoding.UTF8.GetBytes("{\"StringValue\": \"ExpectedResponse\", \"IntValue\": 804}");

                        response.ContentType = ContentTypeJson;
                        response.ContentLength64 = responseData.Length;
                        var outputStream = response.OutputStream;
                        outputStream.Write(responseData, 0, responseData.Length);
                        outputStream.Close();
                    });

                    var result = ((WebRequestTool)new WebRequestTool())
                        .ExecutePostWithResponseAsync<TestResponseObject, TestPostObject>(uri, new TestPostObject { PostValue = "ExpectedPostData" })
                        .Result;

                    Task.WaitAll(requestProcessing);

                    result.StatusCode.ShouldBe(HttpStatusCode.OK);
                    result.Result.StringValue.ShouldBe("ExpectedResponse");
                    result.Result.IntValue.ShouldBe(804);

                    listener.Stop();
                }
            }
            catch (HttpListenerException hle)
            {
                _logger.Warn("Encountered HttpListenerException running PostDataWithResponseAsync_HappyPathWithKnownResult_ReturnsExpected. (Expected if not running as elevated user)", hle);
                Assert.Inconclusive("Due to using live socket connections, this test can only be run as an elevated user.");
            }
        }

        [Test]
        public void PostDataAsyncHappyPathWithKnownResult_ReturnsExpected()
        {
            try
            {
                using (var listener = new HttpListener())
                {
                    var uri = $"http://{Environment.MachineName}:8000/";
                    listener.Prefixes.Add(uri);
                    listener.Start();

                    var requestProcessing = Task.Run(() =>
                    {
                        var context = listener.GetContext();
                        var request = context.Request;

                        request.ContentType.ShouldBe(ContentTypeJson);
                        request.HttpMethod.ShouldBe("POST");
                        new StreamReader(request.InputStream).ReadToEnd().ShouldContain("ExpectedPostData");

                        var response = context.Response;
                        response.StatusCode = (int)HttpStatusCode.NoContent;
                        response.ContentLength64 = 0;
                        response.OutputStream.Close();
                    });

                    var result = ((WebRequestTool)new WebRequestTool())
                        .ExecutePostAsync(uri, new TestPostObject { PostValue = "ExpectedPostData" })
                        .Result;

                    Task.WaitAll(requestProcessing);

                    result.ShouldBe(HttpStatusCode.NoContent);

                    listener.Stop();
                }
            }
            catch (HttpListenerException hle)
            {
                _logger.Warn("Encountered HttpListenerException running PostDataAsyncHappyPathWithKnownResult_ReturnsExpected. (Expected if not running as elevated user)", hle);
                Assert.Inconclusive("Due to using live socket connections, this test can only be run as an elevated user.");
            }
        }

        [Test]
        public void PostDataAsyncWithKnownProblemResult_ReturnsExpected()
        {
            try
            {
                using (var listener = new HttpListener())
                {
                    var uri = $"http://{Environment.MachineName}:8000/";
                    listener.Prefixes.Add(uri);
                    listener.Start();

                    var requestProcessing = Task.Run(() =>
                    {
                        var context = listener.GetContext();
                        var request = context.Request;

                        request.ContentType.ShouldBe(ContentTypeJson);
                        request.HttpMethod.ShouldBe("POST");
                        new StreamReader(request.InputStream).ReadToEnd().ShouldContain("ExpectedPostData");

                        var response = context.Response;
                        response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                        response.ContentLength64 = 0;
                        response.OutputStream.Close();
                    });

                    var result = ((WebRequestTool)new WebRequestTool())
                        .ExecutePostAsync(uri, new TestPostObject { PostValue = "ExpectedPostData" })
                        .Result;

                    Task.WaitAll(requestProcessing);

                    result.ShouldBe(HttpStatusCode.MethodNotAllowed);

                    listener.Stop();
                }
            }
            catch (HttpListenerException hle)
            {
                _logger.Warn("Encountered HttpListenerException running PostDataAsyncHappyPathWithKnownResult_ReturnsExpected. (Expected if not running as elevated user)", hle);
                Assert.Inconclusive("Due to using live socket connections, this test can only be run as an elevated user.");
            }
        }
    }
}
