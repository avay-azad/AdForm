using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace AdForm.SDK
{
    /// <summary>
    /// Base class for all the custom exceptions for the entire application
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public class AdFormException : Exception
    {
        protected AdFormException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {

        }
        public AdFormException(string message, HttpStatusCode httpcode, IList<ValidationFailure> fielderrors = null)
            : base(message)
        {
            Data.Add("AdForm:HttpStatusCode", httpcode);
            Data.Add("AdForm:FieldErrors", fielderrors);
        }

        public AdFormException(string message, HttpStatusCode httpcode, Exception innerException, IList<ValidationFailure> fielderrors = null)
             : base(message, innerException)
        {
            Data.Add("AdForm:HttpStatusCode", httpcode);
            Data.Add("AdForm:FieldErrors", fielderrors);
        }
    }
}
