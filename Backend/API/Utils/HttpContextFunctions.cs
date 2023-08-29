namespace API.Utils
{
    public static class HttpContextFunctions
    {
        public static T GetUser<T>(this HttpContext httpContext)
        {
            object? item = httpContext.Items["User"];
            if (item is not T)
                throw new Exception($"User is not an {typeof(T).Name}");

            if (item is null)
                throw new Exception("User cant be null");

            var user = (T)item;

            return user;
        }
    }
}
