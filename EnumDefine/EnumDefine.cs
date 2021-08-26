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
    public enum ErrorCodeType
    {
        NoErrorCode = 0,
        Success = 1,
        Fail = 2,
        InternalExceptions = 500,
        Unauthorized = 401,
        NullRequestExceptions = 501,
        PermissionDeny = 403
    }

    public enum UserStatusType
    {
        Delete = 0,
        Active = 1,
        InActive = 2
    }

    public enum NotificationType
    {
        All = 0,
        GROUP = 1,
        CLIENT = 2
    }
}