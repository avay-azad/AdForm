namespace AdForm.SDK
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
        ItemAlreadyExists,
        UserDoesNotExist,
        EmailNotVerified,
        UserDetailsDoesNotExist,
        ValidationError,
        ThirdPartyProviderError,
        TopUpTransactionFailed,
        MasterCardError,
        CardError,
        SystemError,
        MaxInvalidOtpAttemptsExceeded,
        MaxResendOtpAttemptsExceeded,
        AccountLocked,
        AccountSuspended,
        SecurityAnswersMaxInvalidAttemptsExceeded,
        OtpExpired,
        SecurityQuestionNotConfigured,
        InvalidOperation,
        ApiError,
        SendGridError,
        OnBoardingNotCompleted,
        IncorrectOTP,
        RequestNull,
        SmartLoginAlreadyConfigured,
        UnsupportedAppVersion
    }


}
