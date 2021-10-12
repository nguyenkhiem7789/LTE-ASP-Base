using EnumDefine;

namespace LTE_ASP_Base.Shared.Requests
{
    public class RoleAddRequest
    {
        public string Name { get; set; }
        public RoleStatusType Status { get; set; }
        public string[] ActionDefinedGroups { get; set; }
    }

    public class RoleChangeRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public RoleStatusType Status { get; set; }
    }

    public class RoleGetByIdRequest
    {
        public string Id { get; set; }
    }

    public class RoleGetsRequest 
    {
        public string Keyword { get; set; }
        public RoleStatusType Status { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}