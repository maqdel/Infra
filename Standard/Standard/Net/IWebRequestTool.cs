using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Threading.Tasks;

namespace maqdel.Infra.Net
{
    public interface IWebRequestTool
    {
        /// <summary>
        /// Makes a HTTP Get request to the specified URL and returns the response as the specified type.
        /// </summary>
        /// <typeparam name="TResult">The expected result type.</typeparam>
        /// <param name="Url">The URL from which the value is to be requested.</param>
        /// <returns>An object providing the web result, as interpreted from the response made to the specified URL.</returns>
        /// <remarks>In the case of a <see cref="WebException"/> occurring, any non-<see cref="HttpWebResponse"/>'s will be converted
        /// into an <c>HttpStatusCode.ServiceUnavailable</c>. For HttpWebResponse's, the returned status code will be returned.</remarks>
        WebResult<TResult> ExecuteGET<TResult>(string Url);

        /// <summary>
        /// Asynchronous variant of <see cref="ExecuteGET{TResult}"/>
        /// </summary>
        /// <typeparam name="TResult">The expected result type.</typeparam>
        /// <param name="Url">The URL from which the value is to be requested.</param>
        /// <returns>An awaitable <see cref="Task"/> that will return the result.</returns>
        /// <remarks>In the case of a <see cref="WebException"/> occurring, any non-<see cref="HttpWebResponse"/>'s will be converted
        /// into an <c>HttpStatusCode.ServiceUnavailable</c>. For HttpWebResponse's, the returned status code will be returned.</remarks>
        Task<WebResult<TResult>> ExecuteGETAsync<TResult>(string Url);

        /// <summary>
        /// Posts the specified data, encoded as a JSON object, to the specified endpoint.
        /// </summary>
        /// <typeparam name="TPostData">The type of data to post.</typeparam>
        /// <typeparam name="TResult">The type of data expected as a response.</typeparam>
        /// <param name="Url">The URL to which the data should be posted.</param>
        /// <param name="PostData">The data itself.</param>
        /// <returns>An object providing the web result, as interpreted from the response made to the specified URL.</returns>
        /// <remarks>In the case of a <see cref="WebException"/> occurring, any non-<see cref="HttpWebResponse"/>'s will be converted
        /// into an <c>HttpStatusCode.ServiceUnavailable</c>. For HttpWebResponse's, the returned status code will be returned.</remarks>
        WebResult<TResult> ExecutePostWithResponse<TResult, TPostData>(string Url, TPostData PostData);

        /// <summary>
        /// Asynchronous variant of <see cref="ExecutePostWithResponse{TResult,TPostData}"/>.
        /// </summary>
        /// <typeparam name="TPostData">The type of data to post.</typeparam>
        /// <typeparam name="TResult">The type of data expected as a response.</typeparam>
        /// <param name="Url">The URL to which the data should be posted.</param>
        /// <param name="PostData">The data itself.</param>
        /// <returns>An awaitable <see cref="Task"/> that will return the result.</returns>
        /// <remarks>In the case of a <see cref="WebException"/> occurring, any non-<see cref="HttpWebResponse"/>'s will be converted
        /// into an <c>HttpStatusCode.ServiceUnavailable</c>. For HttpWebResponse's, the returned status code will be returned.</remarks>
        Task<WebResult<TResult>> ExecutePostWithResponseAsync<TResult, TPostData>(string Url, TPostData PostData);

        /// <summary>
        /// Posts the specified data, encoded as a JSON object, to the specified endpoint.
        /// </summary>
        /// <typeparam name="TPostData">The type of data to post.</typeparam>
        /// <param name="Url">The URL to which the data should be posted.</param>
        /// <param name="PostData">The data itself.</param>
        /// <returns>The <see cref="HttpStatusCode"/> of the response.</returns>
        /// <remarks>In the case of a <see cref="WebException"/> occurring, any non-<see cref="HttpWebResponse"/>'s will be converted
        /// into an <c>HttpStatusCode.ServiceUnavailable</c>. For HttpWebResponse's, the returned status code will be returned.</remarks>
        HttpStatusCode ExecutePost<TPostData>(string Url, TPostData PostData);

        /// <summary>
        /// Asynchronous variant of <see cref="ExecutePost{TPostData}"/>.
        /// </summary>
        /// <typeparam name="TPostData">The type of data to post.</typeparam>
        /// <param name="Url">The URL to which the data should be posted.</param>
        /// <param name="PostData">The data itself.</param>
        /// <returns>An awaitable task that when completed will provide the <see cref="HttpStatusCode"/> of the response.</returns>
        /// <remarks>In the case of a <see cref="WebException"/> occurring, any non-<see cref="HttpWebResponse"/>'s will be converted
        /// into an <c>HttpStatusCode.ServiceUnavailable</c>. For HttpWebResponse's, the returned status code will be returned.</remarks>
        Task<HttpStatusCode> ExecutePostAsync<TPostData>(string Url, TPostData PostData);
    }
}
