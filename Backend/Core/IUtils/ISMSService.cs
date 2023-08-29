namespace Core.IUtils
{
    public interface ISMSService
    {
        Task SendMessage(string message);
    }
}
