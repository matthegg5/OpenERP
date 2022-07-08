using Microsoft.Extensions.Logging;

namespace OpenERP.Services
{

    public class NullMailService : IMailService
    {
        private readonly ILogger<NullMailService> _logger;

        public NullMailService(ILogger<NullMailService> logger)
        {
            this._logger = logger;
        }
        public void SendMessage(string to, string subject, string message)
        {
            //Log the message
            _logger.LogInformation($"To: {to} - Subject: {subject} - Message: {message}");
        }

    }

}