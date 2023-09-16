using System.IdentityModel.Tokens.Jwt;

namespace API.Utils
{
    public static class Headers
    {

        public static Guid GetUserIdFromToken(IHeaderDictionary headers)
        {
            try
            {
                string token = (headers["Authorization"]).ToString().Remove(0, 7);
                if (string.IsNullOrEmpty(token))
                    return Guid.Empty;
                var tokens = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
                var Userid = Guid.Parse(tokens.Claims.First(c => c.Type.ToLower() == "id").Value);
                return Userid;
            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
        }

        public static string GetLanguage(IHeaderDictionary headers)
        {
            string? lang = headers["Language"];
            return string.IsNullOrEmpty(lang) ? "EN" : lang.ToUpper();
        }
    }
}
