using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace AdForm.Core
{
    /// <summary>
    /// Derived class of ENBD exception
    /// </summary>
    /// <seealso cref="ENBD.SDK.EnbdException" />
    [Serializable]
    public class ApiException : AdFormException
    {
        protected ApiException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="httpcode"></param>
        /// <param name="apiExceptionType"></param>
        /// <param name="fielderrors"></param>
        public ApiException(string message, HttpStatusCode httpcode, ApiExceptionType apiExceptionType, IList<ValidationFailure> fielderrors = null)
          : base(message, httpcode, fielderrors)
        {
            Data.Add("AdForm:ExceptionCode", apiExceptionType);
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="httpcode"></param>
        /// <param name="apiExceptionType"></param>
        /// <param name="innerException"></param>
        /// <param name="fielderrors"></param>
        public ApiException(string message, HttpStatusCode httpcode, ApiExceptionType apiExceptionType, Exception innerException, IList<ValidationFailure> fielderrors = null)
             : base(message, httpcode, innerException, fielderrors)
        {
            Data.Add("AdForm:ExceptionCode", apiExceptionType);
        }
    }
}
