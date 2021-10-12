using EnumDefine;

namespace AccountReadModels
{
    public class RRole: AccountBaseReadModel
    {
        public string Name { get; set; }
        public RoleStatusType Status { get; set; }
    }
}