using AccountReadModels;
using LTE_ASP_Base.Models;

namespace LTE_ASP_Base.Mappings
{
    public class LoginMapping
    {
        public static LoginModel ToModel(RLoginModel rLoginModel)
        {
            var model = new LoginModel()
            {
               token = rLoginModel.token,
               minuteExpire = rLoginModel.minuteExpire
            };
            return model;
        }
    }
}