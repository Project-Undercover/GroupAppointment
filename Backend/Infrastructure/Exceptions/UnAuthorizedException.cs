namespace Infrastructure.Exceptions
{
    public class UnAuthorizedException : BaseException
    {
        public UnAuthorizedException(string key, params string[] args) : base(key, "You're not authorized to access", args)
        {
        }
    }
}
