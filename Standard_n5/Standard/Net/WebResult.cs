using System;
using System.Collections.Generic;
using System.Text;

using System.Net;

namespace maqdel.Infra.Net
{
    /// <summary>
    /// Hamdle the response to a GET/POST web request using a strongly typed data object.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class WebResult<TResult>
    {
        /// <summary>
        /// Creates an instance of the WebResult object
        /// </summary>
        /// <param name="StatusCode"></param>
        /// <param name="Result"></param>
        public WebResult(HttpStatusCode StatusCode, TResult Result)
        {
            this.StatusCode = StatusCode;
            this.Result = Result;
        }

        /// <summary>
        /// The <see cref="HttpStatusCode"/> representing the result state.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Indicate if the Status is Ok
        /// </summary>
        public bool StatusOk {
            get => this.StatusCode == HttpStatusCode.OK ? true : false;
        }

        /// <summary>
        /// The actual result (will only be valid if Status and decoding were successful, otherwise it'll be <c>default(TResult)</c>)
        /// </summary>
        /// <remarks>If serialization of the result was unsuccessful, <see cref="StatusCode"/> will be <c>HttpStatusCode.InternalServerError</c>.</remarks>
        public TResult Result { get; }

        /// <summary>
        /// Indicate if the result is not null
        /// </summary>
        public bool AsResult {
            get => this.Result != null ? true : false;
        }
    }
}
