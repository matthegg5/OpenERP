namespace OpenERP.Services
{
    public interface IMailService
    {
        void SendMessage(string to, string subject, string message);
    }

}