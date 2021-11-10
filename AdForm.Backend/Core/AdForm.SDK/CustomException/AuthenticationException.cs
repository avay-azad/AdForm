using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace AdForm.SDK
{
    [Serializable]
    public class AuthenticationException : AdFormException
    {
        protected AuthenticationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {

        }
        public AuthenticationException(string message, HttpStatusCode httpcode, AuthenticationExceptionType authenticationExceptionType, IList<ValidationFailure> fielderrors = null)
        : base(message, httpcode, fielderrors)
        {
            Data.Add("AdForm:ExceptionCode", authenticationExceptionType);

        }

        public AuthenticationException(string message, HttpStatusCode httpcode, AuthenticationExceptionType authenticationExceptionType, Exception innerException, IList<ValidationFailure> fielderrors = null)
             : base(message, httpcode, innerException, fielderrors)
        {
            Data.Add("AdForm:ExceptionCode", authenticationExceptionType);
        }
    }
}
