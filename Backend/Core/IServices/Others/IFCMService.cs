using Infrastructure.DTOs.Others;
using Infrastructure.Enums;

namespace Core.IServices.Others
{
    public interface IFCMService
    {
        Task SendNotificationAsync(FCMDTOs.Response.NotificationBody dataObj, string Reciver, Enums.NotificationType Type, Enums.NotificationSendType SentTo);
        Task SendAllNotificationAsync(FCMDTOs.Response.NotificationBody dataObj, string langKey);
    }
}
