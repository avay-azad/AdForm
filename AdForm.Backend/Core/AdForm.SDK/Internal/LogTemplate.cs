namespace AdForm.SDK
{
    public static class LogTemplate
    {
        public const string EntryMessageTemplate = "{ClassName} :: {FunctionName} :: Entry :: {CorrelationId} :: {RequestData} ::  at {UtcDateTime}";
        public const string ExitMessageTemplate = "{ClassName} :: {FunctionName} ::   Exit :: {CorrelationId}  :: at {UtcDateTime} :: {Elapsed:0.0000} ms";
        public const string ExitMessageTemplateWithResponse = "{ClassName} :: {FunctionName} ::   Exit :: {CorrelationId}  :: {ResponseData} :: at {UtcDateTime} :: {Elapsed:0.0000} ms";
        public const string InformationMessageTemplate = "{ClassName} :: {FunctionName} ::   CorrelationId :: {CorrelationId}  :: {ResponseData} :: at {UtcDateTime}";
        public const string WarningMessageTemplate = "{ClassName} :: {FunctionName} ::   CorrelationId :: {CorrelationId}  :: {ResponseData} :: at {UtcDateTime}";
        public const string ThirdPartyApiExitMessageTemplate = "{ClassName} :: {FunctionName} ::   Exit :: {CorrelationId}  :: at {UtcDateTime} :: {Elapsed:0.0000} ms :: {Response}";
        public const string ExceptionMessageTemplate = "ExceptionMessage: {exceptionMessage}; Exception stack Trace: {stackTrace} CorrelationId : {CorrelationId} responded: {StatusCode} in {timeStamp} ms";
        public const string ResponseMessageTemplate = "HTTP {RequestMethod} {RequestPath} CorrelationId : {CorrelationId} responded {StatusCode} in {Elapsed:0.0000} ms";
        public const string DebugLocalVariableTemplate = "{ClassName} :: {FunctionName} :: {VariableName} :: {CorrelationId} :: {VariableValue} ::  at {UtcDateTime}";
        public const string DebugResponseMessageTemplate = "HTTP {RequestMethod} :: {RequestPath} :: {CorrelationId} :: {Result} :: at {UtcDateTime}";
        public const string DbRepoEntryMessageTemplate = "{EntityAction} :: {EntityName} :: Entry :: at {UtcDateTime}";
        public const string DbRepoExitMessageTemplate = "{EntityAction} :: {EntityName} :: Exit :: at {UtcDateTime} :: {Elapsed:0.000} ms";
    }
}
