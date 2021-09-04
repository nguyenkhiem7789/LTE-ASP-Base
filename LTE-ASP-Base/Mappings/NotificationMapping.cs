using LTE_ASP_Base.Models;
using NotificationReadModels;

namespace LTE_ASP_Base.Mappings
{
    public class NotificationMapping
    {
        public static NotificationModel ToModel(RNotification rNotification)
        {
            var model = new NotificationModel()
            {
                Id = rNotification.Id,
                Title = rNotification.Title,
                Content = rNotification.Content
            };
            return model;
        }
    }
}