using Infrastructure.Utils;

namespace Infrastructure.Utils
{
    public class StaticFunctions
    {
        public static string GenerateRandomCode(int? length = null)
        {
            if (length is null)
                length = Constants.VerificationCodeDuration;

            bool? isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.Contains("Development");
            if (isDevelopment is null || (isDevelopment.HasValue && isDevelopment.Value))
                return "11111";

            var random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length.Value).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
