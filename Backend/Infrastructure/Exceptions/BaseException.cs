namespace Infrastructure.Exceptions
{
    public abstract class BaseException : Exception
    {
        public string Key { get; private set; }

        public string[] Args { get; private set; }
        public BaseException(string key, string message = "Base exception", params string[] args) : base(message)
        {
            Key = key;
            Args = args;
        }
    }
}
