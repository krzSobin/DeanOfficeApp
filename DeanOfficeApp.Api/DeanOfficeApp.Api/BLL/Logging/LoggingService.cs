using DeanOfficeApp.Api.DAL.Logging;

namespace DeanOfficeApp.Api.BLL.Logging
{
    public class LoggingService
    {
        private readonly ILoggingRepository _repository;

        public LoggingService(ILoggingRepository repository)
        {
            _repository = repository;
        }

        public void SaveErrorLog(string message)
        {
            _repository.InsertErrorLog(message);
        }

    }
}