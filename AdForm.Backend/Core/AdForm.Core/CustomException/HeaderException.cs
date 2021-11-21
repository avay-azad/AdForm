using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace AdForm.Core
{
    [Serializable]
    public class HeaderException : AdFormException
    {
        protected HeaderException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {

        }
        public HeaderException(string message, HttpStatusCode httpcode, HeaderExceptionType headerExceptionType, IList<ValidationFailure> fielderrors = null)
       : base(message, httpcode, fielderrors)
        {
            Data.Add("AdForm:ExceptionCode", headerExceptionType);

        }

        public HeaderException(string message, HttpStatusCode httpcode, HeaderExceptionType headerExceptionType, Exception innerException, IList<ValidationFailure> fielderrors = null)
             : base(message, httpcode, innerException, fielderrors)
        {
            Data.Add("AdForm:ExceptionCode", headerExceptionType);
        }
    }
}
