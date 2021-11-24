namespace AdForm.Core
{
    public static class LogTemplate
    {
        public const string ExceptionMessageTemplate = "ExceptionMessage: {exceptionMessage}; Exception stack Trace: {stackTrace} CorrelationId : {CorrelationId} responded: {StatusCode} in {timeStamp} ms";
    }
}
