using System;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.ModelNotification;

namespace CookingBox.Business.IServices
{
    public interface INotificationService
    {
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
    }
}
