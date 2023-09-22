using Core.IServices.Others;
using Infrastructure.DTOs.Others;
using Infrastructure.Enums;
using Infrastructure.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using static Infrastructure.DTOs.Others.FCMDTOs.Response;
using static Infrastructure.Enums.Enums;

namespace Services.Utils
{
    public class FCMService : IFCMService
    {
        private const string Key = Constants.FirebaseKey;
        private const string fcmURL = Constants.FireBaseURL + "send";
        private string publicARTopic = "app_ar";
        private string publicHETopic = "app_he";
        private readonly ILogger<FCMService> _logger;



        public FCMService(ILogger<FCMService> logger)
        {
            _logger = logger;
        }


        public async Task SendAllNotificationAsync(FCMDTOs.Response.NotificationBody dataObj, string langKey)
        {
            if (langKey == "HE")
                await SendNotificationAsync(dataObj, publicHETopic, Enums.NotificationType.Notification, Enums.NotificationSendType.Topic);
            else if (langKey == "AR")
                await SendNotificationAsync(dataObj, publicARTopic, Enums.NotificationType.Notification, Enums.NotificationSendType.Topic);
        }
        public async Task SendNotificationAsync(NotificationBody dataObj, string Receiver, Enums.NotificationType Type, Enums.NotificationSendType SentTo)
        {

            string icon = "https://rabbit-app.s3.eu-central-1.amazonaws.com/155550130_140328954613233_6577948963018142212_o.png";

            FCMDTOs.Response.SendPushNotification obj = new();

            if (Type == NotificationType.Data || Type == NotificationType.DataAndNotification)
            {
                obj.data = dataObj.objdata;
            }
            if (Type == NotificationType.Notification || Type == NotificationType.DataAndNotification)
            {
                obj.notification = new FCMDTOs.Response.SendPushNotification.Notification
                {
                    title = dataObj.Title,
                    body = dataObj.message,
                    icon = icon,
                };
            }
            switch (SentTo)
            {
                case Enums.NotificationSendType.Token:
                    obj.to = Receiver;
                    break;
                case Enums.NotificationSendType.Topic:
                    obj.to = "/topics/" + Receiver;
                    break;
            }

            await PostAsync(obj);
        }



        public async Task<string?> PostAsync<T>(T requestObject)
        {
            try
            {
                var dataAsJson = JsonConvert.SerializeObject(requestObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(dataAsJson);

                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "key=" + Key);

                var res = await client.PostAsync(fcmURL, byteContent);
                if (!res.IsSuccessStatusCode)
                    return null;

                var response = await res.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }
    }
}
