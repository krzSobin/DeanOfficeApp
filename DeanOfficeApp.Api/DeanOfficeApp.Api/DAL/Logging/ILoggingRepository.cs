using System;

namespace DeanOfficeApp.Api.DAL.Logging
{
    public interface ILoggingRepository : IDisposable
    {
        void InsertErrorLog(string message);
    }
}
