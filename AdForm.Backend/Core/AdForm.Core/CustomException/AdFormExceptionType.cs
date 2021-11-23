namespace AdForm.Core
{
    public enum AdFormExceptionType
    {
        AuthenticationException = 1900,
        HeaderException = 1901,
        ApiException = 1902,
    }

    public enum AuthenticationExceptionType
    {
        TokenInvalid,
        Unauthorized,
        SessionExpired,
        Blocked,
        FirstFactorTokenExpired
    }

    public enum HeaderExceptionType
    {
        HeaderMissing
    }
    public enum ApiExceptionType
    {
        ToDoItemAlreadyExists,
        ToDoItemNotfound,
        ToDoListAlreadyExists,
        ToDoListNotfound,
        LabelAlreadyExists,
        LabelNotfound,
        UnsupportedAppVersion,
        ValidationError
    }


}
