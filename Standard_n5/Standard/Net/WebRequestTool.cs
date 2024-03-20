using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Net;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;

namespace maqdel.Infra.Net
{    
    /// <summary>
    /// Provides methods to Get and Post to a web API.
    /// </summary>
    public class WebRequestTool : IWebRequestTool
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(WebRequestTool));

        private const string ContentType = "application/json"; 

        /// <summary>
        /// Deserialize the WebResponse To a WebResult
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="WebResponse"></param>
        /// <returns></returns>
        private WebResult<TResult> DeserializeWebResponseToWebResult<TResult>(HttpWebResponse WebResponse)
        {
            _logger.Info("DeserializeWebResponseToWebResult");            
            TResult result = default(TResult);
            var statusCode = WebResponse.StatusCode;

            using (var responseStream = WebResponse.GetResponseStream())
            {
                try
                {
                    if (responseStream == null)
                    {
                        _logger.DebugFormat("DeserializeWebResponseToWebResult, Response is null - Returning default value.");
                    }
                    else
                    {
                        result = JsonConvert.DeserializeObject<TResult>(new StreamReader(responseStream).ReadToEnd());
                    }
                }
                catch (Exception e)
                {
                    _logger.DebugFormat("DeserializeWebResponseToWebResult, Deserialization of response failed - Returning default value.");
                    _logger.Error("DeserializeWebResponseToWebResult, Serialization failed.", e);
                    statusCode = HttpStatusCode.InternalServerError;
                }
                finally
                {
                    WebResponse.Close();
                    responseStream?.Close();
                }
            }

            return new WebResult<TResult>(statusCode, result);
        }

        /// <summary>
        /// Get WebResult From WebException
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="Exception"></param>
        /// <returns></returns>
        private WebResult<TResult> GetWebResultFromWebException<TResult>(WebException Exception)
        {
            _logger.Info("");
            TResult result = default(TResult);
            HttpStatusCode statusCode = HttpStatusCode.ServiceUnavailable;

            if (Exception.Status == WebExceptionStatus.ProtocolError)
            {
                var response = (HttpWebResponse)Exception.Response;
                statusCode = response.StatusCode;
            }

            return new WebResult<TResult>(statusCode, result);
        }

        /// <summary>
        /// Execute GET method
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="Url"></param>
        /// <returns></returns>
        public WebResult<TResult> ExecuteGET<TResult>(string Url)
        {
            _logger.Info("ExecuteGET");
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.Accept = ContentType;
                httpWebRequest.ContentType = ContentType;
                httpWebRequest.Method = "GET";

                var response = (HttpWebResponse)httpWebRequest.GetResponse();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    _logger.DebugFormat(
                        "ExecuteGET, Request to {0} was not successful (Status code was {1}: {2}). Returning default value.", 
                        Url,
                        response.StatusCode, 
                        response.StatusDescription
                        );
                    response.Close();
                    return new WebResult<TResult>(response.StatusCode, default(TResult));
                }

                _logger.DebugFormat("ExecuteGET, Processing response from {0}", Url);
                return DeserializeWebResponseToWebResult<TResult>(response);
            }
            catch (WebException we)
            {
                _logger.Warn($"ExecuteGET, Web exception in {nameof(ExecuteGET)} accessing {Url}. (Status: {we.Status})");
                _logger.Debug("ExecuteGET, Full exception associated with previous warning.", we);
                return GetWebResultFromWebException<TResult>(we);
            }
            catch (Exception ex)
            {
                _logger.Error($"ExecuteGET, Unexpected exception in {nameof(ExecuteGET)} accessing {Url}", ex);
            }

            return new WebResult<TResult>(HttpStatusCode.InternalServerError, default(TResult));
        }
        
        /// <summary>
        /// Execute GET method Async
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="Url"></param>
        /// <returns></returns>
        public async Task<WebResult<TResult>> ExecuteGETAsync<TResult>(string Url)
        {
            _logger.Info("ExecuteGETAsync");
            return await Task.Run(() => ExecuteGET<TResult>(Url));
        }

        /// <summary>
        /// Execute Post method
        /// </summary>
        /// <typeparam name="TPostData"></typeparam>
        /// <param name="Url"></param>
        /// <param name="PostData"></param>
        /// <returns></returns>
        public HttpStatusCode ExecutePost<TPostData>(string Url, TPostData PostData)
        {
            _logger.Info("ExecutePost");
            if (PostData == null) throw new ArgumentNullException(nameof(PostData), "ExecutePost, Posted data cannot be null");

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.Accept = ContentType;
                httpWebRequest.ContentType = ContentType;
                httpWebRequest.Method = "POST";

                var dataAsJson = JsonConvert.SerializeObject(PostData);
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.WriteLine(dataAsJson);
                    streamWriter.Flush();
                }

                using (var response = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    return response.StatusCode;
                }
            }
            catch (WebException we)
            {
                _logger.Warn($"ExecutePost, Web exception in {nameof(ExecutePost)} accessing {Url}. (Status: {we.Status})");
                _logger.Debug("ExecutePost, Full exception associated with previous warning.", we);

                var statusCode = HttpStatusCode.ServiceUnavailable;

                if (we.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = (HttpWebResponse)we.Response;
                    statusCode = response.StatusCode;
                }

                return statusCode;
            }
            catch (Exception ex)
            {
                _logger.Error($"ExecutePost, Unexpected exception in {nameof(ExecutePost)} accessing {Url}", ex);
                return HttpStatusCode.InternalServerError;
            }
        }

        /// <summary>
        /// Execute Post method Async
        /// </summary>
        /// <typeparam name="TPostData"></typeparam>
        /// <param name="Url"></param>
        /// <param name="PostData"></param>
        /// <returns></returns>
        public async Task<HttpStatusCode> ExecutePostAsync<TPostData>(string Url, TPostData PostData)
        {
            _logger.Info("ExecutePostAsync");
            return await Task.Run(() => this.ExecutePost(Url, PostData));
        }

        /// <summary>
        /// Execute Post With Response
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TPostData"></typeparam>
        /// <param name="Url"></param>
        /// <param name="PostData"></param>
        /// <returns></returns>
        public WebResult<TResult> ExecutePostWithResponse<TResult, TPostData>(string Url, TPostData PostData)
        {
            _logger.Info("ExecutePostWithResponse");
            if (PostData == null) throw new ArgumentNullException(nameof(PostData), "ExecutePostWithResponse, Posted data cannot be null");

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.Accept = ContentType;
                httpWebRequest.ContentType = ContentType;
                httpWebRequest.Method = "POST";

                var dataAsJson = JsonConvert.SerializeObject(PostData);
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.WriteLine(dataAsJson);
                    streamWriter.Flush();
                }

                using (var response = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    // Decode the object from the JSON.
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        _logger.DebugFormat(
                            "ExecutePostWithResponse, Request to {0} was not successful (Status code was {1}: {2}). Returning default value.",
                            Url,
                            response.StatusCode, response.StatusDescription);
                        response.Close();
                        return new WebResult<TResult>(response.StatusCode, default(TResult));
                    }


                    _logger.DebugFormat("ExecutePostWithResponse, Processing response from {0}", Url);
                    return DeserializeWebResponseToWebResult<TResult>(response);
                }
            }
            catch (WebException we)
            {
                _logger.Warn($"ExecutePostWithResponse, Web exception in {nameof(ExecutePostWithResponse)} accessing {Url}. (Status: {we.Status})");
                _logger.Debug("ExecutePostWithResponse, Full exception associated with previous warning.", we);
                return GetWebResultFromWebException<TResult>(we);
            }
            catch (Exception ex)
            {
                _logger.Error($"ExecutePostWithResponse, Unexpected exception in {nameof(ExecutePostWithResponse)} accessing {Url}", ex);
                return new WebResult<TResult>(HttpStatusCode.InternalServerError, default(TResult));
            }
        }

        /// <summary>
        /// Execute Post With Response Async
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TPostData"></typeparam>
        /// <param name="Url"></param>
        /// <param name="PostData"></param>
        /// <returns></returns>
        public async Task<WebResult<TResult>> ExecutePostWithResponseAsync<TResult, TPostData>(string Url, TPostData PostData)
        {
            _logger.Info("ExecutePostWithResponseAsync");
            return await Task.Run(() => ExecutePostWithResponse<TResult, TPostData>(Url, PostData));
        }

    }
}
