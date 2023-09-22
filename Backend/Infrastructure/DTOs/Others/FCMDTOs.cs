
namespace Infrastructure.DTOs.Others
{
    public class FCMDTOs
    {
        public class Request
        {
            public class SendNotification
            {
                public string title_AR { get; set; }
                public string title_HE { get; set; }
                public string text_AR { get; set; }
                public string text_HE { get; set; }
            }
        }

        public class Response
        {
            public record NotificationBody
            {
                public string Title { get; set; } = "Dlelk";
                public string message { get; set; } = "";
                public string subtitle { get; set; } = "";
                public object objdata { get; set; }
                public override string ToString()
                {
                    return $"Title: {Title} Message: {message} subtitile: {subtitle}";
                }
            }


            public record SendPushNotification
            {
                public string priority { get; } = "high";
                public bool content_available { get; } = true;
                public bool mutable_content { get; } = true;
                public Notification notification { get; set; }
                public object data { get; set; }
                public string to { get; set; }
                public Apns apns { get; set; }
                public WebPush webpush { get; set; }

                public record Notification
                {
                    public string title { get; set; }
                    public string body { get; set; }
                    public string icon { get; set; }
                    public string sound { get; set; }
                    public string color { get; } = "#FF6633";
                    public string android_channel_id { get; set; } = "new_email_arrived_channel";
                    public string image { get; set; }
                }
                public record WebPush
                {
                    public Headers headers { get; set; }
                    public record Headers
                    {
                        public string image { get; set; }
                    }
                }
                public record Apns
                {

                    public Payload payload { get; set; }
                    public FCMOptions fcm_options { get; set; }
                    public record Payload
                    {
                        public APS aps { get; set; }
                        public record APS
                        {
                            public string sound { get; set; }
                        }
                    }
                    public record FCMOptions
                    {
                        public string image { get; set; }
                    }
                }
            }
        }
    }
}
