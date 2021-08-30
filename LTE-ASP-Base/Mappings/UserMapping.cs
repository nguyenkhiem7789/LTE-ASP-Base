using AccountReadModels;
using LTE_ASP_Base.Models;

namespace LTE_ASP_Base.Mappings
{
    public class UserMapping
    {
        public static UserModel ToModel(RUser rUser)
        {
            var model = new UserModel()
            {
                Id = rUser.Id,
                FullName = rUser.FullName,
                Email = rUser.Email,
                Status = rUser.Status
            };
            return model;
        }
    }
}