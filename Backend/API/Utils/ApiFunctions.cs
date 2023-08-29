namespace API.Utils
{
    public static class ApiFunctions
    {


        /// <summary>
        /// Check if we're in development ENV
        /// </summary>
        /// <param name="hostEnvironment"></param>
        /// <returns>true if we are in development mode</returns>
        public static bool InDevelopment(this IHostEnvironment hostEnvironment)
        {
            return hostEnvironment.EnvironmentName.Contains("Development");
        }






    }
}
