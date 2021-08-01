namespace EnumDefine
{
    public enum AccountType
    {
        Local = 1,
        LDAP = 2,
        External = 3,
        OldSystem = 4,
        OldSystemToLocal = 5
    }
    public enum ErrorCodeEnum
    {
        NoErrorCode = 0,
        Success = 1,
        Fail = 2,
        InternalExceptions = 500,
        Unauthorized = 401,
        NullRequestExceptions = 501,
        PermissionDeny = 403
    }
}